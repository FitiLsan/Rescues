using System.Collections.Generic;
using System.Linq;
using ModuleFeatures.CurveWay;
using UnityEngine;


namespace Rescues
{
	public class CurveWay : MonoBehaviour
	{
		#region Fileds

		[SerializeField] private WhoCanUseWayTypes _whoCanUseWay = WhoCanUseWayTypes.All;
		[SerializeField] private List<WayPoint> _points;
		[SerializeField, Range(0.01f, 0.5f)] private float _resolution = 0.0166f;

		private LineRenderer _lineRenderer;

		#endregion


		#region Properties

		public WhoCanUseWayTypes WhoCanUseWay => _whoCanUseWay;

		#endregion


		#region UnityMethods

		private void OnDrawGizmos()
		{
			if (_points.Count > 0)
			{
				var drawPoints = GetDrawingPoints();
				for (int i = 0; i < drawPoints.Count; i++)
				{
					if (i == drawPoints.Count - 1) continue;
					Gizmos.color = Color.red;
					Gizmos.DrawLine(drawPoints[i], drawPoints[i + 1]);
				}
			}
		}

		private void Awake()
		{
			_lineRenderer = GetComponent<LineRenderer>();
			var arrayPoints = GetDrawingPoints().ToArray();
			_lineRenderer.positionCount = arrayPoints.Length;
		}

		#endregion


		#region Methods

		private List<Vector3> GetDrawingPoints()
		{
			List<Vector3> drawingPoints = new List<Vector3>();
			float[] distances = new float[_points.Count];

			for (int i = 0; i < _points.Count; i++)
			{
				if (i == _points.Count - 1) continue;

				distances[i] =
					Vector3.Distance(_points[i].ExitPos, _points[i].Position) +
					Vector3.Distance(_points[i + 1].EnterPos, _points[i].ExitPos) +
					Vector3.Distance(_points[i + 1].Position, _points[i + 1].EnterPos);
			}

			for (int i = 0; i < _points.Count; i++)
			{
				if (i == _points.Count - 1) continue;
				
				//Вычисляем количество сегментов в кривой
				var resolution = _resolution * distances[0] / distances[i];
				
				for (float resolutionParts = 0; resolutionParts < 1; resolutionParts += resolution)
				{
					//Великая и ужаная формула кубической кривой Безье
					var point = Mathf.Pow(1 - resolutionParts, 3) * _points[i].Position +
					        3 * resolutionParts * Mathf.Pow(1 - resolutionParts, 2) * _points[i].ExitPos +
					        3 * Mathf.Pow(resolutionParts, 2) * (1 - resolutionParts) * _points[i + 1].EnterPos +
					        Mathf.Pow(resolutionParts, 3) * _points[i + 1].Position;
					
					drawingPoints.Add(point);
				}
			}

			return drawingPoints;
		}

		#endregion
	}
}