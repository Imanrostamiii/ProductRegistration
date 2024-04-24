using System.ComponentModel.DataAnnotations;

namespace Infrastructure.Entity.BaseEntity;

public class BaseEntity<Tkey>:IEntity
{
    [Key] public Tkey Id { get; set; }
}

public interface IEntity
{
}