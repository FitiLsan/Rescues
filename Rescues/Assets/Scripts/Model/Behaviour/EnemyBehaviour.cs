using UnityEngine;


public sealed class EnemyBehaviour : MonoBehaviour
{
    public EnemyData EnemyData;
    public int PatrolState;
    public int Modificator { get => _modificator; }

    private int _modificator = 1;

    public void InvertModificator()
    {
        _modificator *= -1;
    }
}
