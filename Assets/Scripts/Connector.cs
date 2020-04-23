using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Connector : MonoBehaviour
{
    AbstractStationModule module;
    private void Start()
    {
        module = GetComponentInParent<AbstractStationModule>();
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        module.triggerConnect(this, collision);
    }
    void OnTriggerExit2D(Collider2D collision)
    {
        module.triggerConnect(null, null);
    }
}
