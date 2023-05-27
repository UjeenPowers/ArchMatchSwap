using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;

public class Chip
{
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
    public void Disappear()
    {

    }
}
