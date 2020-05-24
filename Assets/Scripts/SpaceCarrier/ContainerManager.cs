using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContainerManager : MonoBehaviour
{
    public GameObject containerPrefab;
    private List<GameObject> containers = new List<GameObject>();
    public void createContainers(int amountToCreate)
    {
        for (int i = 0; i < amountToCreate; i ++)
        {
            GameObject container = Instantiate(containerPrefab, transform);
            SpriteRenderer containerSprite = container.GetComponent<SpriteRenderer>();
            float sizeX = containerSprite.sprite.rect.width / containerSprite.sprite.pixelsPerUnit;
            float padding = 2 / containerSprite.sprite.pixelsPerUnit;
            container.transform.localPosition = new Vector3((sizeX + padding) * i, 0, 0);
            containers.Add(container);
        }
    }

    public bool removeContainer()
    {
        if (containers.Count == 0) return false;
        GameObject container = containers[containers.Count - 1];
        containers.RemoveAt(containers.Count - 1);
        Destroy(container);
        return true;
    }

    public int getContainersCount()
    {
        return containers.Count;
    }
}
