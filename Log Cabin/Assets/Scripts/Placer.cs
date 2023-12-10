using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

public class PoissonWrapper : MonoBehaviour
{
    public int outerRadius = 2;
    public int innerRadius = 1;
    public int objectSize = 2;
    public int gizmoRadius = 2;
    public int maxAttempts = 10;
    public int maxPoints = 1000;
    public Color color = Color.gray;
    public FireScript.fuels type = FireScript.fuels.TINDER;

    public List<UnityEngine.Vector3> points = new List<UnityEngine.Vector3>();

    private void OnValidate() {
        points = PoissonPoints.generatePoints(objectSize,maxAttempts,outerRadius,innerRadius,maxPoints);
    }
    private void OnDrawGizmosSelected() {
        Gizmos.color = color;
        foreach (UnityEngine.Vector3 point in points) {
            Gizmos.DrawSphere(point + transform.position, gizmoRadius);
        }
        Gizmos.DrawWireSphere(transform.position,outerRadius+objectSize/2);
        Gizmos.DrawWireSphere(transform.position,innerRadius-objectSize/2);
    }
}
