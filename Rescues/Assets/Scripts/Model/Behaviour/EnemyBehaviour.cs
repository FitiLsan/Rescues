using UnityEngine;


public sealed class EnemyBehaviour : MonoBehaviour
{
    #region Fields

    public Vector3[] WayPoints;
    public EnemyData EnemyData;
    private int _modificator = 1;
    public int detectionDistance;
    public float CurrenTime;
    public float MaxDistance;
    public Vector3 Direction;
    public SpriteRenderer MySprite;
    public bool IsDead;
    public Transform Transform; 

    #endregion


    #region Properties

    public int PatrolState;
    public int Modificator { get => _modificator; }

    #endregion


    #region Methods

    public void InvertModificator()
    {
        _modificator *= -1;
    }

    #endregion
}
