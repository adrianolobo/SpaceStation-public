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
    private bool inDeliveryProcess = false;
    private float deliveryTime = 1f;
    private float deliveryCountdown = 0f;

    private void Awake()
    {
        createLights();
    }

    private void Update()
    {
        deliverContainer();
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

        carrier.initLanding(this, landingCorrectionPosition, getStationLandingPosition());
    }

    public Vector3 getStationLandingPosition()
    {
        Vector3 stationLandingPosition = transform.position;
        stationLandingPosition.z = 0;
        return stationLandingPosition;
    }

    public bool canReceiveCargo(SpaceCarrier carrier)
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
        return canReceive;
    }

    public void startDelivery()
    {
        inDeliveryProcess = true;
        deliveryCountdown = deliveryTime;
    }

    public void finishLanding()
    {
        carrierLanding.finishDeliveryProcess();
        carrierLanding = null;
    }

    private void deliverContainer()
    {
        if (!carrierLanding) return;
        if (!inDeliveryProcess) return;
        if (GameController.Instance.isGameOver) return;
        if (GameController.Instance.getIsGamePaused()) return;
        deliveryCountdown -= Time.deltaTime;
        if (deliveryCountdown > 0) return;

        bool hasContainer = carrierLanding.removeContainer(accepts);
        if (!hasContainer)
        {
            carrierLanding.startMove();
            inDeliveryProcess = false;
            return;
        }
        GameController.Instance.deliverCargo(1);
        deliveryCountdown = deliveryTime;
    }
}
