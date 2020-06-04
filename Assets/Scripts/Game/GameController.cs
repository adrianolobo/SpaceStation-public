using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GameController : Singleton<GameController>
{
    private enum STATE { START, PLAYING, GAME_OVER };
    public List<SpaceStation> spaceStations = new List<SpaceStation>();
    STATE gameState;
    private void Start()
    {
        gameState = STATE.START;
        play();
    }

    public void gameOver()
    {
        if (isGameOver) return;
        gameState = STATE.GAME_OVER;
        SpawnManager.Instance.stop();

        SpaceCarrierManager.Instance.destroyAll();
        SceneLoader.Instance.goToMenu();
    }

    public void play()
    {
        if (isPlaying) return;
        instantiateStation();
        gameState = STATE.PLAYING;
        SpawnManager.Instance.startSpawnSequence();
        GameEvents.current.startSpawnSequence();
    }

    public void deliverCargo(int cargoAmount)
    {
        int totalCargos = Storage.Instance.getTotalCargos();
        Storage.Instance.setTotalCargos(totalCargos + cargoAmount);
    }

    private void instantiateStation()
    {
        string stationName = SceneLoader.Instance.getStationNameToLoad();
        SpaceStation spaceStation = spaceStations.Find((spaceStationItem) => spaceStationItem.name == stationName);
        Instantiate(spaceStation);
    }

    public bool isPlaying { get { return gameState == STATE.PLAYING; } }
    public bool isGameOver { get { return gameState == STATE.GAME_OVER; } }
}
