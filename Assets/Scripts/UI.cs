using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.UIElements.Runtime;
using UnityEngine.UIElements;
using DG.Tweening;
using System.Threading.Tasks;

public class UI : Singleton<UI>
{
    private PanelRenderer ui;
    private Button playBtn;
    private Button nextBtn;
    private Button prevBtn;
    private VisualElement gameOverScreen;
    private Label gameOverLabel;
    private Box blackOverlay;
    private VisualElement startScreen;
    private bool isStartScreenAnimating = false;

    private void Awake()
    {
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
        gameOverScreen = root.Q<VisualElement>("game-over-screen");
        gameOverLabel = root.Q<Label>("game-over-label");
        blackOverlay = root.Q<Box>("black-overlay");

        setDisplayNone(gameOverScreen);
        setDisplayFlex(startScreen);

        playBtn.clickable.clicked += () =>
        {
            GameController.Instance.play();
            startScreenDisapear();
        };

        nextBtn.clickable.clicked += () =>
        {
            SpaceStations.Instance.next();
        };
        prevBtn.clickable.clicked += () =>
        {
            SpaceStations.Instance.prev();
        };
        return null;
    }

    private void startScreenDisapear()
    {
        if (isStartScreenAnimating) return;
        isStartScreenAnimating = true;
        startScreen.style.opacity = 1;
        setDisplayFlex(startScreen);
        DOTween.To(
            () => 1f,
            opacity => startScreen.style.opacity = opacity,
            0f,
            1f
        ).SetEase(Ease.OutQuad)
        .OnComplete(() => {
            setDisplayNone(startScreen);
            isStartScreenAnimating = false;
        });
    }
    public void startScreenAppear()
    {
        if (isStartScreenAnimating) return;
        setDisplayFlex(startScreen);
        isStartScreenAnimating = true;
        startScreen.style.opacity = 1;
        DOTween.To(
            () => 0f,
            opacity => startScreen.style.opacity = opacity,
            1f,
            1f
        ).SetEase(Ease.OutQuad)
        .OnComplete(() => {
            isStartScreenAnimating = false;
        });
    }

    public async Task showGameOver()
    {
        setDisplayFlex(gameOverScreen);
        setDisplayFlex(blackOverlay);
        setDisplayFlex(gameOverLabel);
        gameOverLabel.style.opacity = 0f;
        blackOverlay.style.opacity = 0f;
        DOTween.To(() => 0f, opacity => gameOverLabel.style.opacity = opacity, 1f, 7f).SetUpdate(UpdateType.Late).SetEase(Ease.OutQuad);
        await DOTween.To(() => 0f, opacity => blackOverlay.style.opacity = opacity, 1f, 5f)
            .SetUpdate(UpdateType.Late)
            .SetDelay(4f)
            .SetEase(Ease.OutQuad)
            .AsyncWaitForCompletion();
        return;
    }

    public async Task hideGameOver()
    {
        gameOverLabel.style.opacity = 1f;
        blackOverlay.style.opacity = 1f;

        setDisplayFlex(gameOverScreen);
        DOTween.To(() => 1f, opacity => gameOverLabel.style.opacity = opacity, 0f, 1f)
            .SetUpdate(UpdateType.Late)
            .SetEase(Ease.OutQuad);
        await DOTween.To(() => 1f, opacity => blackOverlay.style.opacity = opacity, 0.01f, 1f)
            .SetEase(Ease.OutQuad)
            .SetUpdate(UpdateType.Late)
            .OnComplete(() => {
                setDisplayNone(gameOverScreen);
                gameOverLabel.style.opacity = 0f;
                blackOverlay.style.opacity = 0f;
            })
            .AsyncWaitForCompletion();
        return;
    }

    private void setDisplayNone(VisualElement element)
    {
        element.style.display = DisplayStyle.None;
    }
    private void setDisplayFlex(VisualElement element)
    {
        element.style.display = DisplayStyle.Flex;
    }
}
