using GLPI.Core.Enums;

namespace GLPI.Core.Models;
public class Status
{
    public long Id { get; set; }
    public EStatus Name { get; set; } = EStatus.Newer;

    public string Color { get; set; } = string.Empty;
}