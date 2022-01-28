using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonEffects : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerUpHandler,IPointerDownHandler
{
    public TMP_FontAsset Normal;
    public TMP_FontAsset Hover;
    private TextMeshProUGUI text;

    private void Awake()
    {
        text = gameObject.GetComponentInChildren<TMPro.TextMeshProUGUI>();
        normalize();
    }

    public void OnPointerEnter(PointerEventData pointerEventData)
    {
        outline();
    }

    public void OnPointerExit(PointerEventData pointerEventData)
    {
        normalize();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        text.color=new Color32(226, 130, 98, 255);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        text.color=new Color32(255, 255, 255, 255);
    }
    private void normalize()
    {
        text.font = Normal;
        text.outlineWidth = 0f;
    }

    private void outline()
    {
        text.font = Hover;
        text.outlineColor = new Color32(226, 130, 98, 255);
        text.outlineWidth = 0.3f;
    }


   
}