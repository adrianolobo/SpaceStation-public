using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// **********************
// TODO refatorar criando uma classe apenas para o pathLine!!!!!!!!!!!!
public class SpaceCarrier : MonoBehaviour
{

    private LineRenderer pathLine;
    bool isCreatingPath = false;
    Rigidbody2D rigidBody;

    public float minimalLineChunck = 0.15f;
    public float distanceToRemoveChunck = 0.08f;
    public float carrierVelocity = 1.5f;
    void Start()
    {
        pathLine = GetComponent<LineRenderer>();
        pathLine.positionCount = 0;
        rigidBody = GetComponent<Rigidbody2D>();
        rigidBody.velocity = new Vector2(0, 0);
    }

    // Update is called once per frame
    void Update()
    {
        drawLine();
        setAngle();
        move();
        updateLine();
    }

    void updateLine()
    {
        if (pathLine.positionCount == 0) return;
        float distance = Vector3.Distance(currentPosition, pathLine.GetPosition(0));
        if (distance > distanceToRemoveChunck) return;
        Vector3[] positions = new Vector3[pathLine.positionCount];
        pathLine.GetPositions(positions);
        List<Vector3> positionsList = new List<Vector3>(positions);
        positionsList.RemoveAt(0);
        pathLine.SetPositions(positionsList.ToArray());

    }

    void setAngle()
    {
        if (pathLine.positionCount < 1) return;
        Vector2 direction = pathLine.GetPosition(0) - currentPosition;
        float angleBetween = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angleBetween, Vector3.forward);
    }
    void move()
    {
        if (pathLine.positionCount < 1) return;
        Vector2 direction = pathLine.GetPosition(0) - currentPosition;
        float angleBetweenRad = Mathf.Atan2(direction.y, direction.x);
        rigidBody.velocity = new Vector2(
            Mathf.Cos(angleBetweenRad) * carrierVelocity,
            Mathf.Sin(angleBetweenRad) * carrierVelocity
        );
    }

    void drawLine()
    {
        if (!isCreatingPath) return;
        float distanceToMouse = calculateDistanceToMouse();
        if (distanceToMouse < minimalLineChunck) return;
        if (pathLine.positionCount == 0)
        {
            pathLine.positionCount = 1;
            pathLine.SetPosition(0, getMousePosition());
            return;
        }
        int amountChuncksToAdd = Mathf.FloorToInt(distanceToMouse / minimalLineChunck);
        Vector3 lastLinePoint = pathLine.GetPosition(pathLine.positionCount - 1);
        Vector3 direction = getMousePosition() - lastLinePoint;
        float angleBetweenRad = Mathf.Atan2(direction.y, direction.x);
        for (int i = 0; i < amountChuncksToAdd; i++)
        {
            float scalarSize = (i + 1) * minimalLineChunck;
            float pointX = lastLinePoint.x + (Mathf.Cos(angleBetweenRad) * scalarSize);
            float pointY = lastLinePoint.y + (Mathf.Sin(angleBetweenRad) * scalarSize);
            Vector3 newPoint = new Vector3(pointX, pointY, 0);
            int newIndex = pathLine.positionCount;
            pathLine.positionCount++;
            pathLine.SetPosition(newIndex, newPoint);
        }
    }

    float calculateDistanceToMouse()
    {
        if (pathLine.positionCount < 1) return 0;
        Vector3 lastPosition = pathLine.GetPosition(pathLine.positionCount - 1);
        Vector3 mousePosition = getMousePosition();
        return Vector3.Distance(lastPosition, mousePosition);
    }

    private Vector3 getMousePosition() 
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0;
        return mousePosition;
    }

    private void OnMouseDown()
    {
        isCreatingPath = true;
        pathLine.positionCount = 1;
        pathLine.SetPosition(0, getMousePosition());
    }

    private Vector3 currentPosition
    {
        get {
            Vector3 position = transform.position;
            position.z = 0;
            return position;
        }
    }
}
