using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class BarGauge : MonoBehaviour
{
    [SerializeField] private GameObject meterPanel;
    [SerializeField] private Color meterColor = new Color(0,0,0,1);
    private int width = 198;
    public float percentFilled {
        get {return _percentFilled;}
        set {
            _percentFilled = Mathf.Clamp(value,0f,1f);
            updateLength();
        }
    }
    [SerializeField, Range(0f, 1f)]  private float _percentFilled = .5f;

    private void OnValidate() {
        updateLength();
        meterPanel.GetComponent<UnityEngine.UI.Image>().color = meterColor;
    }

    public void updateLength() {
        meterPanel.GetComponent<RectTransform>().sizeDelta = new Vector2(percentFilled*width,18);
    }
}
