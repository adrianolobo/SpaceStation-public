using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameCargoCount : MonoBehaviour
{
    TextMeshProUGUI countText;
    void Awake()
    {
        countText = GetComponent<TextMeshProUGUI>();
        countText.SetText("0");
    }

    public void setCount(string cargoNumber)
    {
        countText.SetText(cargoNumber);
    }
}
