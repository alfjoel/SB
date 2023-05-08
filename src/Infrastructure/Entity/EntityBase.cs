using Infrastructure.Interfaces;

namespace Infrastructure.Entity;

public class EntityBase: IEmv
{
    public int ZrNumber { get; set; }
    public  string DeviceNumber { get; set; }
    public string DeviceType { get; set; }
    
}