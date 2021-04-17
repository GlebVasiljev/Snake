using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AppleController : MonoBehaviour
{
    static AppleController Instance;
    public GoldenApple Apple;
    public SpriteRenderer PlayArea;

    void Start()
    {
        Debug.Log(PlayArea.bounds.min);
        Debug.Log(PlayArea.bounds.max);
    }

    private void Awake()
    {
        Instance = this;
    }
    void AppleCreate()
    {
        Vector3 P = Vector3.zero;
        P.x = Random.Range(PlayArea.bounds.min.x, PlayArea.bounds.max.x);
        P.y = Random.Range(PlayArea.bounds.min.y, PlayArea.bounds.max.y);
        GoldenApple apple = Instantiate(Apple, P, Quaternion.identity);
        float random = Random.value;
        if (random < 0.15f)
        {
            apple.Type = AppleType.Golden;
        }
        else if (random < 0.20f)
        {
            apple.Type = AppleType.Blue;
        }
        else
        {
            apple.Type = AppleType.Normal;
        }
        

    }

    public static void CreateApple()
    {
        Instance.AppleCreate();
    }
}
