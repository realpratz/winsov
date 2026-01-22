using UnityEngine;
using System;
using TMPro;

public class ClockTime : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI clockText;
    private float h;
    private float m;

    // Update is called once per frame
    void Update()
    {
        h=DateTime.Now.Hour;
        m=DateTime.Now.Minute;

        clockText.text=h+":"+m;
    }
}
