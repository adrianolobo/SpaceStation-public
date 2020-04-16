using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StationLanding : MonoBehaviour
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
        StartCoroutine(deliverCargo());
    }

    public void finishLanding()
    {
        carrierLanding.finishDeliveryProcess();
        carrierLanding = null;
    }

    IEnumerator deliverCargo()
    {
        yield return new WaitForSeconds(1f);
        carrierLanding.startMove();
    }
}
