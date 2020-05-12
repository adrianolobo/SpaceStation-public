using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.UIElements.Runtime;
using UnityEngine.UIElements;
using DG.Tweening;

public class UI : MonoBehaviour
{
    private PanelRenderer ui;
    private Button playBtn;
    private VisualElement startScreen;

    private void Awake()
    {
        ui = GetComponent<PanelRenderer>();
        ui.postUxmlReload = BindUI;
    }

    private IEnumerable<Object> BindUI()
    {
        var root = ui.visualTree;
        playBtn = root.Q<Button>("play-btn");
        startScreen = root.Q<VisualElement>("start-screen");
        playBtn.clickable.clicked += () =>
        {
            GameEvents.current.playBtnClicked();
            startScreenDisapear();
        };
        return null;
    }

    private void startScreenDisapear()
    {
        DOTween.To(
            () => 1f,
            opacity => startScreen.style.opacity = opacity,
            0f,
            1f
        ).SetEase(Ease.OutQuad);
    }
}
