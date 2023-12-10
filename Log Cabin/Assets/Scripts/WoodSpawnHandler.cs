using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;

public class WoodSpawnHandler : MonoBehaviour
{
    List<Vector3>[] emptyPoints = new List<Vector3>[5];
    List<Vector3>[] pointsInUse = new List<Vector3>[] {new List<Vector3>(),new List<Vector3>(),new List<Vector3>(),new List<Vector3>(),new List<Vector3>()};
    int[] numSpawned = new int[5];
    public GameObject[] fuelPrefabs = new GameObject[5];

    private void Start() {
        foreach (PoissonWrapper spawner in GetComponents<PoissonWrapper>()) {
            emptyPoints[(int) spawner.type] = spawner.points;
        }
        for (int x = 0; x < 5; x++) {
            if (fuelPrefabs[x] != null) spawnFuel((FireScript.fuels) x,10);
        }
    }

    private void spawnFuel(FireScript.fuels type, int numSpawns = 1) {
        for (int x = 0; x < numSpawns; x++) {
            Vector3 spawnPoint = getEmptyPoint(type);
            GameObject newFuel = Instantiate(fuelPrefabs[(int) type],spawnPoint+(Vector3.up * 3), Quaternion.identity);
        }
    }

    private Vector3 getEmptyPoint(FireScript.fuels type) {
        int i = Random.Range(0,emptyPoints.Length);
        Vector3 chosenPoint = emptyPoints[(int) type][i];
        pointsInUse[(int) type].Add(chosenPoint);
        emptyPoints[(int) type].RemoveAt(i);
        return chosenPoint;
    }

    private void restorePoint(FireScript.fuels type, Vector3 p) {
        emptyPoints[(int) type].Add(p);
        pointsInUse[(int) type].Remove(p);
    }
}
