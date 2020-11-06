namespace Rescues
{
    public class GateData : IGate
    {
        public string ThisLevelName { get; set; }
        public string ThisLocationName { get; set; }
        public int ThisGateId { get; }
        public string GoToLevelName { get; }
        public string GoToLocationName { get; }
        public int GoToGateId { get; }
        public bool Activated { get; set; }


        public GateData(string levelName, string locationName, int id)
        {
            GoToLevelName = levelName;
            GoToLocationName = locationName;
            GoToGateId = id;
        }
        
    }
}