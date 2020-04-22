using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StationBuilder : MonoBehaviour
{

    public AbstractStationModule sationModule;
    private AbstractStationModule moduleToPlace;

    void Start()
    {
        moduleToPlace = Instantiate(sationModule);
        moduleToPlace.startPlacing();
    }

    void Update()
    {
        if (!moduleToPlace) return;
        Vector3 position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        moduleToPlace.move(position);
        if (Input.GetMouseButtonDown(0))
        {
            (Connector moduleToPlaceConnector, Collider2D otherModuleConnectorCollider) = moduleToPlace.getConnectors();
        }
    }
}
