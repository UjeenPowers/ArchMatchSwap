using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;

public class Cell
{
    private const string PrefabAdress = "CellPrefab";
    public CellView CellView {get; private set;}
    private Chip Chip;
    private Vector2Int Position;
    public void Initialize(Transform parent)
    {
        var prefab = Addressables.LoadAssetAsync<GameObject>(PrefabAdress).WaitForCompletion();
        CellView = GameObject.Instantiate(prefab,parent).GetComponent<CellView>();
    }
    public void Appear(Vector2Int position, int width, int hight)
    {
        Position = position;
        CellView.AnimateAppear(position,width,hight);
    }
    public void UpdateSize(Vector2Int position, int width, int hight)
    {
        Position = position;
        CellView.AnimateUpdate(position,width,hight);
    }
    public void Disappear()
    {
        CellView.AnimateDisappear();
    }
}
