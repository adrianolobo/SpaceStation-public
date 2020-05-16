using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StationLanding : AbstractStationModule
{
    private SpaceCarrier carrierLanding;

    private Coroutine deliverContainerCoroutine = null;

    private void Start()
    {
        GameEvents.current.onNewModuleCreated += startDelivery;
    }

    private void stopDelivery()
    {
        if (deliverContainerCoroutine == null) return;
        StopCoroutine(deliverContainerCoroutine);
    }

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
        deliverContainerCoroutine = StartCoroutine(deliverContainer());
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
                GameEvents.current.cargosDelivered(1);
            }
        }
        if (!carrierLanding) yield return null;
        carrierLanding.startMove();
        deliverContainerCoroutine = null;
        // The last one must be executed here because if its the last it will stop the movement of StartMove();
        GameEvents.current.cargosDelivered(1);
    }
}
