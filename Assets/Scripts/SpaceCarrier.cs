using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceCarrier : MonoBehaviour
{

    private PathLine pathLine;
    Rigidbody2D rigidBody;
    private StationLanding stationLanding;
    private ContainerManager containerManager;
    private Engine engine;
    private ProximityRadar proximityRadar;

    private float initialCarrierVelocity = 0.5f;
    private float carrierVelocity;
    private bool isLanded = false;
    public int amountContainers = 1;

    private string landingLayer = "SpaceCarrierLanding";
    private string deliveredLayer = "SpaceCarrierDelivered";
    void Start()
    {
        containerManager = GetComponentInChildren<ContainerManager>();
        containerManager.createContainers(amountContainers);

        engine = GetComponentInChildren<Engine>();
        engine.fire();

        proximityRadar = GetComponentInChildren<ProximityRadar>();

        carrierVelocity = initialCarrierVelocity;
        pathLine = GetComponent<PathLine>();
        rigidBody = GetComponent<Rigidbody2D>();
        rigidBody.velocity = new Vector2(0, 0);
    }

    void Update()
    {
        pathLine.drawLine();
        setAngle();
        move();
        pathLine.updateLine();
    }

    void setAngle()
    {
        if (pathLine.positionCount < 1) return;
        Vector2 direction = pathLine.getPosition(0) - currentPosition;
        float angleBetween = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angleBetween, Vector3.forward);
    }
    void move()
    {
        rigidBody.velocity = transform.right * carrierVelocity;
    }

    public void startMove()
    {
        carrierVelocity = initialCarrierVelocity;
        engine.fire();
    }

    public void lineEnded()
    {
        if (!stationLanding) return;
        if (isLanded) return;
        engine.stop();
        isLanded = true;
        carrierVelocity = 0f;
        stationLanding.carrierLanded();
    }

    public Vector3 currentPosition
    {
        get {
            Vector3 position = transform.position;
            position.z = 0;
            return position;
        }
    }

    public void initLanding(StationLanding stationToLand, Vector3 landCorrectionPosition, Vector3 targetPosition)
    {
        stationLanding = stationToLand;
        gameObject.layer = LayerMask.NameToLayer(landingLayer);
        pathLine.createLandingLine(landCorrectionPosition, targetPosition);
    }

    public void finishDeliveryProcess()
    {
        gameObject.layer = LayerMask.NameToLayer(deliveredLayer);
    }

    public bool removeContainer()
    {
        return containerManager.removeContainer();
    }

    public bool isInDeliveryProcess
    {
        get {
            return LayerMask.LayerToName(gameObject.layer) == landingLayer;
        }
    }
}
