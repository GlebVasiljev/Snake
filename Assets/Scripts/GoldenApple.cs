using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum AppleType { Normal, Golden, Blue }

public class GoldenApple : MonoBehaviour
{
    public AppleType Type;
    void Start()
    {
        for(int x = 0 ; x < transform.childCount; x++)
        {
            transform.GetChild(0).GetChild(x).gameObject.SetActive(false);

        }
        switch (Type)
        {
            case AppleType.Normal:
                transform.GetChild(0).GetChild(0).gameObject.SetActive(true);
                break;
            case AppleType.Golden:
                transform.GetChild(0).GetChild(1).gameObject.SetActive(true);
                break;
            case AppleType.Blue:
                transform.GetChild(0).GetChild(2).gameObject.SetActive(true);
                break;
        }
    }


}
