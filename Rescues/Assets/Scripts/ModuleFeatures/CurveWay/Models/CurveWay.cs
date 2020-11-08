using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine;


namespace Rescues
{
	public class CurveWay : MonoBehaviour
	{
		#region Fileds
		
		[SerializeField] private ScalePoint _scaleLine;
		[SerializeField] private WhoCanUseCurve _whoCanUseWay = WhoCanUseCurve.All;
		[SerializeField] private List<WayPoint> _wayPoints;
		[SerializeField, Range(0.005f, 0.5f)] private float _resolution = 0.007f;
		[SerializeField, EnableIf("false")] 
		private int _allPointsCount;
		
		private List<Vector3> _allPoints;
		
		#endregion


		#region Properties

		public WhoCanUseCurve WhoCanUseWay => _whoCanUseWay;
		public List<Vector3> AllPoints => _allPoints;
		public ScalePoint ScalePoint => _scaleLine;
		public int StartPointId { get; set; }
		public Vector3 GetStartPointPosition => AllPoints[StartPointId];
		
		#endregion


		#region UnityMethods

		[Button("Draw curve")]
		private void Awake()
		{
			GetDrawingPoints();
		}

		private void OnDrawGizmos()
		{
			if (_allPoints == null) return;
			
			var drawPoints = _allPoints;
			for (int i = 0; i < drawPoints.Count; i++)
			{
				if (i == drawPoints.Count - 1) continue;
				Gizmos.color = Color.red;
				Gizmos.DrawLine(drawPoints[i], drawPoints[i + 1]);
			}
		}
		
		#endregion


		#region Methods

		private void GetDrawingPoints()
		{
			float[] distances = new float[_wayPoints.Count];
			_allPoints = new List<Vector3>();
			
			for (int i = 0; i < _wayPoints.Count; i++)
			{
				if (i == _wayPoints.Count - 1) continue;

				distances[i] =
					Vector3.Distance(_wayPoints[i].ExitPos, _wayPoints[i].Position) +
					Vector3.Distance(_wayPoints[i + 1].EnterPos, _wayPoints[i].ExitPos) +
					Vector3.Distance(_wayPoints[i + 1].Position, _wayPoints[i + 1].EnterPos);
			}

			for (int i = 0; i < _wayPoints.Count; i++)
			{
				if (i == _wayPoints.Count - 1) continue;
				
				//Вычисляем количество сегментов в кривой чтобы кучность отрезков между WayPoint была одинаковая
				var resolution = _resolution * distances[0] / distances[i];
				
				for (float resolutionParts = 0; resolutionParts < 1; resolutionParts += resolution)
				{
					//Великая и ужаcная формула кубической кривой Безье
					var point = Mathf.Pow(1 - resolutionParts, 3) * _wayPoints[i].Position +
					        3 * resolutionParts * Mathf.Pow(1 - resolutionParts, 2) * _wayPoints[i].ExitPos +
					        3 * Mathf.Pow(resolutionParts, 2) * (1 - resolutionParts) * _wayPoints[i + 1].EnterPos +
					        Mathf.Pow(resolutionParts, 3) * _wayPoints[i + 1].Position;
					
					_allPoints.Add(point);
				}
			}

			_allPointsCount = _allPoints.Count;
		}

		#endregion
	}
}