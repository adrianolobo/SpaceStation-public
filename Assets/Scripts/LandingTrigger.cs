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
        if (collision.gameObject.name != "SpaceCarrier") return;
        SpaceCarrier spaceCarrier = collision.gameObject.GetComponent<SpaceCarrier>();
        Vector3 landingCorrectionPosition = transform.position;
        stationLanding.landCarrier(spaceCarrier, landingCorrectionPosition);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
