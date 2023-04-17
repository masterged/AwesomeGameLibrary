using Audit.Core;

namespace AwesomeGameLibrary.Domain.Audit;

public class FancyAuditEvent
{
    /// <summary>
    /// Поле
    /// </summary>
    public string? MyExtensionField { get; set; }
    
    /// <summary>
    /// Тип
    /// </summary>
    /// 
    public FancyEventType MyFancyEventType { get; set; }
}