using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StationModule : AbstractStationModule
{
    Rigidbody2D stationRB;

    private void Start()
    {
        stationRB = GetComponent<Rigidbody2D>();
    }
    public override void move(Vector3 position)
    {
        stationRB.MovePosition(position);
    }
    public override void endPlacing()
    {
        BoxCollider2D collider2d = GetComponent<BoxCollider2D>();
        collider2d.enabled = true;
    }
    public override void startPlacing()
    {
        BoxCollider2D collider2d = GetComponent<BoxCollider2D>();
        collider2d.enabled = false;
    }
}
