using Microsoft.EntityFrameworkCore;
namespace Project.Data.Base;

public interface IEntityBase
{
    public int Id { get; set; }
}