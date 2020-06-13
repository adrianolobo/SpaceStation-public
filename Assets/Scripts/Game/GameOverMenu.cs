using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverMenu : Singleton<GameOverMenu>
{
    private CanvasGroup gameOverOverlayCanvasGroup;
    private CanvasGroup gameOverBackgroundCanvasGroup;
    void Start()
    {
        gameOverOverlayCanvasGroup = transform.Find("GameOverOverlay").GetComponent<CanvasGroup>();
        gameOverBackgroundCanvasGroup = transform.Find("GameOverBackground").GetComponent<CanvasGroup>();

        gameOverOverlayCanvasGroup.gameObject.SetActive(false);
        gameOverBackgroundCanvasGroup.gameObject.SetActive(false);
    }

    public void openGameOverMenu()
    {
        gameOverOverlayCanvasGroup.gameObject.SetActive(true);
        gameOverBackgroundCanvasGroup.gameObject.SetActive(true);
        gameOverOverlayCanvasGroup.interactable = true;
        gameOverOverlayCanvasGroup.blocksRaycasts = true;
        gameOverBackgroundCanvasGroup.alpha = 0;
        gameOverOverlayCanvasGroup.alpha = 0;
        DOTween.To(
            () => 0f,
            (alpha) => gameOverOverlayCanvasGroup.alpha = alpha,
            0.6f,
            1f
        ).SetEase(Ease.OutQuad);
        DOTween.To(
            () => 0f,
            (alpha) => gameOverBackgroundCanvasGroup.alpha = alpha,
            1f,
            1.5f
        )
        .SetDelay(0.8f)
        .SetEase(Ease.OutQuad);
    }

    private void closeGameOverMenu()
    {
        DOTween.To(
            () => gameOverOverlayCanvasGroup.alpha,
            (alpha) => gameOverOverlayCanvasGroup.alpha = alpha,
            0f,
            0.5f
        ).SetEase(Ease.OutQuad);
        DOTween.To(
            () => gameOverBackgroundCanvasGroup.alpha,
            (alpha) => gameOverBackgroundCanvasGroup.alpha = alpha,
            0f,
            0.5f
        )
        .SetEase(Ease.OutQuad);
    }

    public void playAgain()
    {
        SceneLoader.Instance.goToGame(SpaceStation.Instance.name);
        closeGameOverMenu();
    }

    public void goToMenu()
    {
        SceneLoader.Instance.goToMenu();
        closeGameOverMenu();
    }
}
