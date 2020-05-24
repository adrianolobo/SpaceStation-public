using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StationBuilder : MonoBehaviour
{

    public AbstractStationModule sationModule;
    void Start()
    {
        GameEvents.current.onModulePlaced += placeModule;
    }

    private void OnDestroy()
    {
        GameEvents.current.onModulePlaced -= placeModule;
    }
    void createNewModule()
    {
        AbstractStationModule moduleToPlace = Instantiate(sationModule, new Vector3(2, 4, 0), Quaternion.identity);
        moduleToPlace.setOnlyVisual();
        moduleToPlace.startPlacing();
    }

    void placeModule(AbstractStationModule module)
    {
        if (!module.canConect) return;
        (Connector moduleToPlaceConnector, Collider2D otherModuleConnectorCollider) = module.getConnectors();
        Vector3 connectorsDifferencePosition = moduleToPlaceConnector.transform.position - otherModuleConnectorCollider.transform.position;
        module.move(module.transform.position - connectorsDifferencePosition);
        module.endPlacing();

        GameEvents.current.newModuleCreated();
    }
}
