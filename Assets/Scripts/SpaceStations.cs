using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class SpaceStations : Singleton<SpaceStations>
{
    public SpaceStation[] spaceStationsPrefabs;
    private List<SpaceStation> spaceStations = new List<SpaceStation>();
    private Vector3 screenSize;
    private SpaceStation selectedStation;

    private void Start()
    {
        screenSize = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0));
        createSpaceStations();
        selectedStation = spaceStations[0];
    }

    private void createSpaceStations()
    {
        for (int i = 0; i < spaceStationsPrefabs.Length; i++)
        {
            SpaceStation spaceStationPrefab = spaceStationsPrefabs[i];

            SpaceStation spaceStation = Instantiate(spaceStationPrefab, transform);
            float stationPosX = screenSize.x * 2 * i;
            spaceStation.transform.localPosition = new Vector3(stationPosX, 0, 0);
            spaceStations.Add(spaceStation);
        }
    }

    public void next()
    {
        int nextIndex = selectedIndex + 1;
        if (nextIndex > spaceStations.Count - 1) return;
        selectedStation = spaceStations[nextIndex];
        moveToSelected();
    }

    public void prev()
    {
        int prevIndex = selectedIndex - 1;
        if (prevIndex < 0) return;
        selectedStation = spaceStations[prevIndex];
        moveToSelected();
    }

    private void moveToSelected()
    {
        transform.DOMoveX(-selectedIndex * screenSize.x * 2, 0.8f)
        .SetEase(Ease.InOutQuint);
    }
    
    public SpaceStation getSelected()
    {
        return this.selectedStation;
    }

    int selectedIndex
    {
        get {
            return spaceStations.FindIndex((spaceStation) => spaceStation == selectedStation);
        }

    }
}
