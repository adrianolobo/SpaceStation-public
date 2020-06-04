using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CountCargosToUnlock : MonoBehaviour
{
    void Awake()
    {
        SpaceStation spaceStation = GetComponentInParent<MenuStationLevel>().spaceStation;
        TextMeshProUGUI cargosCount = GetComponent<TextMeshProUGUI>();
        int totalCargosToUnlock = spaceStation.cargosToUnlock - Storage.Instance.getTotalCargos();
        cargosCount.SetText(totalCargosToUnlock.ToString());
    }
}
