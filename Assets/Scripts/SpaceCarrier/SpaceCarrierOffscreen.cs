using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceCarrierOffscreen : MonoBehaviour
{
    SpaceCarrier spaceCarrier;
    OffscreenIndicator offscreenIndicator;
    public OffscreenIndicator OffscreenIndicatorPrefab;
    void Start()
    {
        spaceCarrier = GetComponent<SpaceCarrier>();
    }

    void Update()
    {
        createIndicator();
    }

    public void destroyIndicator()
    {
        Destroy(offscreenIndicator.gameObject);
    }

    private void createIndicator()
    {
        if (!spaceCarrier.isVisible && offscreenIndicator == null)
        {
            offscreenIndicator = Instantiate(OffscreenIndicatorPrefab);
            offscreenIndicator.setSpaceCarrier(spaceCarrier);
            return;
        }
        if (spaceCarrier.isVisible && offscreenIndicator != null)
        {
            destroyIndicator();
            offscreenIndicator = null;
        }
    }
}
