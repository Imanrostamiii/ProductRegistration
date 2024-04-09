using System.Security.Claims;
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
public class ProductController : ControllerBase
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
    public async Task<IActionResult> filter(int Id)
    {
        try
        {
            var list = _repository.TableNoTracking;
            if (!string.IsNullOrEmpty(Id.ToString()))
                list = list.Where(p => p.userId.ToString().Contains(Id.ToString()));
            var newlist = list.ProjectTo<ProductSelectDto>(_mapper.ConfigurationProvider).ToList();
            return Ok(newlist);
        }
        catch (Exception e)
        {
            throw new Exception(e.Message);
        }
    }

    [HttpGet]
    public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
    {
        var list = await _repository.TableNoTracking.ToListAsync(cancellationToken);
        return Ok(list);
    }
    
    [HttpPost, Authorize]
    public async Task<IActionResult> Creat(ProductDto dto, CancellationToken cancellationToken)
    {
        try
        {
            var entity = _mapper.Map<productSelles>(dto);
            var id = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            entity.userId = long.Parse(id);
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

    [HttpPut, Authorize]
    public IActionResult Put(ProductDto dto)
    {
        var user = _mapper.Map<productSelles>(dto);
        var id = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
        user.userId = long.Parse(id);
        _repository.Update(user);
        return Ok("Update done");
    }

    [HttpDelete, Authorize]
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