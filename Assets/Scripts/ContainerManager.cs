using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContainerManager : MonoBehaviour
{
    public GameObject containerPrefab;
    private GameObject[] containers = new GameObject[3];
    void Start()
    {
        int amountToCreate = 3;
        for (int i = 0; i < amountToCreate; i ++)
        {
            GameObject container = Instantiate(containerPrefab, transform);
            SpriteRenderer containerSprite = container.GetComponent<SpriteRenderer>();
            float sizeX = containerSprite.sprite.rect.width / containerSprite.sprite.pixelsPerUnit;
            float padding = 2 / containerSprite.sprite.pixelsPerUnit;
            container.transform.localPosition = new Vector3((sizeX + padding) * i, 0, 0);
            containers[i] = container;
        }
    }
}
