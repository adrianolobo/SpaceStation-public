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
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            goToGame();
        }
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
        await DOTween.To(() => 0f, (alpha) => canvasGroup.alpha = alpha, 1f, 0.8f)
            .SetEase(Ease.OutQuad)
            .AsyncWaitForCompletion();
        return;
    }
    private async Task sceneAppear()
    {
        canvasGroup.alpha = 1;
        await DOTween.To(() => 1f, (alpha) => canvasGroup.alpha = alpha, 0f, 0.8f)
            .SetEase(Ease.OutQuad)
            .AsyncWaitForCompletion();
        return;
    }
}
