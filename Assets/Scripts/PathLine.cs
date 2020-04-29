using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathLine : MonoBehaviour
{

    private LineRenderer pathLine;
    private SpaceCarrier spaceCarrier;

    bool isCreatingPath = false;
    Vector3 lastMousePosition;

    public float minimalLineChunck = 0.15f;
    public float distanceToRemoveChunck = 0.08f;
    void Start()
    {
        lastMousePosition = getMousePosition();
        pathLine = GetComponent<LineRenderer>();
        pathLine.positionCount = 0;
        spaceCarrier = GetComponent<SpaceCarrier>();
    }

    private void Update()
    {
        if (Input.GetMouseButtonUp(0))
        {
            isCreatingPath = false;
            return;
        }
    }

    private void OnMouseDown()
    {
        if (spaceCarrier.isInDeliveryProcess)
        {
            isCreatingPath = false;
            return;
        };
        isCreatingPath = true;
        pathLine.positionCount = 1;
        pathLine.SetPosition(0, getMousePosition());
    }

    private bool hasMouseMoved()
    {
        if (getMousePosition() == lastMousePosition) return false;
        lastMousePosition = getMousePosition();
        return true;
    }

    public void drawLine()
    {
        if (!spaceCarrier)
        {
            Debug.Log("BUG");
            return;
        }
        if (spaceCarrier.isInDeliveryProcess) return;
        if (!isCreatingPath) return;
        if (!hasMouseMoved()) return;
        float distanceToMouse = calculateDistanceToMouse();
        if (distanceToMouse < minimalLineChunck) return;
        createLineChunks(getMousePosition());
    }

    private void createLineChunks(Vector3 targetPosition)
    {
        Vector3 lastLinePoint = getMouseOrCarrierPosition();
        float distanceToTarget = Vector3.Distance(lastLinePoint, targetPosition);
        int amountChuncksToAdd = Mathf.FloorToInt(distanceToTarget / minimalLineChunck);
        Vector3 direction = targetPosition - lastLinePoint;
        float angleBetweenRad = Mathf.Atan2(direction.y, direction.x);
        for (int i = 0; i < amountChuncksToAdd; i++)
        {
            float scalarSize = (i + 1) * minimalLineChunck;
            float pointX = lastLinePoint.x + (Mathf.Cos(angleBetweenRad) * scalarSize);
            float pointY = lastLinePoint.y + (Mathf.Sin(angleBetweenRad) * scalarSize);
            Vector3 newPoint = new Vector3(pointX, pointY, 0);
            addPathPoint(newPoint);
        }
        addPathPoint(targetPosition);
    }

    private void addPathPoint(Vector3 newPoint)
    {
        int newIndex = pathLine.positionCount;
        pathLine.positionCount++;
        pathLine.SetPosition(newIndex, newPoint);
    }

    private Vector3 getMouseOrCarrierPosition()
    {
        if (pathLine.positionCount > 0)
        {
            return pathLine.GetPosition(pathLine.positionCount - 1);
        }
        return spaceCarrier.currentPosition;
    }

    float calculateDistanceToMouse()
    {
        Vector3 lastPosition = getMouseOrCarrierPosition();
        Vector3 mousePosition = getMousePosition();
        return Vector3.Distance(lastPosition, mousePosition);
    }

    private Vector3 getMousePosition()
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0;
        return mousePosition;
    }


    public void updateLine()
    {
        if (pathLine.positionCount == 0)
        {
            spaceCarrier.lineEnded();
            return;
        };
        float distance = Vector3.Distance(spaceCarrier.currentPosition, pathLine.GetPosition(0));
        if (distance > distanceToRemoveChunck) return;
        Vector3[] positions = new Vector3[pathLine.positionCount];
        pathLine.GetPositions(positions);
        List<Vector3> positionsList = new List<Vector3>(positions);
        positionsList.RemoveAt(0);
        Vector3[] positionsArray = positionsList.ToArray();
        pathLine.positionCount = positionsArray.Length;
        pathLine.SetPositions(positionsArray);
    }

    public Vector3 getPosition(int position)
    {
        return pathLine.GetPosition(position);
    }

    public int positionCount
    {
        get
        {
            return pathLine.positionCount;
        }
    }

    public void createLandingLine(Vector3 landCorrectionPosition, Vector3 position)
    {
        pathLine.positionCount = 0;
        createLineChunks(landCorrectionPosition);
        createLineChunks(position);
    }
}
