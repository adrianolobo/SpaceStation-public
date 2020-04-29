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
    private bool hasAlreadyEnteredScreen = false;

    private bool isLanded = false;
    private int amountContainers = 1;
    private float carrierVelocity = 0f;
    private float[] velocityByContainers = new float[] { 1, 0.8f, 0.5f, 0.3f };

    private string landingLayer = "SpaceCarrierLanding";
    private string deliveredLayer = "SpaceCarrierDelivered";
    void Awake()
    {
        containerManager = GetComponentInChildren<ContainerManager>();

        engine = GetComponentInChildren<Engine>();

        proximityRadar = GetComponentInChildren<ProximityRadar>();

        pathLine = GetComponent<PathLine>();
        rigidBody = GetComponent<Rigidbody2D>();
        rigidBody.velocity = new Vector2(0, 0);
        turnToCenter();
    }

    public void createContainers(int amountContainersToCreate)
    {
        amountContainers = amountContainersToCreate;
        carrierVelocity = velocityByContainers[amountContainers];
        containerManager.createContainers(amountContainers);
    }

    void Update()
    {
        pathLine.drawLine();
        setAngle();
        move();
        pathLine.updateLine();
    }

    void turnToCenter()
    {
        Vector2 direction = new Vector3(0, 0, 0) - currentPosition;
        float angleBetween = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angleBetween, Vector3.forward);
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
        carrierVelocity = velocityByContainers[0];
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

    public bool getHasAlreadyEnteredScreen()
    {
        return hasAlreadyEnteredScreen;
    }

    public int getAmountOfContainers()
    {
        return amountContainers;
    }

    public void enteredScreen()
    {
        hasAlreadyEnteredScreen = true;
    }

    public bool hasDelivered
    {
        get
        {
            return LayerMask.LayerToName(gameObject.layer) == deliveredLayer;
        }
    } 

    public bool isInDeliveryProcess
    {
        get {
            return LayerMask.LayerToName(gameObject.layer) == landingLayer;
        }
    }
}
