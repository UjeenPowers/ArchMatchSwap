using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.UI;
using DG.Tweening;
using System;

public class ChipView : MonoBehaviour
{
    private const string spite0 = "BoneSprite";
    private const string spite1 = "ShellSprite";
    private const string spite2 = "ToothSprite";
    [SerializeField] private Image Image;
    public void UpdateImage(int chipId)
    {
        string assetId;
        switch (chipId)
        {
            case 0: assetId = spite0; break;
            case 1: assetId = spite1; break;
            case 2: assetId = spite2; break;
            default: assetId = ""; break;
        }
        Image.sprite = Addressables.LoadAssetAsync<Sprite>(assetId).WaitForCompletion();
    }
    public void SetParent(Transform parent)
    {
        transform.SetParent(parent);
        transform.localPosition = Vector3.zero;
        transform.localScale = Vector3.one;
    }
    public async void MoveToCell(Transform cell, Action callback = null)
    {
        transform.SetParent(cell);
        await transform.DOLocalMove(Vector3.zero,0.5f).AsyncWaitForCompletion();
        callback?.Invoke();
    }
}
