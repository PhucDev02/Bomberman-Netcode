using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using DG.Tweening;
using UnityEngine;

public abstract class UI_AbstractScreen : MonoBehaviour
{

    [SerializeField] protected CanvasGroup canvasGroup;
    public float timeFade = 0.5f;
    public virtual void Open()
    {
        gameObject.SetActive(true);
        Reset();
        canvasGroup.blocksRaycasts = true;
        canvasGroup.DOFade(1, timeFade).OnComplete(() =>
        {
            canvasGroup.interactable = true;
            Anim();
        });

    }

    public virtual void Close()
    {
        canvasGroup.interactable = false;
        canvasGroup.DOFade(0, timeFade).OnComplete(() =>
        {
            canvasGroup.blocksRaycasts = false;
            gameObject.SetActive(false);
        });
    }
    public void Reset()
    {
        canvasGroup.interactable = false;
        canvasGroup.blocksRaycasts = false;
        canvasGroup.alpha = 0;
        ResetAnim();
    }
    public abstract void Anim();
    public abstract void ResetAnim();
    private void OnValidate()
    {
        if (GetComponent<CanvasGroup>() == null)
        {
            canvasGroup = gameObject.AddComponent<CanvasGroup>();
        }
    }
}
