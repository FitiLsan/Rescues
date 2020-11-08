using NaughtyAttributes;
using UnityEngine;


namespace Rescues
{
	public class ScalePoint : MonoBehaviour
	{

		
		#region Fileds

		[SerializeField]private float _scaleOnLine = 1f;
		[EnableIf("false"), SerializeField] private float _functionKoef;
		
		private readonly Vector3 _lineOffset = new Vector3(20, 0, 0);

		#endregion


		#region UnityMethods

		private void Awake()
		{
			Calculate();
		}

		private void OnDrawGizmos()
		{
			Gizmos.color = Color.cyan;
			Gizmos.DrawLine(transform.position - _lineOffset, transform.position + _lineOffset);
		}
		
		#endregion


		#region Methods

		public float GetScale(Vector3 position)
		{
			var newScale = position.y * _functionKoef;
			return newScale;
		}
		
		[Button("Calculate koefs")]
		private void Calculate()
		{
			_functionKoef = _scaleOnLine / transform.position.y ;
		}

		#endregion
		
		
	}
}