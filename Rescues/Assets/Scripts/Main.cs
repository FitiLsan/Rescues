using UnityEngine;

public class Main : MonoBehaviour
{
    private InputController InputController { get; set; }

    private void Awake()
    {
        InputController = new InputController();
        InputController.Character = FindObjectOfType<Character>();
    }

    private void Update()
    {
        InputController.OnUpdate();
    }
}
