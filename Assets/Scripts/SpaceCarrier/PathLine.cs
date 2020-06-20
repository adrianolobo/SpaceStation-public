using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathLine : MonoBehaviour
{

    private LineRenderer pathLine;
    private SpaceCarrier spaceCarrier;
    public GameObject TouchLandingTriggerPrefab;
    private TouchLandingTrigger touchLandingTrigger;
    public Gradient defaultColor;
    public Gradient landingColor;
    bool isCreatingPath = false;
    Vector3 lastTouchPosition;
    private Sound onTrackAudio;

    public float minimalLineChunck = 0.02f;
    public float distanceToRemoveChunck = 0.08f;
    void Start()
    {
        lastTouchPosition = new Vector3(0, 0, 0);
        pathLine = GetComponent<LineRenderer>();
        pathLine.positionCount = 0;
        spaceCarrier = GetComponent<SpaceCarrier>();
        onTrackAudio = SoundManager.Instance.getSound(Sounds.SOUND.ON_TRACK);
    }

    private void Update()
    {
        #if UNITY_EDITOR
        drawLine(getMousePosition());
        if (Input.GetMouseButtonUp(0))
        {
            touchEnded();
        }
        #endif
    }

    private void OnMouseDown()
    {
        #if UNITY_EDITOR
        touchBegan(getMousePosition());
        #endif
    }

    public void touchBegan(Vector3 touchPosition)
    {
        if (GameController.Instance.isGameOver) return;
        if (spaceCarrier.isInDeliveryProcess)
        {
            touchEnded();
            return;
        };
        setDefaultColor();
        touchLandingTrigger = Instantiate(TouchLandingTriggerPrefab, touchPosition, Quaternion.identity).GetComponent<TouchLandingTrigger>();
        touchLandingTrigger.register(spaceCarrier);
        isCreatingPath = true;
        pathLine.positionCount = 1;
        pathLine.SetPosition(0, touchPosition);
    }

    public void touchEnded()
    {
        isCreatingPath = false;
        if (!touchLandingTrigger) return;
        Destroy(touchLandingTrigger.gameObject);
        touchLandingTrigger = null;
    }

    private bool hasTouchMoved(Vector3 touchPosition)
    {
        if (touchPosition == lastTouchPosition) return false;
        lastTouchPosition = touchPosition;
        return true;
    }

    public void drawLine(Vector3 touchPosition)
    {
        if (GameController.Instance.isGameOver) return;
        if (!spaceCarrier)
        {
            Debug.Log("BUG");
            return;
        }
        if (spaceCarrier.isInDeliveryProcess) return;
        if (!isCreatingPath) return;
        if (!hasTouchMoved(touchPosition)) return;
        touchLandingTrigger.transform.localPosition = touchPosition;
        float distanceToTouch = calculateDistanceToTouch(touchPosition);
        if (distanceToTouch < minimalLineChunck) return;
        createLineChunks(touchPosition);
    }

    private void createLineChunks(Vector3 targetPosition)
    {
        Vector3 lastLinePoint = getLastOrCarrierPosition();
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

    private Vector3 getLastOrCarrierPosition()
    {
        if (pathLine.positionCount > 0)
        {
            return pathLine.GetPosition(pathLine.positionCount - 1);
        }
        return spaceCarrier.currentPosition;
    }

    float calculateDistanceToTouch(Vector3 touchPosition)
    {
        Vector3 lastPosition = getLastOrCarrierPosition();
        return Vector3.Distance(lastPosition, touchPosition);
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
        createLineChunks(landCorrectionPosition);
        createLineChunks(position);
        touchEnded();
        setLandingColor();
    }

    public void resetLine()
    {
        if (!pathLine) return;
        pathLine.positionCount = 0;
    }

    private void setLandingColor()
    {
        if (pathLine.colorGradient.Equals(landingColor)) return;
        onTrackAudio.Play();
        pathLine.colorGradient = landingColor;
    }

    private void setDefaultColor()
    {
        pathLine.colorGradient = defaultColor;
    }
}
