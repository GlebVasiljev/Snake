using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class DayController : MonoBehaviour
{
    public Light2D Light;
    public float DaySpeed = 40f;
    public static float Day = 1f;
    void Update()
    {
        Day = Mathf.PingPong(Time.time/DaySpeed, 1);
       // Light.intensity = 1 - Day;

        /* if(Day > 0)
        {
            Day -= Time.deltaTime/DaySpeed;
            Light.intensity = Day;
        }
        else
        {
            Light.intensity = 0;
        }
        */
    }
    
}
