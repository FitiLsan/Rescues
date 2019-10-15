using UnityEngine;
using System.Collections;

public class TestQuad : MonoBehaviour, ITick
{
    private void Awake()
    {
        ManagerUpdate.AddTo(this);
    }

    public void Tick()
    {
        Debug.Log("Я куб");

        if (Input.GetKeyDown(KeyCode.Space))
        {
            ToolBox.Get<ManagerEvents>().CratePrefab(Random.insideUnitSphere * Random.Range(-10, 100));
        }
    }

}
