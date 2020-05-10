using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    private int currentCargos = 5;
    private void Start()
    {
        GameEvents.current.onCargosDelivered += cargosDelivered;
        GameEvents.current.onNewModuleCreated += placeModuleEnded;
    }

    private void OnDestroy()
    {
        GameEvents.current.onCargosDelivered -= cargosDelivered;
        GameEvents.current.onNewModuleCreated -= placeModuleEnded;
    }

    void cargosDelivered(int amountCargos)
    {
    }

    void spawnSequence()
    {
        GameEvents.current.startSpawnSequence();
    }

    void placeModule()
    {
        GameEvents.current.createNewModule();
    }

    void placeModuleEnded()
    {
    }
}
