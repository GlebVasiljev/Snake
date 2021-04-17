using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Language
{
    English, Latvian, Russian
}

public class LanguageInf{

}
public class Translate : MonoBehaviour
{
    public Language language;
    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

 
}
