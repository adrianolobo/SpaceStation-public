using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StationLanding : MonoBehaviour
{
    public void landCarrier(SpaceCarrier carrier, Vector3 landingCorrectionPosition)
    {

        carrier.land(landingCorrectionPosition, transform.position);
    }
}
