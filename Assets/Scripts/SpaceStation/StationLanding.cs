﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StationLanding : MonoBehaviour
{
    private SpaceCarrier carrierLanding;


    public void landCarrier(SpaceCarrier carrier, Vector3 landingCorrectionPosition)
    {
        if (carrierLanding) return;
        carrierLanding = carrier;
        Vector3 stationLandingPosition = transform.position;
        stationLandingPosition.z = 0;
        carrier.initLanding(this, landingCorrectionPosition, stationLandingPosition);
    }

    public void startDelivery()
    {
        StartCoroutine(deliverContainer());
    }

    public void finishLanding()
    {
        carrierLanding.finishDeliveryProcess();
        carrierLanding = null;
    }

    IEnumerator deliverContainer()
    {
        bool hasContainer = true;
        while(hasContainer)
        {
            yield return new WaitForSeconds(1f);
            if (!carrierLanding) break;
            hasContainer = carrierLanding.removeContainer();

            if (carrierLanding.getAmountOfContainers() > 0)
            {
                GameController.Instance.deliverCargo(1);
            }
        }
        if (!carrierLanding) yield return null;
        carrierLanding.startMove();
        // The last one must be executed here because if its the last it will stop the movement of StartMove();
        GameController.Instance.deliverCargo(1);
    }
}
