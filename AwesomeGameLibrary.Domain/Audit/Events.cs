namespace AwesomeGameLibrary.Domain.Audit;

public class GenreEvent : FancyAuditEvent
{
    public GenreEvent()
    {
        MyExtensionField = "Aaaa";
        MyFancyEventType = FancyEventType.Type1;
    }
}