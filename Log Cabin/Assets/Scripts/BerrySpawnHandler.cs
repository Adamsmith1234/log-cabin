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
            //Subtracting 100 to each
            if ((p[2]<41 && p[2] >-110) && (p[0]>-153 && p[0] < 47.036)){
            if (Physics.Raycast(p, -Vector3.up, out hit))
                {
                    //float offsetDistance = hit.distance;
                    Debug.Log(hit.point);
                    GameObject newBush = Instantiate(BerryPrefabs[0], (hit.point), Quaternion.identity);
                    //Debug.DrawLine(transform.position, hit.point, Color.cyan);
                }
            }
            
        }
    }
}

