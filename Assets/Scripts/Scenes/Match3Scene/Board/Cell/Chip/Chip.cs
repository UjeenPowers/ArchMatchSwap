using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using Zenject;

public class Chip
{
    [Inject] private AnimationChainer AnimationChainer;
    [Inject] private ActionSystem ActionSystem;
    private const string PrefabAdress = "ChipPrefab";
    private ChipView ChipView;
    private int ChipIndex;
    public void Initialize()
    {
        var prefab = Addressables.LoadAssetAsync<GameObject>(PrefabAdress).WaitForCompletion();
        ChipView = GameObject.Instantiate(prefab).GetComponent<ChipView>();
    }
    public void SetRandomChip()
    {
        ChipIndex = UnityEngine.Random.Range(0,3);
        ChipView.UpdateImage(ChipIndex);
    }
    public void SetCell(Cell cell)
    {
        ChipView.SetParent(cell.CellView.transform);
    }
    public void AnimateSwipe(Cell cell)
    {
        AnimationChainer.AddToQueue(
            () => {ActionSystem.StartAction(); ChipView.MoveToCell(cell.CellView.transform, ()=> ActionSystem.FinishAction());}
        );
        // ChipView.MoveToCell(cell.CellView.transform, callback);
    }
    public void AnimateFall(Cell cell, Action callback)
    {
        AnimationChainer.AddToQueue(
            () => {ActionSystem.StartAction(); ChipView.MoveToCell(cell.CellView.transform, ()=> ActionSystem.FinishAction());}
        );
    }
    public void Disappear()
    {

    }
}
