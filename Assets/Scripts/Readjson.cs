using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

[System.Serializable]
class readJson
{
    public string tpye, name, crs, features;
}



public class Readjson : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        var loadedJson = Resources.Load<TextAsset>("JSON/��⵵_������_�д籸_�ǹ�");
        
        readJson myjson = JsonUtility.FromJson<readJson>(loadedJson.ToString());
        Debug.Log($"{myjson.crs})");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
