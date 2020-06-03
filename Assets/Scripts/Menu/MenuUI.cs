using DanielLochner.Assets.SimpleScrollSnap;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuUI : MonoBehaviour
{
    public void play()
    {
        SimpleScrollSnap menuScrollSnap = transform.Find("ScrollSnap").GetComponent<SimpleScrollSnap>();
        Transform menuItemSelected = menuScrollSnap.Content.GetChild(menuScrollSnap.CurrentPanel);
        SpaceStation spaceStationSelected = menuItemSelected.GetComponent<MenuStationLevel>().spaceStation;
        SceneLoader.Instance.goToGame(spaceStationSelected.name);
    }
}
