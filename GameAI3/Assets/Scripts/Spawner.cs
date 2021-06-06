using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    private float spawnTimer = 1f;
    private int maxDwarves = 50;
    private int dwarvesSpawned = 0;
    public Transform goal;
    public GameObject dwarfPrefab;

    int survived = 0;
    int died = 0;
    float time = 0;

    // Update is called once per frame
    void Update()
    {

        if (survived + died == maxDwarves)
        {
            PrintData();
        }

        if (spawnTimer <= 0 && dwarvesSpawned < maxDwarves)
        {
            SpawnDwarf();
            dwarvesSpawned++;
            spawnTimer = 1f;
        }
        spawnTimer -= Time.deltaTime;
    }

    void SpawnDwarf()
    {
        GameObject dwarf = Instantiate(dwarfPrefab, new Vector3(0, 0, -2), Quaternion.identity);
        //dwarf.GetComponent<Dwarf>().spawner = this;
    }

    public void AddSurvived()
    {
        survived++;
    }

    public void AddDied()
    {
        died++;
    }

    public void AddTime(float time)
    {
        this.time += time;
    }

    void PrintData()
    {
        print("Dwarves survived: " + survived);
        print("Average time to the goal: " + Mathf.FloorToInt(time / survived));
        print("Dwarves died: " + died);

    }
}