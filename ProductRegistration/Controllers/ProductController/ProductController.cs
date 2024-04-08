using AutoMapper;
using AutoMapper.QueryableExtensions;
using Data.Repositories;
using Infrastructure.Entity.Sell;
using Infrastructure.Entity.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
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
    private readonly UserManager<loginUser> _userManager;

    public ProductController(IMapper mapper, IRepository<productSelles> repository, UserManager<loginUser> userManager)
    {
        _mapper = mapper;
        _repository = repository;
        _userManager = userManager;
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
    [HttpGet("by-creator")]
    public async Task<IActionResult> GetProductsByCreator()
    {
        var currentUser = await _userManager.GetUserAsync(User);
        var products = _repository.Entities.Where(p => p.Id == currentUser.Id).ToList();
        return Ok(products);
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