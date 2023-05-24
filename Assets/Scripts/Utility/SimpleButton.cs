using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SimpleButton : Button
{
    public Action OnClick;
    public override void OnPointerClick(PointerEventData eventData)
    {
        OnClick?.Invoke();
    }
}
