using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
public class AnimationChainer
{
    [Inject] private ActionSystem ActionSystem;
    private Queue<Tuple<Action,bool>> AnimationQueue = new Queue<Tuple<Action,bool>>();
    public void AddToQueue(Action action)
    {
        Debug.Log("Try add added to queue");
        if (AnimationQueue.Count != 0) AnimationQueue.Enqueue(new Tuple<Action,bool>(action,false));
        else 
        {
            Debug.Log("Action executed in queue");
            action?.Invoke();
        }
    }
    public void AddBreak()
    {
        Debug.Log("Try add break");
        if (AnimationQueue.Count != 0) {AnimationQueue.Enqueue(new Tuple<Action,bool>(null,true));}
        else 
        {
            AnimationQueue.Enqueue(new Tuple<Action,bool>(null,true));
            ActionSystem.AppendAction(() => ExecuteNextBatch());
        }
    }
    private void ExecuteNextBatch()
    {
        Debug.Log("Execute next batch");
        if (AnimationQueue.Count == 0) return;
        AnimationQueue.Dequeue();
        while (AnimationQueue.Count != 0 && AnimationQueue.Peek().Item2 == false)
        {
            AnimationQueue.Dequeue().Item1?.Invoke();
        }
    }
}
