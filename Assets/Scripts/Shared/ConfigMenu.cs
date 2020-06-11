using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConfigMenu : MonoBehaviour
{
    private CanvasGroup optionsOverlayCanvasGroup;
    private Transform optionsBackground;
    void Start()
    {
        GameEvents.Instance.onCloseConfigMenu += closeConfigMenu;
        GameEvents.Instance.onOpenConfigMenu += openConfigMenu;
        optionsOverlayCanvasGroup = transform.Find("OptionsOverlay").GetComponent<CanvasGroup>();
        optionsBackground = transform.Find("OptionsBackground");

        optionsOverlayCanvasGroup.interactable = false;
        optionsOverlayCanvasGroup.gameObject.SetActive(false);
        optionsBackground.gameObject.SetActive(false);
    }

    private void OnDestroy()
    {
        GameEvents.Instance.onCloseConfigMenu -= closeConfigMenu;
        GameEvents.Instance.onOpenConfigMenu -= openConfigMenu;
    }

    private void openConfigMenu()
    {
        optionsOverlayCanvasGroup.gameObject.SetActive(true);
        optionsBackground.gameObject.SetActive(true);
        optionsBackground.localScale = new Vector3(0, 0, 0);
        DOTween.To(
            () => 0f,
            (alpha) => optionsOverlayCanvasGroup.alpha = alpha,
            0.85f,
            0.4f
        ).SetEase(Ease.OutQuad)
        .OnComplete(() =>
        {
            optionsOverlayCanvasGroup.interactable = true;
            optionsOverlayCanvasGroup.blocksRaycasts = true;
        });
        optionsBackground.DOScale(1, 0.4f).SetEase(Ease.OutBack);
    }

    private void closeConfigMenu()
    {
        DOTween.To(
            () => 0.85f,
            (alpha) => optionsOverlayCanvasGroup.alpha = alpha,
            0f,
            0.4f
        ).SetEase(Ease.OutQuad)
        .OnComplete(() =>
        {
            optionsOverlayCanvasGroup.interactable = false;
            optionsOverlayCanvasGroup.blocksRaycasts = false;
        });
        optionsBackground.DOScale(0, 0.4f).SetEase(Ease.InBack).OnComplete(() => {
            optionsBackground.gameObject.SetActive(false);
        });
    }
}
