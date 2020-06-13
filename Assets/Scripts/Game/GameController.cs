using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GameController : Singleton<GameController>
{
    private enum STATE { START, PLAYING, GAME_OVER };
    public List<SpaceStation> spaceStations = new List<SpaceStation>();
    public SpaceStation defaultSpaceStation;
    private SpaceStation spaceStation;
    private int currentScore = 0;
    private GameCargoCount gameCargoCount;
    private bool isGamePaused = false;
    STATE gameState;
    private void Start()
    {
        gameCargoCount = GameObject.Find("GameCargoCount").GetComponent<GameCargoCount>();
        gameState = STATE.START;
        play();

        GameEvents.Instance.onOpenConfigMenu += () => isGamePaused = true;
        GameEvents.Instance.onCloseConfigMenu += () => isGamePaused = false;
    }

    public async void gameOver(Vector3 colliderPosition)
    {
        if (isGameOver) return;
        gameState = STATE.GAME_OVER;
        SpawnManager.Instance.stop();
        CollisionExplosions.Instance.explode(colliderPosition);
        await new WaitForSeconds(5f);
        GameOverMenu.Instance.openGameOverMenu();
    }

    public void play()
    {
        if (isPlaying) return;
        instantiateStation();
        gameState = STATE.PLAYING;
        SpawnManager.Instance.startSpawnSequence();
    }

    public void deliverCargo(int cargoAmount)
    {
        int totalCargos = Storage.Instance.getTotalCargos();
        Storage.Instance.setTotalCargos(totalCargos + cargoAmount);
        currentScore += 1;
        gameCargoCount.setCount(currentScore.ToString());
        Storage.Instance.setHighScore(currentScore, spaceStation.name);
    }

    private void instantiateStation()
    {
        string stationName = SceneLoader.Instance.getStationNameToLoad();

        spaceStation = spaceStations.Find((spaceStationItem) => spaceStationItem.name == stationName);
        if (spaceStation == null)
        {
            spaceStation = defaultSpaceStation;
        }
        Instantiate(spaceStation);
    }

    public bool getIsGamePaused()
    {
        return isGamePaused;
    }

    public bool isPlaying { get { return gameState == STATE.PLAYING; } }
    public bool isGameOver { get { return gameState == STATE.GAME_OVER; } }
}
