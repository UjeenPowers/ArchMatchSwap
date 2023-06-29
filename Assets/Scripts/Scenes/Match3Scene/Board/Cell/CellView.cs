using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System.Threading.Tasks;
using System;
using UnityEngine.EventSystems;

public class CellView : MonoBehaviour
{
    private const float BoardX = 900;
    private const float BoardY = 1500;
    private const float deltaAppear = 600;
    private const float appearDuration = 0.5f;
    private const float refScale = 100;
    private Vector2 PointerDownPos;
    private Tween Tween;
    public Action<Vector2Int> OnSwipe;
    void Awake()
    {
        gameObject.SetActive(false);
    }
    public void OnPointerDown(BaseEventData data)
    {
        PointerEventData pointerData = data as PointerEventData;
        PointerDownPos = pointerData.position;
    }
    public void OnPointerUp(BaseEventData data)
    {
        PointerEventData pointerData = data as PointerEventData;
        Vector2 swipe = pointerData.position - PointerDownPos;
        Vector2Int move = new Vector2Int(0,0);
        if (swipe.magnitude > 30f)
        {
            if (Mathf.Abs(swipe.x) > Mathf.Abs(swipe.y))
            {
                move.x = (int)Mathf.Sign(swipe.x);
            }
            else
            {
                move.y = (int)Mathf.Sign(swipe.y);
            }
            OnSwipe?.Invoke(move);
        }
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
        GameObject.Destroy(gameObject);
        return;
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
