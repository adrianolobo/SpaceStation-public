using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TotalCargoCount : MonoBehaviour
{
    void Start()
    {
        TextMeshProUGUI textMP = GetComponent<TextMeshProUGUI>();
        textMP.SetText(Storage.Instance.getTotalCargos().ToString());
    }

}
