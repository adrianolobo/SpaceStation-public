using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    private int currentCargos = 5;
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
        GameEvents.current.startSpawnSequence();
    }
}
