using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.ResourceManagement.ResourceProviders;
using DG.Tweening;
using TMPro;
using System;

public class LoadingScreen : MonoBehaviour
{
    private const float minimumLoadingTime = 1f;
    private const float seqenceFps = 24f;
    [SerializeField] private TextMeshProUGUI Percent;
    [SerializeField] private Camera Camera;
    private Sequence Sequence;
    private float CurrentPercentile;
    void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }
    public void AnimateLoading(AsyncOperationHandle<SceneInstance> operation, Action callback)
    {
        CurrentPercentile = 0f;
        Camera.depth = 0;
        
        Sequence = DOTween.Sequence();
        Sequence.AppendCallback(() => UpdatePercentile(operation));
        Sequence.AppendInterval(1/seqenceFps);
        Sequence.SetLoops(-1);
        Sequence.OnKill(() => callback.Invoke());
    }
    private void UpdatePercentile(AsyncOperationHandle<SceneInstance> operation)
    {
        if (operation.PercentComplete > CurrentPercentile) 
        {
            CurrentPercentile += 1/seqenceFps * (1f/minimumLoadingTime);
            if (CurrentPercentile >= 1)
            {
                Sequence.Kill();
                CurrentPercentile = 1f;
                Camera.depth = -2;
            }
            Percent.text = ((int)(CurrentPercentile*100f)).ToString();
        }
    }
}
