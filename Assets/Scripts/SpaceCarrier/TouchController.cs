﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchController : MonoBehaviour
{
    List<TouchCarrier> touches = new List<TouchCarrier>();
    private void Start()
    {
        touches = new List<TouchCarrier>();
    }
    void Update()
    {
        if (Input.touchCount == 0) return;
        for (int i = 0; i < Input.touchCount; i++)
        {
            Touch touch = Input.GetTouch(i);
            #if UNITY_ANDROID
            touchHandler(touch);
            #endif
        }
    }

    private void touchHandler(Touch touch)
    {
        if (touch.phase == TouchPhase.Ended || touch.phase == TouchPhase.Canceled)
        {
            touchEndHandler(touch);
            return;
        }
        if (touch.phase == TouchPhase.Began)
        {
            touchBeganHandler(touch);
            return;
        }

        TouchCarrier touchCarrier = getTouchCarrierByFingerId(touch.fingerId);
        if (touchCarrier == null) return;
        touchCarrier.touch = touch;
        touchCarrier.pathLine.drawLine(getTouchWorldPosition(touchCarrier));
    }

    private void touchEndHandler(Touch touch)
    {
        TouchCarrier touchCarrier = getTouchCarrierByFingerId(touch.fingerId);
        if (touchCarrier == null) return;
        touchCarrier.pathLine.touchEnded();
        touches.Remove(touchCarrier);
    }

    private void touchBeganHandler(Touch touch)
    {
        Vector3 touchPosition = Camera.main.ScreenToWorldPoint(touch.position);
        RaycastHit2D[] hits = Physics2D.RaycastAll(touchPosition, -Vector2.up);
        GameObject spaceCarrierGO = null;
        for (int i = 0; i < hits.Length; i++)
        {
            GameObject colliderGO = hits[i].collider.gameObject;
            if (colliderGO.tag == "TouchRadar")
            {
                spaceCarrierGO = colliderGO;
                break;
            }
        }
        if (spaceCarrierGO == null) return;
        PathLine pathLine = spaceCarrierGO.GetComponentInParent<PathLine>();
        TouchCarrier touchCarrier = new TouchCarrier(touch.fingerId, touch, pathLine);
        touches.Add(touchCarrier);
        touchCarrier.pathLine.touchBegan(getTouchWorldPosition(touchCarrier));
    }

    private Vector3 getTouchWorldPosition(TouchCarrier touchCarrier)
    {
        Touch touch = touchCarrier.touch;
        Vector3 touchPosition = Camera.main.ScreenToWorldPoint(touch.position);
        touchPosition.z = 0;
        return touchPosition;
    }

    private TouchCarrier getTouchCarrierByFingerId(int fingerId)
    {
        return touches.Find(touch => touch.fingerId == fingerId);
    }
}
