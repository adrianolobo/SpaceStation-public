using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceCarrier : MonoBehaviour
{

    private PathLine pathLine;
    Rigidbody2D rigidBody;

    private float carrierVelocity = 40f;
    void Start()
    {
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
        if (pathLine.positionCount < 1) return;
        Vector2 direction = pathLine.getPosition(0) - currentPosition;
        float angleBetweenRad = Mathf.Atan2(direction.y, direction.x);
        rigidBody.velocity = new Vector2(
            Mathf.Cos(angleBetweenRad) * (carrierVelocity * Time.deltaTime),
            Mathf.Sin(angleBetweenRad) * (carrierVelocity * Time.deltaTime)
        );
    }

    public Vector3 currentPosition
    {
        get {
            Vector3 position = transform.position;
            position.z = 0;
            return position;
        }
    }

    public void land(Vector3 landCorrectionPosition, Vector3 targetPosition)
    {
        pathLine.createLandingLine(landCorrectionPosition, targetPosition);
    }
}
