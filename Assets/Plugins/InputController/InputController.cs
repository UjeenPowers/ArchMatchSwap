using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using Zenject;

public class InputController
{
    private EventSystem EventSystem;
    public InputController(EventSystem eventSystem)
    {
        EventSystem = eventSystem;
    }
    public void LockInput()
    {
        EventSystem.enabled = false;
    }
    public void UnlockInput()
    {
        EventSystem.enabled = true;
    }
}
