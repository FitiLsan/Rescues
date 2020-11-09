using NaughtyAttributes;
using UnityEngine;


namespace Rescues
{
	public class ScalePoint : MonoBehaviour
	{
		
		#region Fileds

		[SerializeField] private float _frontScale = 1f;
		[SerializeField] private float _backScale = 0.8f;
		[SerializeField] private Transform _backLine;
		[EnableIf("false"), SerializeField] private float _kKoef;
		[EnableIf("false"), SerializeField] private float _mKoef;
		
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
			Gizmos.DrawLine(_backLine.transform.position - _lineOffset, _backLine.transform.position + _lineOffset);
			_backLine.gameObject.name = "Back scale = " + _backScale;
			gameObject.name = "Front scale = " + _frontScale;
		}
		
		#endregion


		#region Methods

		public float GetScale(Vector3 position)
		{
			return  _kKoef * position.y + _mKoef;
		}
		
		[Button("Calculate koefs")]
		private void Calculate()
		{
			_kKoef = (_backScale - _frontScale) / (_backLine.transform.position.y - transform.position.y);
			_mKoef = _frontScale - _kKoef * transform.position.y;
		}

		#endregion
		
		
	}
}