using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishLandingTrigger : MonoBehaviour
{
    private StationLanding stationLanding;
    void Start()
    {
        stationLanding = GetComponentInParent<StationLanding>();
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        // TODO: Verify if is necessary to check if the collision is of the landed carrier
        stationLanding.finishLanding();
    }
}
