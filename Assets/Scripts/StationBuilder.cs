using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StationBuilder : MonoBehaviour
{

    public AbstractStationModule sationModule;
    private AbstractStationModule moduleToPlace;
    // Start is called before the first frame update
    void Start()
    {
        moduleToPlace = Instantiate(sationModule);
        moduleToPlace.startPlacing();
    }

    // Update is called once per frame
    void Update()
    {
        if (moduleToPlace)
        {
            Vector3 position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            moduleToPlace.move(position);
        }
    }
}
