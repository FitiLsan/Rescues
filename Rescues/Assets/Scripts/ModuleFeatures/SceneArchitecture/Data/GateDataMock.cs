using System;

namespace Rescues
{
    public class GateDataMock : IGate
    {
        public string ThisLevelName { get; set; }
        public string ThisLocationName { get; set; }
        public int ThisGateId { get; }
        public string GoToLevelName { get; }
        public string GoToLocationName { get; }
        public int GoToGateId { get; }

        public bool Activated { get; set; }

        public static GateDataMock GetMock(string levelName, string locationName, int id)
        {
            return new GateDataMock(levelName, locationName, id);
        }
        
        private GateDataMock(string levelName, string locationName, int id)
        {
            GoToLevelName = levelName;
            GoToLocationName = locationName;
            GoToGateId = id;
        }
        
        public void LoadWithTransferTime(Action onLoadComplete)
        {
            throw new NotImplementedException("Потому, что это Mock");
        }
    }
}