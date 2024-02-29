using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json; // Newtonsoft.Json을 사용하여 JSON 파일을 읽습니다.
using CesiumForUnity;
using Unity.Mathematics;




// JSON 파일의 형식에 맞게 클래스를 정의합니다.
[System.Serializable]
public class BuildingData
{
    public string filename;
    public string longitude;
    public string latitude;
}

[System.Serializable]
public class BuildingList
{
    public List<BuildingData> buildinfo;
}

public class BuildingLoader : MonoBehaviour
{
    public GameObject buildingPrefab; // 프리팹으로 사용할 건물 오브젝트

    void Start()
    {
        // JSON 파일을 읽어서 문자열로 저장합니다.
        string jsonFilePath = Path.Combine(Application.streamingAssetsPath, "Buildings.json");
        string jsonString = File.ReadAllText(jsonFilePath);

        // JSON 문자열을 BuildingList 객체로 역직렬화합니다.
        BuildingList buildingList = JsonConvert.DeserializeObject<BuildingList>(jsonString);

        // BuildingList에서 각 BuildingData를 가져와 건물을 생성합니다.
        foreach (BuildingData buildingData in buildingList.buildinfo)
        {
            // Resources 폴더에서 obj 파일을 로드합니다.
            GameObject prefab = Resources.Load<GameObject>(buildingData.filename);
            if (prefab == null)
            {
                Debug.LogError("Prefab not found for filename: " + buildingData.filename);
                continue;
            }

            // 건물을 생성하고 위치를 설정합니다.
            GameObject building = Instantiate(prefab);
            building.transform.SetParent(transform); // 현재 스크립트를 붙인 게임 오브젝트를 부모로 설정합니다.

            // 건물의 위치를 설정합니다.
            CesiumGlobeAnchor globeAnchor = building.GetComponent<CesiumGlobeAnchor>();

            if (globeAnchor != null)
            {
                double latitude, longitude;

                if (double.TryParse(buildingData.latitude, out latitude) && double.TryParse(buildingData.longitude, out longitude))
                {
                    double3 double3Position = new double3(longitude, latitude, 0); // 위도와 경도를 설정합니다.
                    globeAnchor.longitudeLatitudeHeight = double3Position;
                    //globeAnchor.longitudeLatitudeHeight = new Vector3((float)longitude, (float)latitude, 0f); 
                }
                else
                {
                    Debug.LogError("Invalid latitude or longitude for building: " + buildingData.filename);
                }
            }
            else
            {
                Debug.LogError("CesiumGlobeAnchor component not found for building: " + buildingData.filename);
            }
        }
    }
}
