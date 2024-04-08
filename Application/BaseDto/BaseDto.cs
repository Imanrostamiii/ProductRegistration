using System.ComponentModel.DataAnnotations;
using AutoMapper;
using Infrastructure.Entity.BaseEntity;


namespace Application.BaseDto;

public class BaseDto<TDto, TEntity, TKey> : IHaveCustomMapping
{
    [Key] public TKey Id { get; set; }

    public virtual TEntity ToEntity()
    {
        var config = new MapperConfiguration(cfg =>
        {
            cfg.AddMaps(typeof(CustomMappingProfile).Assembly);
        });
        var mapper = config.CreateMapper();
        return mapper.Map<TEntity>(this);
    }
    public TEntity ToEntity(TEntity entity)
    {
        var config = new MapperConfiguration(cfg =>
        {
            cfg.AddMaps(typeof(CustomMappingProfile).Assembly);
        });
        var mapper = config.CreateMapper();
        return mapper.Map(CastToDerivedClass(this), entity);
    }
 
    protected TDto CastToDerivedClass(BaseDto<TDto, TEntity, TKey> baseInstance)
    {
        var config = new MapperConfiguration(cfg =>
        {
            cfg.AddMaps(typeof(CustomMappingProfile).Assembly);
        });
        var mapper = config.CreateMapper();
        return mapper.Map<TDto>(baseInstance);
    }


    public virtual void ApplyMapping(Profile profile)
    {
        var mappingExpression = profile.CreateMap<TDto, TEntity>();

        var dtoType = typeof(TDto);
        var entityType = typeof(TEntity);
        //Ignore any property of source (like Post.Author) that dose not contains in destination 
        foreach (var property in entityType.GetProperties())
        {
            if (dtoType.GetProperty(property.Name) == null)
                mappingExpression.ForMember(property.Name, opt => opt.Ignore());
        }

        CustomMappings(mappingExpression.ReverseMap());
    }
    public virtual void CustomMappings(IMappingExpression<TEntity, TDto> mapping)
    {
    }
}

public abstract class BaseDto<TDto, TEntity> : BaseDto<TDto, TEntity, int>
    where TDto : class, new()
    where TEntity : BaseEntity<int>, new()
{

}
