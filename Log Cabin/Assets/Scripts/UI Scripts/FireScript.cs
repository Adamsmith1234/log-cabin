using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireScript : MonoBehaviour
{
    public BarGauge heat;
    public void updateSystem() {
        heat.percentFilled = 1f;
    }
}
