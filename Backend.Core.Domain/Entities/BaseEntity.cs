namespace Backend.Core.Domain.Entities;

public class BaseEntity
{
    public int Id { get; set; }
    public DateTimeOffset Created { get; set; }
    public DateTimeOffset Updated { get; set; }
}
