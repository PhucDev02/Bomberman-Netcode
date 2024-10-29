using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonClickAnim : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    [SerializeField] private float scale=0.9f;

    public void OnPointerDown(PointerEventData eventData)
    {
        transform.DOScale(scale, 0.3f);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        transform.DOScale(1, 0.3f);

    }
}
