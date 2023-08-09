using CleanArcTemp.Domain.Common;

namespace CleanArcTemp.Domain.Entities;

public class Policy : BaseEntity
{
    public string Name { get; set; } = null!;

    public string? Description { get; set; }
}

