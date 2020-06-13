using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProximityRadar : MonoBehaviour
{
    private int countNearObjects = 0;
    private SpriteRenderer proximitySrpite;
    private SpaceCarrier spaceCarrier;

    private void Start()
    {
        proximitySrpite = GetComponent<SpriteRenderer>();
        spaceCarrier = GetComponentInParent<SpaceCarrier>();
        disableRadar();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        countNearObjects++;
        manageWarning();
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        countNearObjects--;
        manageWarning();
    }

    private void manageWarning()
    {
        if (spaceCarrier.isInDeliveryProcess)
        {
            disableRadar();
            return;
        }
        if (countNearObjects > 0)
        {
            activateRadar();
            return;
        }
        disableRadar();
    }
    private void activateRadar()
    {
        changeOpacity(0.5f);
    }

    private void disableRadar()
    {
        changeOpacity(0);
    }

    private void changeOpacity(float opacity)
    {
        Color materialColor = proximitySrpite.material.color;
        materialColor.a = opacity;
        proximitySrpite.material.color = materialColor;
    }
}
