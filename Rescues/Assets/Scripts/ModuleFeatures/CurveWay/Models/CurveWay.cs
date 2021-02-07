using System;
using PathCreation;
using UnityEngine;


namespace Rescues
{
	[RequireComponent(typeof(PathCreator))]
	public class CurveWay : MonoBehaviour
	{
		
		#region Fileds
		
		[SerializeField] private float _frontScale = 1f;
		[SerializeField] private float _backScale = 0.8f;
		[SerializeField] private WhoCanUseCurve _whoCanUseWay = WhoCanUseCurve.All;
		[SerializeField] private PathCreator _pathCreator;

		#endregion


		#region Properties

		public PathCreator PathCreator => _pathCreator;
		//public List<WayPoint> WayPoints => _wayPoints;
		public Vector3 Scale { get; set; } = Vector3.one;
		public float KKoef { get; set; }
		public float MKoef { get; set; }
		public float FrontScale => _frontScale;
		public float BackScale => _backScale;
		public WhoCanUseCurve WhoCanUseWay => _whoCanUseWay;
		public Vector3 StartCharacterPosition { get; set; }
		public Vector3 GetStartPointPosition => _pathCreator.path.GetClosestPointOnPath(StartCharacterPosition);
		public Action<Vector3,  CurveWay> GetScaleAction { get; set; }
		
		#endregion


		#region Methods

		public Vector3 GetScale(Vector3 position)
		{
			GetScaleAction?.Invoke(position, this);
			return Scale;
		}

		#endregion

	}
}