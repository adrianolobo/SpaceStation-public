using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StationLanding : AbstractStationModule
{
    private SpaceCarrier carrierLanding;
    public void landCarrier(SpaceCarrier carrier, Vector3 landingCorrectionPosition)
    {
        if (carrierLanding) return;
        carrierLanding = carrier;
        carrier.initLanding(this, landingCorrectionPosition, transform.position);
    }

    public void carrierLanded()
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
            hasContainer = carrierLanding.removeContainer();
        }
        carrierLanding.startMove();
    }
    public override void move(Vector3 position)
    {

    }
    public override void startPlacing()
    {
        BoxCollider2D collider2d = GetComponent<BoxCollider2D>();
        collider2d.enabled = false;
    }
    public override void endPlacing()
    {
        BoxCollider2D collider2d = GetComponent<BoxCollider2D>();
        collider2d.enabled = true;
    }
}
