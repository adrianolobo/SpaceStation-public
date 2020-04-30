using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public SpaceCarrier spaceCarrier;

    private Stack<string> spawnOrder;

    private SpaceCarrierManager spaceCarrierManager;

    private Coroutine spawnCoroutine;

    void Awake()
    {
        resetSpawnOrder();
        GameEvents.current.onStartSpawnSequence += startSpawnSequence;
        spaceCarrierManager = GetComponent<SpaceCarrierManager>();

        GameEvents.current.onCreateNewModule += pauseSpawn;
        GameEvents.current.onNewModuleCreated += resumeSpawn;
    }

    private void OnDestroy()
    {
        GameEvents.current.onStartSpawnSequence -= startSpawnSequence;
        GameEvents.current.onCreateNewModule -= pauseSpawn;
        GameEvents.current.onNewModuleCreated -= resumeSpawn;
    }
    private void pauseSpawn()
    {
        StopCoroutine(spawnCoroutine);
    }

    private void resumeSpawn()
    {
        spawnCoroutine = StartCoroutine(spawnSequence());
    }

    private void startSpawnSequence()
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

        if (cargoChance < 50) return 2;
        else if (cargoChance < 75) return 1;
        return 3;
    }

    IEnumerator spawnSequence()
    {
        // TODO: CREATE A STOP FLAG
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(7, 14));
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
