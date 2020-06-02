using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MenuStationLevel : MonoBehaviour
{
    public SpaceStation spaceStation;

    private void Start()
    {
        TextMeshProUGUI title = GetComponentInChildren<TextMeshProUGUI>();
        title.SetText(spaceStation.stationName);

    }
}
