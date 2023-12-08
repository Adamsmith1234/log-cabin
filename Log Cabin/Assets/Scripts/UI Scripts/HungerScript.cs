using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HungerScript : MonoBehaviour
{
    public BarGauge stomach;
    public void updateSystem() {
        stomach.percentFilled = 1f;
    }
}
