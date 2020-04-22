using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject spaceCarrier;

    private Stack<string> spawnOrder;

    void Start()
    {
        resetSpawnOrder();
        // StartCoroutine(spawnProcess());
    }

    IEnumerator spawnProcess()
    {
        string order = spawnOrder.Pop();

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

        Instantiate(spaceCarrier, new Vector3(posX, posY, 0), Quaternion.identity);


        yield return new WaitForSeconds(50f);


        if (spawnOrder.Count == 0) {
            resetSpawnOrder();
        };

        StartCoroutine(spawnProcess());
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
