using UnityEngine;


public sealed class EnemyBehaviour : MonoBehaviour
{
    #region Fields


    public EnemyData EnemyData;
    public int PatrolState;
    public int Modificator { get => _modificator; }
    private int _modificator = 1;


    #endregion


    #region Properties

    public bool IsDead;

    #endregion


    #region Methods


    public void InvertModificator()
    {
        _modificator *= -1;
    }


    #endregion
}
