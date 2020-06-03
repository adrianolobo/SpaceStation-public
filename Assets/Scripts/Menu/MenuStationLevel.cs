using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class MenuStationLevel : MonoBehaviour
{
    public SpaceStation spaceStation;

    private void Start()
    {
        TextMeshProUGUI title = GetComponentInChildren<TextMeshProUGUI>();
        title.SetText(spaceStation.stationName);

        GameObject stationImageObj = transform.Find("StationImage").gameObject;
        Image stationImage = stationImageObj.GetComponent<Image>();
        stationImage.sprite = spaceStation.stationImage;
    }
}
