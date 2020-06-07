using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContainerManager : MonoBehaviour
{
    public GameObject containerPrefab;
    private List<GameObject> containers = new List<GameObject>();
    public void createContainers(int amountToCreate)
    {
        bool allContainersSameColor = SpaceStation.Instance.allContainersSameColor;
        Container.CARGO_COLOR initialCargoColor = getRandomCargoColor();
        List<Container.CARGO_COLOR> cargoColorsToCreate = new List<Container.CARGO_COLOR>();
        for (int i = 0; i < amountToCreate; i++)
        {
            if (allContainersSameColor)
            {
                cargoColorsToCreate.Add(initialCargoColor);
                continue;
            }
            cargoColorsToCreate.Add(getRandomCargoColor());
        }


        for (int i = 0; i < cargoColorsToCreate.Count; i++)
        {
            Container.CARGO_COLOR cargoColor = cargoColorsToCreate[i];
            GameObject containerObj = Instantiate(containerPrefab, transform);
            Container containerSprite = containerObj.GetComponent<Container>();
            float sizeX = containerSprite.getWitdh();
            float padding = 2 / containerSprite.getPixelPerUnit();

            setCargoType(cargoColor, containerSprite);

            containerObj.transform.localPosition = new Vector3((sizeX + padding) * i, 0, 0);
            containers.Add(containerObj);
        }
    }

    public void setCargoType(Container.CARGO_COLOR cargoColor, Container containerSprite)
    {
        if (cargoColor == Container.CARGO_COLOR.RED)
        {
            containerSprite.setContainerRed();
            return;
        }
        containerSprite.setContainerBlue();
    }

    private Container.CARGO_COLOR getRandomCargoColor()
    {
        float colorChance = Random.Range(0, 100);
        int redPercentage = SpaceStation.Instance.cargoRedPercentage;
        if (colorChance <= redPercentage) return Container.CARGO_COLOR.RED;
        return Container.CARGO_COLOR.BLUE;
    }
    public bool removeContainer(Container.CARGO_COLOR[] accepts)
    {
        List<Container.CARGO_COLOR> acceptsList = new List<Container.CARGO_COLOR>(accepts);
        GameObject containerToRemove = containers.Find((containerObj) =>
        {
            Container container = containerObj.GetComponent<Container>();
            return acceptsList.Exists((acceptColor) =>
            {
                return container.getCargoColor() == acceptColor;
            });
        });
        if (containerToRemove == null) return false;
        containers.Remove(containerToRemove);
        Destroy(containerToRemove);
        return true;
    }

    public bool hasRedContainer()
    {
        GameObject redContainer = containers.Find((container) =>
        {
            return container.GetComponent<Container>().isCargoRed();
        });
        return redContainer != null;
    }

    public bool hasBlueContainer()
    {
        GameObject blueContainer = containers.Find((container) =>
        {
            return container.GetComponent<Container>().isCargoBlue();
        });
        return blueContainer != null;
    }

    public int getContainersCount()
    {
        return containers.Count;
    }
}
