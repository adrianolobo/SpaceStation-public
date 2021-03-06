﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceStation : Singleton<SpaceStation>
{
    public float[] spawnChances;
    public float cargoPercentage1;
    public float cargoPercentage2;
    public string stationName;
    public Sprite stationImage;
    public int cargosToUnlock = 0;
    public int cargoRedPercentage = 100;
    public bool allContainersSameColor = true;
}
