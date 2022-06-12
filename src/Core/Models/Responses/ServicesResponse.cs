using Core.Models.Base;

namespace Core.Models.Repositories;

public class ServicesResponse
{
    public uint Id { get; set; }
    public string Title { get; set; }
    public Icon Icon { get; set; }
    public ICollection<ServiceAreaTypeDto> ServiceAreaTypes { get; set; }
}

public class Icon
{
    public uint Id { get; set; }
    public string Path { get; set; }
}