using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Connector : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D collision)
    {
        AbstractStationModule module = GetComponentInParent<AbstractStationModule>();
        module.triggerConnect(this, collision);
        Debug.Log("CONNECTOR TRIGGER ENTER");
    }
    void OnTriggerExit2D(Collider2D collision)
    {
        Debug.Log("CONNECTOR TRIGGER EXIT");
    }
}
