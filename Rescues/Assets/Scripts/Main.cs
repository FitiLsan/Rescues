using UnityEngine;

public class Main : MonoBehaviour
{
    public InputController InputController { get; private set; }
    public Transform Player { get; private set; }

    public static Main Instance { get; private set; }

    private IOnUpdate[] _controllers;

    private void Awake()
    {
        Instance = this;
        InputController = new InputController();

        Player = GameObject.FindGameObjectWithTag("Player").transform;

        _controllers = new IOnUpdate[1];
        _controllers[0] = InputController;
    }

    private void Update()
    {
        for (var index = 0; index < _controllers.Length; index++)
        {
            var controller = _controllers[index];
            controller.OnUpdate();
        }
    }
}
