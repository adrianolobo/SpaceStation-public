using System.Collections;
using System.Collections.Generic;
using UnityEngine;

abstract public class AbstractStationModule: MonoBehaviour 
{
    abstract public void move(Vector3 position);
    abstract public void endPlacing();
    abstract public void startPlacing();
}
