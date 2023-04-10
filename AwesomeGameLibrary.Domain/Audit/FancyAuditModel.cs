using Audit.Core;

namespace AwesomeGameLibrary.Domain.Audit;

public class FancyAuditEvent : AuditEvent
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