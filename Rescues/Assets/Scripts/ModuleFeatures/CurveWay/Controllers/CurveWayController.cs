using System.Collections.Generic;
using UnityEngine;


namespace Rescues
{
	public class CurveWayController
	{
		#region Fildes

		private const float CURVES_RESOLUTION = 0.001f;
		private List<CurveWay> _curveWays;

		#endregion


		#region Private

		public CurveWayController(List<CurveWay> curveWays)
		{
			_curveWays = curveWays;

			foreach (var curve in _curveWays)
			{
				CalculateCurveData(curve);
				curve.GetScaleAction += GetScaleByCurve;
			}
		}

		#endregion
		
		
		#region Methods

		private void GetScaleByCurve(Vector3 position, CurveWay curve)
		{
			curve.Scale = Vector3.one * (curve.KKoef * position.y + curve.MKoef);
		}

		private void CalculateCurveData(CurveWay curveWay)
		{
			var backPoint = new Vector3(0, float.MinValue, 0);
			var frontPoint = new Vector3(0, float.MaxValue, 0);
			var wayPoints = curveWay.WayPoints;
			float[] distances = new float[wayPoints.Count];
			var allPoints = new List<Vector3>();
			
			for (int i = 0; i < wayPoints.Count; i++)
			{
				if (i == wayPoints.Count - 1) continue;

				distances[i] = Vector3.Distance(wayPoints[i + 1].Position, wayPoints[i].Position);
					
				// Vector3.Distance(_wayPoints[i].ExitPos, _wayPoints[i].Position) +
				// Vector3.Distance(_wayPoints[i + 1].EnterPos, _wayPoints[i].ExitPos) +
				// Vector3.Distance(_wayPoints[i + 1].Position, _wayPoints[i + 1].EnterPos);
			}

			for (int i = 0; i < wayPoints.Count; i++)
			{
				if (i == wayPoints.Count - 1) continue;
				
				//Вычисляем количество сегментов в кривой чтобы кучность отрезков между WayPoint была одинаковая
				var resolution = CURVES_RESOLUTION * distances[0] / distances[i];
				
				for (float resolutionParts = 0; resolutionParts < 1; resolutionParts += resolution)
				{
					//Великая и ужаcная формула кубической кривой Безье
					var point = Mathf.Pow(1 - resolutionParts, 3) * wayPoints[i].Position +
					            3 * resolutionParts * Mathf.Pow(1 - resolutionParts, 2) * wayPoints[i].ExitPos +
					            3 * Mathf.Pow(resolutionParts, 2) * (1 - resolutionParts) * wayPoints[i + 1].EnterPos +
					            Mathf.Pow(resolutionParts, 3) * wayPoints[i + 1].Position;
					
					allPoints.Add(point);
					
					//Определение самой верхней и нижней точек по Y
					if (point.y > backPoint.y)
						backPoint = point;

					if (point.y < frontPoint.y)
						frontPoint = point;
				}
			}

			curveWay.AllPoints = allPoints;
			
			//Вычисление коэфицентов линейной функции для скейла персонажей на Curve
			curveWay.KKoef = (curveWay.BackScale - curveWay.FrontScale) / (backPoint.y - frontPoint.y);
			curveWay.MKoef = curveWay.FrontScale - curveWay.KKoef * frontPoint.y;
		}
		
		public CurveWay GetCurve(Gate enterGate, WhoCanUseCurve type)
		{
			var chosenCurves = _curveWays.FindAll(x => x.WhoCanUseWay == type);
            
			if (chosenCurves.Count == 0)
				chosenCurves = _curveWays.FindAll(x => x.WhoCanUseWay == WhoCanUseCurve.All);
            
			var result = chosenCurves[0];

			foreach (var curve in chosenCurves)
			{
				//Бинарный поиск ближейшей точки curve к enterGate
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

			//Поиск ближайшей Curve к точке enterGate
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

		public void UnloadData()
		{
			foreach (var curve in _curveWays)
			{
				curve.GetScaleAction = null;
			}
		}

#if UNITY_EDITOR
		public void CalculateCurveDataForEditor(CurveWay curveWay) => CalculateCurveData(curveWay);
#endif

		#endregion
	}
}