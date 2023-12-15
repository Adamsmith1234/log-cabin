using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BerrySpawnHandler : MonoBehaviour
{
    public GameObject[] BerryPrefabs;

    private void Start()
    {
        List<Vector3> points = GetComponent<PoissonWrapper>().points;
        RaycastHit hit; // 

        foreach (Vector3 p in points)
        {
            
            if (Physics.Raycast(p, -Vector3.up, out hit))
            {
                //float offsetDistance = hit.distance;
                Debug.Log(hit.point);
                GameObject newBush = Instantiate(BerryPrefabs[0], (hit.point) + new Vector3 (0,2,0), Quaternion.identity);
                //Debug.DrawLine(transform.position, hit.point, Color.cyan);
            }

            
        }
    }
}

