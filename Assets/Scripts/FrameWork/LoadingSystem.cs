using TMPro;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Cysharp.Threading.Tasks;
using System;

public class LoadingSystem : MonoBehaviour
{
    public static LoadingSystem Instance { get; private set; }

    [SerializeField] private Canvas canvas;
    [SerializeField] private CanvasGroup canvasGroup;
    [SerializeField] private Image progressFill;
    [SerializeField] private TextMeshProUGUI progressTxt;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        Reset();
        this.canvasGroup.alpha = 1f;
    }
    public void Reset()
    {
        this.canvasGroup.gameObject.SetActive(false);
        this.progressFill.fillAmount = 0f;
        this.progressTxt.text = "0%";
    }
    public async void LoadScene(string sceneName, float time)
    {
        Reset();
        var scene = SceneManager.LoadSceneAsync(sceneName);
        scene.allowSceneActivation = false;

        //this.canvas.worldCamera = Camera.main;
        this.canvasGroup.gameObject.SetActive(true);
        this.canvasGroup.DOFade(1f, 0.2f);
        await UniTask.Delay(500);
        // allway wait 1s to load
        do
        {
            this.progressFill.DOFillAmount(scene.progress / 0.9f, time).SetEase(Ease.OutQuad);
            int currentPercent = (int)(this.progressFill.fillAmount * 100);
            DOVirtual.Float(currentPercent, (scene.progress / 0.9f) * 100, time, x => progressTxt.text = Mathf.FloorToInt(x) + "%");
            await UniTask.Delay((int)time * 1000);
        }
        while (scene.progress < 0.9f);

        scene.allowSceneActivation = true;
        canvasGroup.DOFade(0, 0.5f);
    }
    public async void LoadScene(string sceneName,Action action)
    {
        Reset();
        var scene = SceneManager.LoadSceneAsync(sceneName);
        scene.allowSceneActivation = false;

        //this.canvas.worldCamera = Camera.main;
        this.canvasGroup.gameObject.SetActive(true);
        this.canvasGroup.DOFade(1f, 0.2f);
        await UniTask.Delay(500);
        // allway wait 1s to load
        do
        {
            this.progressFill.DOFillAmount(scene.progress / 0.9f, 0.5f).SetEase(Ease.OutQuad);
            int currentPercent = (int)(this.progressFill.fillAmount * 100);
            DOVirtual.Float(currentPercent, (scene.progress / 0.9f) * 100, 0.5f, x => progressTxt.text = Mathf.FloorToInt(x) + "%");
            await UniTask.Delay((int)0.5f * 1000);
        }
        while (scene.progress < 0.9f);

        scene.allowSceneActivation = true;
        canvasGroup.DOFade(0, 0.5f).OnComplete(() =>
        {
            action();
        });
    }
}