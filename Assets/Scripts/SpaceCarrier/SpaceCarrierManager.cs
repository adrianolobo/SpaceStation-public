using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceCarrierManager : Singleton<SpaceCarrierManager>
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
            if (spaceCarrier.isVisible) {
                spaceCarrier.enteredScreen();
                continue;
            };
            offScreenSpaceCarriers.Add(spaceCarrier);
        }
        for (int i = 0; i < offScreenSpaceCarriers.Count; i++)
        {
            SpaceCarrier offScreenSpaceCarrier = offScreenSpaceCarriers[i];
            if (!offScreenSpaceCarrier.getHasAlreadyEnteredScreen()) continue;
            if (offScreenSpaceCarrier.getAmountOfContainers() > 0)
            {
                offScreenSpaceCarrier.turnToCenter();
                return;
            }
            spaceCarrierList.Remove(offScreenSpaceCarrier);
            Destroy(offScreenSpaceCarrier.gameObject);
        }

    }

    public void destroyAll()
    {
        for (int i = 0; i < spaceCarrierList.Count; i++)
        {
            Destroy(spaceCarrierList[i].gameObject);
        }
        spaceCarrierList = new List<SpaceCarrier>();
    }

    public void addSpaceCarrier(SpaceCarrier spaceCarrier)
    {
        spaceCarrierList.Add(spaceCarrier);
    }
}
