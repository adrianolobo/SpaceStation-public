using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using DG.Tweening;
using System.Threading.Tasks;

public class SceneLoader : Singleton<SceneLoader>
{
    CanvasGroup canvasGroup;
    
    void Awake() {
        canvasGroup = GetComponentInChildren<CanvasGroup>();
        canvasGroup.alpha = 0;
    }
    private async void Start()
    {
        await sceneAppear();
    }

    public async void goToGame()
    {
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
                Debug.Log(canvasGroup.interactable);
            })
            .AsyncWaitForCompletion();
        return;
    }
}
