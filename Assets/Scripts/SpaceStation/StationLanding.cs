using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StationLanding : MonoBehaviour
{
    private SpaceCarrier carrierLanding;
    public Container.CARGO_COLOR[] accepts = new Container.CARGO_COLOR[2] ;
    public GameObject blueLight;
    public GameObject redLight;
    public GameObject redBlueLight;

    private void Awake()
    {
        createLights();
    }

    private void createLights()
    {
        GameObject landingLight = getCorrectLight();
        Instantiate(landingLight, transform);
        Instantiate(landingLight, transform).transform.localRotation = Quaternion.Euler(0, 0, 180);
    }

    private GameObject getCorrectLight()
    {
        if (accepts[0] != accepts[1]) return redBlueLight;
        if (accepts[0] == Container.CARGO_COLOR.BLUE) return blueLight;
        return redLight;

    }


    public void landCarrier(SpaceCarrier carrier, Vector3 landingCorrectionPosition)
    {
        if (carrierLanding) return;
        if (!canReceiveCargo(carrier)) return;
        carrierLanding = carrier;
        Vector3 stationLandingPosition = transform.position;
        stationLandingPosition.z = 0;
        carrier.initLanding(this, landingCorrectionPosition, stationLandingPosition);
    }

    private bool canReceiveCargo(SpaceCarrier carrier)
    {
        ContainerManager containerManager = carrier.getContainerManager();
        bool canReceive = false;
        for (int i = 0; i < accepts.Length; i++)
        {
            if (accepts[i] == Container.CARGO_COLOR.RED)
            {
                if (containerManager.hasRedContainer()) {
                    canReceive = true;
                };
                continue;

            }
            if (containerManager.hasBlueContainer())
            {
                canReceive = true;
            };
        }
        Debug.Log(canReceive);
        return canReceive;
    }

    public void startDelivery()
    {
        StartCoroutine(deliverContainer(accepts));
    }

    public void finishLanding()
    {
        carrierLanding.finishDeliveryProcess();
        carrierLanding = null;
    }

    IEnumerator deliverContainer(Container.CARGO_COLOR[] accepts)
    {
        bool hasContainer = true;
        while(hasContainer)
        {
            yield return new WaitForSeconds(1f);
            if (!carrierLanding) break;
            hasContainer = carrierLanding.removeContainer(accepts);

            GameController.Instance.deliverCargo(1);
        }
        if (!carrierLanding) yield return null;
        carrierLanding.startMove();
    }
}
