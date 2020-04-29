﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    private int currentCargos = 10;
    private void Start()
    {
        GameEvents.current.onCargosDelivered += cargosDelivered;
        GameEvents.current.onNewModuleCreated += placeModuleEnded;

        spawnSequence();
    }

    private void OnDestroy()
    {
        GameEvents.current.onCargosDelivered -= cargosDelivered;
        GameEvents.current.onNewModuleCreated -= placeModuleEnded;
    }

    void cargosDelivered(int amountCargos)
    {
        currentCargos -= amountCargos;
        if (currentCargos > 0) return;
        currentCargos = 10;
        placeModule();

    }

    void spawnSequence()
    {
        GameEvents.current.startSpawnSequence(currentCargos);
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