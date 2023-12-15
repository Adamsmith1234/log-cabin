using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeafScript : MonoBehaviour
{
    public int leavesNeeded = 10;
    int leavesPickedUp = 0;
    public BarGauge leafMeter;

    private void Start() {
        leafMeter.percentFilled = 0f;
    }

    public void pickUpLeaf() {
        Debug.Log("BOOM");
        if (leavesPickedUp < leavesNeeded) {
            leavesPickedUp += 1;
            leafMeter.percentFilled = leavesPickedUp/leavesNeeded;
        }
    }
}
