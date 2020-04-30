using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceCarrierManager : MonoBehaviour
{
    List<SpaceCarrier> spaceCarrierList = new List<SpaceCarrier>();

    private void Update()
    {
        checkOffscreen();
    }

    void checkOffscreen()
    {
        List<SpaceCarrier> offScreenSpaceCarriers = new List<SpaceCarrier>();
        for (int i = 0; i < spaceCarrierList.Count; i++)
        {
            SpaceCarrier spaceCarrier = spaceCarrierList[i];
            bool isVisible = spaceCarrier.GetComponent<Renderer>().isVisible;
            if (isVisible) {
                spaceCarrier.enteredScreen();
                continue;
            };
            offScreenSpaceCarriers.Add(spaceCarrier);
        }
        for (int i = 0; i < offScreenSpaceCarriers.Count; i++)
        {
            SpaceCarrier offScreenSpaceCarrier = offScreenSpaceCarriers[i];
            if (!offScreenSpaceCarrier.getHasAlreadyEnteredScreen()) continue;
            spaceCarrierList.Remove(offScreenSpaceCarrier);
            Destroy(offScreenSpaceCarrier.gameObject);
        }

    }

    public void addSpaceCarrier(SpaceCarrier spaceCarrier)
    {
        spaceCarrierList.Add(spaceCarrier);
    }
}
