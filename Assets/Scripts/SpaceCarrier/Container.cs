using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Container : MonoBehaviour
{
    public Sprite containerRed;
    public Sprite containerBlue;
    public enum CARGO_COLOR { BLUE, RED };
    static CARGO_COLOR cargoColor = CARGO_COLOR.RED;
    void Awake()
    {
        setContainerRed();
    }

    public void setContainerBlue()
    {
        GetComponent<SpriteRenderer>().sprite = containerBlue;
        cargoColor = CARGO_COLOR.BLUE;
    }
    public void setContainerRed()
    {
        GetComponent<SpriteRenderer>().sprite = containerRed;
        cargoColor = CARGO_COLOR.RED;
    }

    public float getWitdh()
    {
        SpriteRenderer containerSprite = GetComponent<SpriteRenderer>();
        return containerSprite.sprite.rect.width / containerSprite.sprite.pixelsPerUnit;
    }

    public float getPixelPerUnit()
    {
        SpriteRenderer containerSprite = GetComponent<SpriteRenderer>();
        return containerSprite.sprite.pixelsPerUnit;
    }

    public bool isCargoRed()
    {
        return cargoColor == CARGO_COLOR.RED;
    }

    public bool isCargoBlue()
    {
        return cargoColor == CARGO_COLOR.BLUE;
    }
}
