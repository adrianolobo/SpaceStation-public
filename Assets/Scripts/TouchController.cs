using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchController : MonoBehaviour
{
    List<TouchCarrier> touches = new List<TouchCarrier>();
    void Update()
    {
        if (Input.touchCount == 0) return;
        for (int i = 0; i < Input.touchCount; i++)
        {
            Touch touch = Input.GetTouch(i);
            if (touch.phase == TouchPhase.Began)
            {
                Vector3 touchPosition = Camera.main.ScreenToWorldPoint(touch.position);
                RaycastHit2D[] hits = Physics2D.RaycastAll(touchPosition, -Vector2.up);
                for (int j = 0; j < hits.Length; j++)
                {
                    Debug.Log(hits[j].collider);
                }
            }
            return;
            if (!checkIdExists(touch.fingerId))
            {
                touches.Add(new TouchCarrier(touch.fingerId, touch));
            };
            TouchCarrier touchCarrier = getTouchCarrierByFingerId(touch.fingerId);
            if (touch.phase == TouchPhase.Ended)
            {
                touches.Remove(touchCarrier);
                continue;
            }
            touchCarrier.touch = touch;
        }
    }

    private bool checkIdExists(int fingerId)
    {
        return touches.Exists((touch) => touch.fingerId == fingerId);
    }

    private TouchCarrier getTouchCarrierByFingerId(int fingerId)
    {
        return touches.Find(touch => touch.fingerId == fingerId);
    }
}
