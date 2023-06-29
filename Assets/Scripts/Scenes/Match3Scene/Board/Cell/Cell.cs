using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using Zenject;

public class Cell
{
    private Match3Model Match3Model;
    private const string PrefabAdress = "CellPrefab";
    public CellView CellView {get; private set;}
    private Vector2Int Position;
    public Cell(Match3Model match3Model)
    {
        Match3Model = match3Model;
    }
    public void Initialize(Transform parent)
    {
        var prefab = Addressables.LoadAssetAsync<GameObject>(PrefabAdress).WaitForCompletion();
        CellView = GameObject.Instantiate(prefab,parent).GetComponent<CellView>();
        CellView.OnSwipe += HandleSwipe;
    }
    private void HandleSwipe(Vector2Int swipe)
    {
        Match3Model.Swipe(Position, swipe);
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
