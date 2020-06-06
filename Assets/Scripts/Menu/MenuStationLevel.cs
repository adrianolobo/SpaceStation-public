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

        manageStationSprite();
        manageLockedDetails();
    }

    private void manageStationSprite()
    {
        GameObject stationImageObj = transform.Find("StationImage").gameObject;
        Image stationImage = stationImageObj.GetComponent<Image>();
        stationImage.sprite = spaceStation.stationImage;
        if (isLocked)
        {
            stationImage.color = new Color(0.1f, 0.1f, 0.1f); ;
            return;
        }
        GameObject lockImg = transform.Find("Lock").gameObject;
        lockImg.SetActive(false);
    }

    private void manageLockedDetails()
    {
        if (isLocked) {
            GameObject highScoreContainer = transform.Find("HighScoreContainer").gameObject;
            highScoreContainer.SetActive(false);
            return;
        };
        GameObject toUnlockContainer = transform.Find("CargosToUnlock").gameObject;
        toUnlockContainer.SetActive(false);
    }

    private bool isLocked
    {
        get
        {
            return Storage.Instance.getTotalCargos() <= spaceStation.cargosToUnlock;
        }
    }
}
