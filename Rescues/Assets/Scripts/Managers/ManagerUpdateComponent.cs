using UnityEngine;
using System.Collections;

public class ManagerUpdateComponent : MonoBehaviour
{
    /// <summary>
    /// запускает юнитевские апдейты и передает их на обработку
    /// </summary>

    private ManagerUpdate mng;

    public void Setup(ManagerUpdate mng)
    {
        this.mng = mng;
    }

    private void Update()
    {
        mng.Tick();
    }

    private void FixedUpdate()
    {
        mng.TickFixed();
    }

    private void LateUpdate()
    {
        mng.TickLate();
    }
}
