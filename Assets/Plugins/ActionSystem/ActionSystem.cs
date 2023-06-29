using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionSystem
{
    private Action OnActionsFinished;
    public int Actions = 0;
    public void AppendAction(Action action)
    {
        OnActionsFinished += action;
    }
    public void StartAction()
    {
        Actions++;
    }
    public void FinishAction()
    {
        Actions--;
        if (Actions == 0)
        {
            Debug.Log("Actions reached zero");
            OnActionsFinished?.Invoke();
            OnActionsFinished = null;
        }
    }
}
