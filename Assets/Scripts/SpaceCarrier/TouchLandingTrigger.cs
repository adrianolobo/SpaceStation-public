using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchLandingTrigger : MonoBehaviour
{
    private SpaceCarrier spaceCarrier;
    public void register(SpaceCarrier spaceCarrier)
    {
        this.spaceCarrier = spaceCarrier;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        LandingTrigger landingTrigger = collision.gameObject.GetComponent<LandingTrigger>();
        if (!landingTrigger) return;
        if (!landingTrigger.checkLandingTrigger(spaceCarrier)) return;
        spaceCarrier.getPathLine().createLandingLine(
            landingTrigger.getLandingCorrectionPosition(),
            landingTrigger.getStationLandingPosition()
        );
    }
}
