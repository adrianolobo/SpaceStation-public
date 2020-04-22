using System.Collections;
using System.Collections.Generic;
using UnityEngine;

abstract public class AbstractStationModule: MonoBehaviour 
{
    protected int triggerCount = 0;
    protected Connector selfConnector;
    protected Collider2D otherConnectorCollider;

    virtual public void triggerConnect(Connector connector, Collider2D collider)
    {
        selfConnector = connector;
        otherConnectorCollider = collider;
    }
       
    virtual public (Connector, Collider2D) getConnectors()
    {
        return (selfConnector, otherConnectorCollider);
    }

    virtual public void move(Vector3 position)
    {
        Rigidbody2D stationRB = GetComponent<Rigidbody2D>();
        stationRB.MovePosition(position);
    }
    virtual protected void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Connector") return;
        triggerCount++;
        Debug.Log("TRIGGER ENTER");
    }
    virtual protected void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Connector") return;
        triggerCount--;
        Debug.Log("TRIGGER EXIT");
    }
    virtual public void startPlacing()
    {
        triggerCount = 0;
        BoxCollider2D collider2d = GetComponent<BoxCollider2D>();
        collider2d.isTrigger = true;
    }
    virtual public void endPlacing()
    {
        triggerCount = 0;
        BoxCollider2D collider2d = GetComponent<BoxCollider2D>();
        collider2d.isTrigger = false;
    }
}
