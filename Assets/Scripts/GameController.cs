using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    private void Start()
    {
        Debug.Log("Game Controller");
        GameEvents.current.onEndSpawnSequence += spawnSequenceEnded;
        GameEvents.current.onNewModuleCreated += placeModuleEnded;

        spawnSequence();
    }

    private void OnDestroy()
    {
        GameEvents.current.onEndSpawnSequence -= spawnSequenceEnded;
        GameEvents.current.onNewModuleCreated -= placeModuleEnded;
    }

    void spawnSequence()
    {
        GameEvents.current.startSpawnSequence(10);
    }

    void spawnSequenceEnded()
    {
        placeModule();   
    }

    void placeModule()
    {
        GameEvents.current.createNewModule();
    }

    void placeModuleEnded()
    {
        spawnSequence();
    }
}
