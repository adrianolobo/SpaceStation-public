using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LandingTrigger : MonoBehaviour
{
    private StationLanding stationLanding;
    void Start()
    {
        stationLanding = GetComponentInParent<StationLanding>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("YAYYYYY");
        Debug.Log(collision.gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
