using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public SpaceCarrier spaceCarrier;

    private Stack<string> spawnOrder;

    private SpaceCarrierManager spaceCarrierManager;

    void Awake()
    {
        resetSpawnOrder();
        GameEvents.current.onStartSpawnSequence += startSpawnSequence;
        spaceCarrierManager = GetComponent<SpaceCarrierManager>();
    }

    private void OnDestroy()
    {
        GameEvents.current.onStartSpawnSequence -= startSpawnSequence;
    }

    public void startSpawnSequence(int amountOfCargos)
    {
        int cargosLeftToAdd = amountOfCargos;
        Stack<int> cargosList = new Stack<int>();

        // 50% chance of 2 cargos
        // 25% and 25% for 1 and 2 cargos;
        // if less than 3, the amount that is left is added
        while (cargosLeftToAdd > 0)
        {
            float cargoChance = Random.Range(0, 100);
            int cargoToAdd = 3;


            if (cargosLeftToAdd < 3) cargoToAdd = cargosLeftToAdd;
            else if (cargoChance < 50) cargoToAdd = 2;
            else if (cargoChance < 75) cargoToAdd = 1;

            cargosList.Push(cargoToAdd);
            cargosLeftToAdd -= cargoToAdd;
        }
        StartCoroutine(spawnSequence(cargosList));
    }

    IEnumerator spawnSequence(Stack<int> cargosList)
    {
        while (cargosList.Count > 0)
        {
            if (spawnOrder.Count == 0) resetSpawnOrder();
            string order = spawnOrder.Pop();
            Vector3 carrierPosition = getPosition(order);
            SpaceCarrier newSpaceCarrier = Instantiate(spaceCarrier, carrierPosition, Quaternion.identity);
            spaceCarrierManager.addSpaceCarrier(newSpaceCarrier);

            int cargos = cargosList.Pop();
            newSpaceCarrier.createContainers(cargos);
            yield return new WaitForSeconds(10f);
        }
        endSpawnSequence();
    }

    void endSpawnSequence()
    {
        GameEvents.current.endSpawnSequence();
    }

    private Vector3 getPosition(string order)
    {
        Vector3 maxPositions = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0));
        Vector3 minPositions = Camera.main.ScreenToWorldPoint(new Vector3(0, 0, 0));

        float posY = 0;
        float posX = 0;

        if (order == "top")
        {
            posX = Random.Range(minPositions.x, maxPositions.x);
            posY = maxPositions.y;
        } else if (order == "right")
        {
            posX = maxPositions.x;
            posY = Random.Range(minPositions.y, maxPositions.y);
        } else if (order == "bottom")
        {
            posX = Random.Range(minPositions.x, maxPositions.x);
            posY = minPositions.y;
        } else if (order == "left")
        {
            posX = minPositions.x;
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
