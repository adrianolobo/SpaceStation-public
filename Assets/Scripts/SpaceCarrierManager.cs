using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceCarrierManager : MonoBehaviour
{
    List<SpaceCarrier> spaceCarrierList = new List<SpaceCarrier>();

    private void Start()
    {
        BoxCollider2D boxCollider = GetComponent<BoxCollider2D>();
        boxCollider.size = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width * 2, Screen.height * 2, 0));
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        Debug.Log(collision);
    }

    public void addSpaceCarrier(SpaceCarrier spaceCarrier)
    {
        spaceCarrierList.Add(spaceCarrier);
    }
}
