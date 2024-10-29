using Cysharp.Threading.Tasks;
using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI_Countdown : MonoBehaviour
{
    public TextMeshProUGUI num;
    public CanvasGroup background;
    public async void StartCountDown()
    {
        gameObject.SetActive(true);
        background.DOFade(1, 0);
        num.text = "3";
        TextAnim();
        await UniTask.Delay(1000);
        num.text = "2";
        TextAnim();
        await UniTask.Delay(1000);
        num.text = "1";
        TextAnim();
        await UniTask.Delay(1000);
        num.text = "Start !!!";
        TextAnim();
        await UniTask.Delay(1000);
        background.DOFade(0, 0.3f).OnComplete(()=>
        {
            gameObject.SetActive(false);
        });
    }
    private void TextAnim()
    {
        num.DOFade(0.5f, 0);
        num.rectTransform.DOScale(2f, 0);
        num.rectTransform.DOScale(1, 0.5f);
        num.DOFade(1, 0.5f);
    }
}
