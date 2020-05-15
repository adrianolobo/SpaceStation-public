using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GameController : Singleton<GameController>
{
    private enum STATE { START, PLAYING, GAME_OVER };
    STATE gameState;
    private void Start()
    {
        gameState = STATE.START;
    }

    public void gameOver()
    {
        if (isGameOver) return;
        gameState = STATE.GAME_OVER;
        SpaceCarrierManager.Instance.destroyAll();
    }

    public void play()
    {
        if (isPlaying) return;
        gameState = STATE.PLAYING;
        SpawnManager.Instance.startSpawnSequence();
        GameEvents.current.startSpawnSequence();
    }

    public bool isPlaying { get { return gameState == STATE.PLAYING; } }
    public bool isGameOver { get { return gameState == STATE.GAME_OVER; } }
}
