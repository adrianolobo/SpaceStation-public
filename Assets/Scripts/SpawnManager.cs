using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    private List<string> spawnOrder;
    // Start is called before the first frame update
    void Start()
    {
        resetSpawnOrder();
        Debug.Log(spawnOrder);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void resetSpawnOrder()
    {
        spawnOrder = new List<string> { "top", "right", "bottom", "left" };
        for (int i = 0; i < spawnOrder.Count; i++)
        {
            string temp = spawnOrder[i];
            int randomIndex = Random.Range(i, spawnOrder.Count);
            spawnOrder[i] = spawnOrder[randomIndex];
            spawnOrder[randomIndex] = temp;
        }
    }
}
