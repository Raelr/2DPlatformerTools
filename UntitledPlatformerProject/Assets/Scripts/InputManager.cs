using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class InputManager : MonoBehaviour
{
    public delegate void ArrowKeysUsed();

    public event ArrowKeysUsed keyboardInput;

    public delegate void MouseMoved();

    public event MouseMoved mouseMoved;

    public UnityEvent UpdateEvent;

    private void Awake() {

        if (UpdateEvent == null) {
            UpdateEvent = new UnityEvent();
        }
    }

    void Update()
    {
        if (UpdateEvent != null) {

            UpdateEvent.Invoke();
        }
    }
}
