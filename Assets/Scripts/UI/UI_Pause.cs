using Cysharp.Threading.Tasks;
using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UI_Pause : UI_AbstractScreen
{
    public Transform board;
    public TextMeshProUGUI msg;
    public override void Open()
    {
        base.Open();
        board.DOScale(1, 0.5f).SetEase(Ease.OutBack);
    }
    public async void Pause(int time)
    {
        Open();
        for(int i=0;i<time;i++)
        {
            msg.text = $"Game paused\nContinue in <color=red>{time - i}</color>";
            await UniTask.Delay(1000);
        }
        Close();
    }
    public override void Close()
    {
        board.DOScale(0, 0.5f).SetEase(Ease.InBack);
        base.Close();
    }
    public override void Anim()
    {
    }

    public override void ResetAnim()
    {
        board.localScale = Vector3.zero;
    }
    public void QuitGame()
    {
        this.PostEvent(EventID.OnQuitRoom);
        LoadingSystem.Instance.LoadScene("Home",2.0f); 
    }

}
