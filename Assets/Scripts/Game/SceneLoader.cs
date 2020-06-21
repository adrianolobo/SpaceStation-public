using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using DG.Tweening;
using System.Threading.Tasks;

public class SceneLoader : Singleton<SceneLoader>
{
    CanvasGroup canvasGroup;

    static string stationNameToLoad;
    
    void Awake() {
        canvasGroup = GetComponentInChildren<CanvasGroup>();
        canvasGroup.alpha = 0;
    }
    private async void Start()
    {
        await sceneAppear();
    }

    public string getStationNameToLoad()
    {
        if (stationNameToLoad == null) return "Station1";
        return stationNameToLoad;
    }

    public async void goToGame(string stationName)
    {
        stationNameToLoad = stationName;
        SoundManager.Instance.Play(Sounds.SOUND.PLAY);
        await sceneDisappear();
        SceneManager.LoadScene("GameScene");
    }

    public async void goToMenu()
    {
        await sceneDisappear();
        SceneManager.LoadScene("MenuScene");
    }

    private async Task sceneDisappear()
    {
        canvasGroup.alpha = 0;
        canvasGroup.interactable = false;
        await DOTween.To(() => 0f, (alpha) => canvasGroup.alpha = alpha, 1f, 0.8f)
            .SetEase(Ease.OutQuad)
            .OnComplete(() =>
            {
                canvasGroup.interactable = true;
            })
            .AsyncWaitForCompletion();
        return;
    }
    private async Task sceneAppear()
    {
        canvasGroup.interactable = true;
        canvasGroup.alpha = 1;
        await DOTween.To(() => 1f, (alpha) => canvasGroup.alpha = alpha, 0f, 0.8f)
            .SetEase(Ease.OutQuad)
            .OnComplete(() =>
            {
                canvasGroup.interactable = false;
            })
            .AsyncWaitForCompletion();
        return;
    }
}
