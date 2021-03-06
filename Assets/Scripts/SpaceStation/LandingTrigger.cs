﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LandingTrigger : MonoBehaviour
{
    private StationLanding stationLanding;
    void Start()
    {
        stationLanding = GetComponentInParent<StationLanding>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        SpaceCarrier spaceCarrier = collision.gameObject.GetComponent<SpaceCarrier>();
        if (!spaceCarrier) return;

        stationLanding.landCarrier(spaceCarrier, getLandingCorrectionPosition());
    }

    public bool checkLandingTrigger(SpaceCarrier spaceCarrierToCheck)
    {
        return stationLanding.canReceiveCargo(spaceCarrierToCheck);
    }

    public Vector3 getLandingCorrectionPosition()
    {
        Vector3 landingCorrectionPosition = transform.position;
        landingCorrectionPosition.z = 0;
        return landingCorrectionPosition;
    }

    public Vector3 getStationLandingPosition()
    {
        return stationLanding.getStationLandingPosition();
    }
}
