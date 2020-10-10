namespace Rescues
{
    public interface IGate
    {
        string ThisLevelName { get; set; }
        string ThisLocationName { get; set; }
        int ThisGateId { get; }
        string GoToLevelName { get; }
        string GoToLocationName { get; }
        int GoToGateId { get; }
    }
}