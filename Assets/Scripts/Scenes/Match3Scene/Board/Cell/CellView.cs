using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CellView : MonoBehaviour
{
    private const float BoardX = 900;
    private const float BoardY = 1500;
    private const float deltaAppear = 600;
    private const float appearDuration = 0.5f;
    private const float refScale = 100;
    private Tween Tween;
    void Awake()
    {
        gameObject.SetActive(false);
    }
    public void AnimateAppear(Vector2Int position, int width, int hight)
    {
        var finalPos = CalculateLocalPosition(position, width, CalculateCellWidth(width, hight));

        transform.localPosition = new Vector2(finalPos.x + deltaAppear, finalPos.y);
        gameObject.SetActive(true);
        Tween = transform.DOLocalMove(finalPos,appearDuration);

        transform.localScale = CalculateLocalScale(CalculateCellWidth(width, hight));
    }
    public void AnimateUpdate(Vector2Int position, int width, int hight)
    {
        var finalPos = CalculateLocalPosition(position, width, CalculateCellWidth(width, hight));
        Tween = transform.DOLocalMove(finalPos,appearDuration);

        transform.localScale = CalculateLocalScale(CalculateCellWidth(width, hight));
    }
    public async void AnimateDisappear()
    {
        Tween = transform.DOLocalMove(transform.localPosition + new Vector3(deltaAppear,0,0),appearDuration);
        await Tween.AsyncWaitForCompletion();
        gameObject.SetActive(false);
    }
    private float CalculateCellWidth(int width, int height)
    {
        return (Mathf.Min(BoardX/width,BoardY/height));
    }
    private Vector2 CalculateLocalPosition(Vector2Int position,int width, float targetCellWidth)
    {
        float spaceTaken = targetCellWidth*width;
        return new Vector2((position.x*targetCellWidth) - spaceTaken/2, position.y*targetCellWidth);
    }
    private Vector3 CalculateLocalScale(float targetCellWidth)
    {
        float targetScale = targetCellWidth/refScale;
        return new Vector3(targetScale,targetScale,1);
    }
}
