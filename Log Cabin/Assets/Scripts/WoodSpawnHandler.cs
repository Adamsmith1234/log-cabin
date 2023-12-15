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

    public int numberOfEach = 30;

    private void Start() {
        foreach (PoissonWrapper spawner in GetComponents<PoissonWrapper>()) {
            emptyPoints[(int) spawner.type] = spawner.points;
        }
        for (int x = 0; x < 5; x++) {
            if (fuelPrefabs[x] != null) spawnFuel((FireScript.fuels) x, numberOfEach);
        }
    }

    private void spawnFuel(FireScript.fuels type, int numSpawns = 1) {
        for (int x = 0; x < numSpawns; x++) {
            if (emptyPoints[(int) type].Count == 0) return; 

            Vector3 spawnPoint = getEmptyPoint(type);
            RaycastHit hit; // 
            if ((spawnPoint[2]<41 && spawnPoint[2] >-110) && (spawnPoint[0]>-153 && spawnPoint[0] < 47.036)){
                if (Physics.Raycast(spawnPoint, -Vector3.up, out hit)){
                    

                    GameObject newFuel = Instantiate(fuelPrefabs[(int) type], (hit.point), Quaternion.identity);
                    //Debug.DrawLine(transform.position, hit.point, Color.cyan);
                    newFuel.GetComponent<WoodPickup>().spawnPoint = spawnPoint;
                    newFuel.GetComponent<WoodPickup>().type = type;

                }
            }
        }
    }

    private Vector3 getEmptyPoint(FireScript.fuels type) {
        int i = Random.Range(0,emptyPoints[(int) type].Count);
        Vector3 chosenPoint = emptyPoints[(int) type][i];
        pointsInUse[(int) type].Add(chosenPoint);
        emptyPoints[(int) type].RemoveAt(i);
        return chosenPoint;
    }
    
    public void restorePoint(WoodPickup wood) {
        pointsInUse[(int) wood.type].Remove(wood.spawnPoint);
        spawnFuel(wood.type,1);
        emptyPoints[(int) wood.type].Add(wood.spawnPoint);
    }
}
