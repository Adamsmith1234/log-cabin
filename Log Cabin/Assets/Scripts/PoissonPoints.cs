using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Numerics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public static class PoissonPoints {
    public static List<UnityEngine.Vector3> generatePoints(float objectRadius, int maxAttempts = 10, float regionRadius = 2, float innerRadius = 1, int maxPoints = 1000) {
        float gridCellWidth = objectRadius/Mathf.Sqrt(2);
        int gridWidth = Mathf.CeilToInt(regionRadius * 2 / gridCellWidth);
        int[,] grid = new int[gridWidth,gridWidth];
        List<UnityEngine.Vector2> points = new List<UnityEngine.Vector2>();
        List<UnityEngine.Vector2> spawnPoints = new List<UnityEngine.Vector2>{new UnityEngine.Vector2(0,0)};
        while (spawnPoints.Count > 0 && points.Count < maxPoints) {
            int spawnPointIndex = UnityEngine.Random.Range(0,spawnPoints.Count);
            UnityEngine.Vector2 spawnOrigin = spawnPoints[spawnPointIndex];
            bool newPointFound = false;
            for (int i = 0; i < maxAttempts; i++) {
                //Create New Candidate Point
                float angle = UnityEngine.Random.value * Mathf.PI * 2;
                UnityEngine.Vector2 possibleNewPoint = spawnOrigin + (new UnityEngine.Vector2(Mathf.Sin(angle),Mathf.Cos(angle))
                                            * UnityEngine.Random.Range(objectRadius,2*objectRadius));
                int newPointX = (int) (possibleNewPoint.x/gridCellWidth) + gridWidth/2;
                int newPointY = (int) (possibleNewPoint.y/gridCellWidth) + gridWidth/2;
                //Check if point is within ring
                float centerDistance = possibleNewPoint.sqrMagnitude;
                if (centerDistance > regionRadius*regionRadius) {
                    continue;
                }
                //Check if any points nearby overlap
                bool broken = false;
                for (int x = Mathf.Max(0,newPointX-2); x < Mathf.Min(grid.GetLength(0),newPointX+3); x++) {
                    if (broken) break;
                    for (int y = Mathf.Max(0,newPointY-2); y < Mathf.Min(grid.GetLength(1),newPointY+3); y++) {
                        if (grid[x,y] != 0 && (possibleNewPoint - points[grid[x,y]-1]).sqrMagnitude < objectRadius*objectRadius) {
                            broken = true;
                            break;
                        }
                    }
                }
                if (!broken) {
                    points.Add(possibleNewPoint);
                    spawnPoints.Add(possibleNewPoint);
                    grid[newPointX,newPointY] = points.Count;
                    newPointFound = true;
                    break;
                }
            }
            if (!newPointFound) {
                spawnPoints.RemoveAt(spawnPointIndex);
            }
        }
        List<UnityEngine.Vector3> outPoints = new List<UnityEngine.Vector3>();
        for (int p = points.Count - 1; p >= 0; p--) {
            if (!(points[p].sqrMagnitude < innerRadius*innerRadius)) {
                outPoints.Add(new UnityEngine.Vector3(points[p].x,points[p].y,UnityEngine.Random.Range(0,5)));
            }
        }
        return outPoints;
    }
}
