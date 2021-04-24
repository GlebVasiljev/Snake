using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

[System.Serializable]
public class UserInfo
{
    public string PlayerName;
    public int PlayerScore;
    public double PlayerGameTime;
    public LevelInfo[] Level;
}

[System.Serializable]
public class LevelInfo
{
    public int LevelNumber;
    public string LevelName;
}

public class JsonReader : MonoBehaviour
{
    string Path;
    public TextAsset JsonFile;

    UserInfo GetUserInfo()
    {
        
        StreamReader FileReader = new StreamReader(Path);
        if(FileReader == null)
        {
            Debug.Log("File not found");
            return null;
        }
        string Json = FileReader.ReadToEnd();
        FileReader.Close();
        UserInfo Result = JsonUtility.FromJson<UserInfo>(Json);
        return Result;
    }

    void SaveJson(UserInfo Info)
    {

        string Json = JsonUtility.ToJson(Info);
        StreamWriter FileWriter = new StreamWriter(Path, false);
        FileWriter.Write(Json);
        FileWriter.Close();
    }

    private void Start()
    {
        Path = Application.dataPath + "/Test.json";
        UserInfo Test = GetUserInfo();

        if (Test != null)
        {
            Debug.Log(Test.PlayerName);

            Test.PlayerScore = 10;
            foreach (LevelInfo Level in Test.Level)
            {
                Debug.Log(Level.LevelName);
            }
            SaveJson(Test);
        }
    }
}


