using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;
using Object = UnityEngine.Object;


namespace Rescues
{
    public class LocationController : IExecuteController
    {

        #region Fileds
        
        private LocationData _activeLocation;

        #endregion
        
        
        #region Properties
        
        private LevelController LevelController { get; }
        private GameContext Context { get; }
        public List<LocationData> Locations { get; } = new List<LocationData>();
        public LocationData ActiveLocation
        {
            get => _activeLocation;
            set
            {
                Context.ActiveLocation = value;
                _activeLocation = value;
            }
        }
        private Vector3 GetScale
        {
            get
            {
                var position = Context.Character.Transform.position;
                var scale = Vector3.one * Context.Character.CurveWay.GetScale(position);
                return scale;
            }
        }
        public string LevelName { get; }

        #endregion

        
        #region Private
        
        public LocationController(LevelController levelController, GameContext context, string levelName, Transform levelParent)
        {
            Context = context;
            LevelName = levelName;
            LevelController = levelController;
            var path = Path.Combine(AssetsPathGameObject.Object[GameObjectType.Levels], levelName);
            var locationsData = Resources.LoadAll<LocationData>(path);

            foreach (var location in locationsData)
            {
                var locationInstance = Object.Instantiate(location.LocationPrefab, levelParent);
                locationInstance.name = location.LocationName;
                location.Gates = locationInstance.transform.GetComponentsInChildren<Gate>().ToList();
                location.LocationInstance = locationInstance;
                location.LevelName = levelName;

                if (location.CustomBootScreenPrefab != null)
                {
                    location.CustomBootScreenInstance = Object.Instantiate(location.CustomBootScreenPrefab, levelParent);
                    location.CustomBootScreenInstance.gameObject.name = "BootScreen" + location.LocationName;
                    location.CustomBootScreenInstance.gameObject.SetActive(false);
                }

                foreach (var gate in location.Gates)
                {
                    gate.GoAction += LoadLocation;
                    gate.ThisLocationName = location.LocationName;
                    gate.ThisLevelName = levelName;
                }

                var triggers = location.LocationInstance.transform.GetComponentsInChildren<InteractableObjectBehavior>();
                foreach (var trigger in triggers)
                {
                    Context.AddTriggers(trigger.Type, trigger);
                }
                
                location.DisableOnScene();
                Locations.Add(location);
            }

        }

        #endregion


        #region Methods
        
        private void LoadLocation(Gate gate) => LevelController.LoadLevel(gate);

        public void Execute()
        {
            Context.Character.Transform.localScale = GetScale;
        }
        
        public CurveWay GetCurve(Gate enterGate, WhoCanUseCurve type)
        {
            var curves = _activeLocation.LocationInstance.СurveWays;
            var chosenCurves = curves.FindAll(x => x.WhoCanUseWay == type);
            
            if (chosenCurves.Count == 0)
                chosenCurves = curves.FindAll(x => x.WhoCanUseWay == WhoCanUseCurve.All);
            
            var result = chosenCurves[0];
            
            //Бинарный поиск ближейшей точки curve к enterGate
            foreach (var curve in chosenCurves)
            {
                var min = 0;
                var max = curve.AllPoints.Count;
                int i = max / 2;
                var minDistance = Vector3.Distance(curve.AllPoints[min], enterGate.transform.position);
                
                do
                {
                    var newDistance = Vector3.Distance(curve.AllPoints[i], enterGate.transform.position);
                    if (newDistance < minDistance)
                    {
                        min = i;
                        minDistance = newDistance;
                    }
                    else
                    { 
                        max = i;
                    }
                    
                    i = (max - min) / 2 + min;
                    
                } while (min + 1 != max);

                curve.StartPointId = i;
            }

            var distanceToStartPoint = float.MaxValue;
            
            foreach (var curve in chosenCurves)
            {
                var newDistanceToStartPoint = Vector3.Distance(curve.GetStartPointPosition, enterGate.transform.position);

                if (newDistanceToStartPoint < distanceToStartPoint)
                {
                    distanceToStartPoint = newDistanceToStartPoint;
                    result = curve;
                }
            }
            
            return result;
        }
        
        #endregion
    }
}