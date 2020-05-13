using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    bool isPlaying = false;
    private void Start()
    {
        GameEvents.current.onPlayBtnClicked += spawnSequence;
    }

    private void OnDestroy()
    {
        GameEvents.current.onPlayBtnClicked -= spawnSequence;
    }

    void spawnSequence()
    {
        if (isPlaying) return;
        isPlaying = true;
        GameEvents.current.startSpawnSequence();
    }
}
