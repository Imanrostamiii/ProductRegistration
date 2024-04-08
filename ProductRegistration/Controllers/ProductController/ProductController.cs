using AutoMapper;
using AutoMapper.QueryableExtensions;
using Data.Repositories;
using Infrastructure.Entity.Sell;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProductRegistration.Modele.Dto;

namespace ProductRegistration.Controllers.ProductController;
[ApiController]
[Route("api/[controller]/[action]")]
public class ProductController:ControllerBase
{
    private readonly IMapper _mapper;
    private readonly IRepository<productSelles> _repository;

    public ProductController(IMapper mapper, IRepository<productSelles> repository)
    {
        _mapper = mapper;
        _repository = repository;
    }

    [HttpGet]
    public async Task<ActionResult<ProductSelectDto>> Get(int id)
    {
        var model = _mapper.ProjectTo<ProductSelectDto>(_repository.TableNoTracking)
            .FirstOrDefault(p => p.Id.Equals(id));
        return Ok(model);
    }
    [HttpGet]
    public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
    {
        var list = await _repository.TableNoTracking.ToListAsync(cancellationToken);
        return Ok(list);
    }
    
    
    [HttpPost,Authorize]
    public async Task<IActionResult> Creat(ProductDto dto, CancellationToken cancellationToken)
    {
        try
        {
            var entity = _mapper.Map<productSelles>(dto);
            await _repository.AddAsync(entity, cancellationToken);
            var modele = _repository
                .TableNoTracking
                .Where(p => p.Id.Equals(entity.Id))
                .ProjectTo<ProductSelectDto>(_mapper.ConfigurationProvider);
            return Ok(modele);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
    [HttpPut,Authorize]
    public IActionResult Put(int id, ProductDto dto)
    {
        var user = _mapper.Map<productSelles>(dto);
        _repository.Update(user);
        return Ok();
    }

    [HttpDelete,Authorize]
    public async Task<IActionResult> deleteItem(int id)
    {
        var modele = _repository.TableNoTracking.FirstOrDefault(p => Equals(p.Id, id));
        if (modele == null)
        {
            return NotFound();
        }

        var dto = _mapper.Map<ProductSelectDto>(modele);
        _repository.Delete(modele);
        return Ok(dto);
        
    }

}