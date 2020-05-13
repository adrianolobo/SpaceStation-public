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
    private Button nextBtn;
    private Button prevBtn;
    private VisualElement startScreen;
    private bool isScreenDisappearing = false;
    private SpaceStations spaceStations;

    private void Awake()
    {
        spaceStations = GameObject.Find("SpaceStations").GetComponent<SpaceStations>();
        ui = GetComponent<PanelRenderer>();
        ui.postUxmlReload = BindUI;
    }

    private IEnumerable<Object> BindUI()
    {
        var root = ui.visualTree;
        startScreen = root.Q<VisualElement>("start-screen");
        playBtn = root.Q<Button>("play-btn");
        nextBtn = root.Q<Button>("next-btn");
        prevBtn = root.Q<Button>("prev-btn");

        playBtn.clickable.clicked += () =>
        {
            GameEvents.current.playBtnClicked();
            startScreenDisapear();
        };

        nextBtn.clickable.clicked += () =>
        {
            spaceStations.next();
        };
        prevBtn.clickable.clicked += () =>
        {
            spaceStations.prev();
        };
        return null;
    }

    private void startScreenDisapear()
    {
        if (isScreenDisappearing) return;
        isScreenDisappearing = true;
        DOTween.To(
            () => 1f,
            opacity => startScreen.style.opacity = opacity,
            0f,
            1f
        ).SetEase(Ease.OutQuad)
        .OnComplete(() => {
            startScreen.AddToClassList("start-screen_disabled");
            isScreenDisappearing = false;
        });
    }
}
