using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OffscreenIndicator : MonoBehaviour
{
    private SpaceCarrier spaceCarrier;
    private Vector3 maxPositions;
    private Vector3 minPositions;

    private void Awake()
    {
        maxPositions = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0));
        minPositions = Camera.main.ScreenToWorldPoint(new Vector3(0, 0, 0));
    }

    public void setSpaceCarrier(SpaceCarrier spaceCarrier)
    {
        this.spaceCarrier = spaceCarrier;
    }

    // Update is called once per frame
    void Update()
    {
        if (!spaceCarrier) {
            Destroy(gameObject);
            return;
        };
        string direction = getSpaceScarrierDirection();
        followCarrier(direction);
    }

    private void followCarrier(string direction)
    {
        Rect indicatorRect = GetComponent<SpriteRenderer>().sprite.rect;
        float pixelPerUnit = GetComponent<SpriteRenderer>().sprite.pixelsPerUnit;
        float halfWidth = (indicatorRect.width / pixelPerUnit) / 2;
        float halfHeight = (indicatorRect.height/ pixelPerUnit) / 2;

        Vector3 carrierPosition = spaceCarrier.transform.position;
        if (direction == "right")
        {
            transform.position = new Vector3(maxPositions.x - halfWidth, carrierPosition.y, 0);
            transform.rotation = Quaternion.Euler(0, 0, 0);
            return;
        }
        if (direction == "top")
        {
            transform.position = new Vector3(carrierPosition.x, maxPositions.y - halfHeight, 0);
            transform.rotation = Quaternion.Euler(0, 0, 90);
            return;
        }
        if (direction == "left")
        {
            transform.position = new Vector3(minPositions.x + halfWidth, carrierPosition.y, 0);
            transform.rotation = Quaternion.Euler(0, 0, 180);
            return;
        }
        transform.position = new Vector3(carrierPosition.x, minPositions.y + halfHeight, 0);
        transform.rotation = Quaternion.Euler(0, 0, 270);
    }

    private string getSpaceScarrierDirection()
    {
        Vector3 position = spaceCarrier.transform.position;

        if (position.x < minPositions.x) return "left";
        if (position.x > maxPositions.x) return "right";
        if (position.y > maxPositions.y) return "top";
        return "bottom";
    }
}
