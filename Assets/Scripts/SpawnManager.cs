using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : Singleton<SpawnManager>
{
    public SpaceCarrier spaceCarrier;

    private Stack<string> spawnOrder;

    private SpaceCarrierManager spaceCarrierManager;

    private Coroutine spawnCoroutine;

    void Awake()
    {
        resetSpawnOrder();
        spaceCarrierManager = GetComponent<SpaceCarrierManager>();
    }

    public void stop()
    {
        StopCoroutine(spawnCoroutine);
    }

    public void startSpawnSequence()
    {
        createCarrier();
        spawnCoroutine = StartCoroutine(spawnSequence());
    }
    private int getAmountCargos()
    {
        // 50% chance of 2 cargos
        // 25% and 25% for 1 and 2 cargos;
        // if less than 3, the amount that is left is added
        float cargoChance = Random.Range(0, 100);
        SpaceStation spaceStation = SpaceStations.Instance.getSelected();
        float cargoPercentage1 = spaceStation.cargoPercentage1;
        float cargoPercentage2 = spaceStation.cargoPercentage2;

        if (cargoChance < cargoPercentage1) return 1;
        else if (cargoChance < (cargoPercentage1 + cargoPercentage2)) return 2;
        return 3;
    }

    IEnumerator spawnSequence()
    {
        float[] spawnChanges = SpaceStations.Instance.getSelected().spawnChances;
        // TODO: CREATE A STOP FLAG
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(spawnChanges[0], spawnChanges[1]));
            createCarrier();
        }
    }

    private void createCarrier()
    {
        if (spawnOrder.Count == 0) resetSpawnOrder();
        string order = spawnOrder.Pop();
        Vector3 carrierPosition = getPosition(order);
        SpaceCarrier newSpaceCarrier = Instantiate(spaceCarrier, carrierPosition, Quaternion.identity);
        spaceCarrierManager.addSpaceCarrier(newSpaceCarrier);

        int cargos = getAmountCargos();
        newSpaceCarrier.createContainers(cargos);
    }

    private Vector3 getPosition(string order)
    {
        Vector3 maxPositions = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0));
        Vector3 minPositions = Camera.main.ScreenToWorldPoint(new Vector3(0, 0, 0));

        float posY = 0;
        float posX = 0;
        float padding = 1;

        if (order == "top")
        {
            posX = Random.Range(minPositions.x, maxPositions.x);
            posY = maxPositions.y + padding;
        } else if (order == "right")
        {
            posX = maxPositions.x + padding;
            posY = Random.Range(minPositions.y, maxPositions.y);
        } else if (order == "bottom")
        {
            posX = Random.Range(minPositions.x, maxPositions.x);
            posY = minPositions.y - padding;
        } else if (order == "left")
        {
            posX = minPositions.x - padding;
            posY = Random.Range(minPositions.y, maxPositions.y);
        }

        return new Vector3(posX, posY, 0);
    }

    private void resetSpawnOrder()
    {
        List<string> defaultOrder = new List<string> { "top", "right", "bottom", "left" };
        for (int i = 0; i < defaultOrder.Count; i++)
        {
            string temp = defaultOrder[i];
            int randomIndex = Random.Range(i, defaultOrder.Count);
            defaultOrder[i] = defaultOrder[randomIndex];
            defaultOrder[randomIndex] = temp;
        }
        spawnOrder = new Stack<string>(defaultOrder.ToArray());
    }
}
