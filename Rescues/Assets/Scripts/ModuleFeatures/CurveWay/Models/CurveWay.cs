using System;
using System.Collections.Generic;
using UnityEngine;


namespace Rescues
{
	public class CurveWay : MonoBehaviour
	{
		
		#region Fileds
		
		[SerializeField] private float _frontScale = 1f;
		[SerializeField] private float _backScale = 0.8f;
		[SerializeField] private WhoCanUseCurve _whoCanUseWay = WhoCanUseCurve.All;
		[SerializeField] private List<WayPoint> _wayPoints;
		private List<Vector3> _allPoints;
		
#if UNITY_EDITOR
		private CurveWayController _controller;
#endif

		#endregion


		#region Properties

		public List<WayPoint> WayPoints => _wayPoints;
		public Vector3 Scale { get; set; } = Vector3.one;
		public float KKoef { get; set; }
		public float MKoef { get; set; }
		public float FrontScale => _frontScale;
		public float BackScale => _backScale;
		public WhoCanUseCurve WhoCanUseWay => _whoCanUseWay;
		public List<Vector3> AllPoints { get => _allPoints; set => _allPoints = value; }
		public int StartPointId { get; set; }
		public Vector3 GetStartPointPosition => AllPoints[StartPointId];
		public Action<Vector3,  CurveWay> GetScaleAction { get; set; }
		
		#endregion


		#region Methods

		public Vector3 GetScale(Vector3 position)
		{
			GetScaleAction?.Invoke(position, this);
			return Scale;
		}

		#endregion
		
		
		#region UnityEditorMethods

		private void OnDrawGizmos()
		{
			if (_controller == null)
				_controller = new CurveWayController(new List<CurveWay>(){this});

			_controller.CalculateCurveDataForEditor(this);

			var drawPoints = _allPoints;
			Gizmos.color = Color.magenta;
			for (int i = 0; i < drawPoints.Count; i++)
			{
				if (i == drawPoints.Count - 1) continue;
				Gizmos.DrawLine(drawPoints[i], drawPoints[i + 1]);
			}
		}

		#endregion

	}
}