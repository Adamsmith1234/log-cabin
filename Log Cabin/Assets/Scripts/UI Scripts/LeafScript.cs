using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeafScript : MonoBehaviour
{
    public int leavesNeeded = 1;
    int leavesPickedUp = 0;
    public BarGauge leafMeter;
    public bool leavesOn = false;

    public bool full {
        get {return leavesPickedUp >= leavesNeeded;}
    }

    private void Start() {
        leafMeter.percentFilled = 0f;
    }

    public void pickUpLeaf() {
        if (leavesOn && leavesPickedUp < leavesNeeded) {
            leavesPickedUp += 1;
            leafMeter.percentFilled = (float) leavesPickedUp/(float) leavesNeeded;
        }
    }

    public void clearLeaves() {
        leavesPickedUp = 0;
        leafMeter.percentFilled = 0f;
    }
}
