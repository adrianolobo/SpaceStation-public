using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathLine : MonoBehaviour
{

    private LineRenderer pathLine;
    private SpaceCarrier spaceCarrier;

    bool isCreatingPath = false;

    public float minimalLineChunck = 0.15f;
    public float distanceToRemoveChunck = 0.08f;
    void Start()
    {
        pathLine = GetComponent<LineRenderer>();
        pathLine.positionCount = 0;
        spaceCarrier = GetComponent<SpaceCarrier>();
    }

    private void Update()
    {
        if (Input.GetMouseButtonUp(0))
        {
            isCreatingPath = false;
        }
    }

    private void OnMouseDown()
    {
        isCreatingPath = true;
        pathLine.positionCount = 1;
        pathLine.SetPosition(0, getMousePosition());
    }

    public void drawLine()
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
        // COLOCAR ESSA lÒGICA GENERICA E VER SE ARRUMA O BUG
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


    public void updateLine()
    {
        Debug.Log(pathLine.positionCount);
        if (pathLine.positionCount == 0) return;
        float distance = Vector3.Distance(spaceCarrier.currentPosition, pathLine.GetPosition(0));
        Debug.Log("distance");
        Debug.Log(distance);
        if (distance > distanceToRemoveChunck) return;
        Debug.Log("TROEEE");
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
}
