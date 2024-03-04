using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json; // Newtonsoft.Json�� ����Ͽ� JSON ������ �н��ϴ�.
using CesiumForUnity;
using Unity.Mathematics;




// JSON ������ ���Ŀ� �°� Ŭ������ �����մϴ�.
[System.Serializable]
public class BuildingData
{
    public string filename;
    public string longitude;
    public string latitude;
}

[System.Serializable]
public class BuildingList
{   // List<BuildingData>
    public BuildingData[] buildinfo;
}

public class BuildingLoader : MonoBehaviour
{
    public GameObject buildingPrefab; // ���������� ����� �ǹ� ������Ʈ

    void Start()
    {
        // JSON ������ �о ���ڿ��� �����մϴ�.
        string jsonFilePath = Path.Combine(Application.streamingAssetsPath, "Buildings.json");
        string jsonString = File.ReadAllText(jsonFilePath);

        // JSON ���ڿ��� BuildingList ��ü�� ������ȭ�մϴ�.
        BuildingList buildingList = JsonConvert.DeserializeObject<BuildingList>(jsonString);

        // BuildingList���� �� BuildingData�� ������ �ǹ��� �����մϴ�.
        foreach (BuildingData buildingData in buildingList.buildinfo)
        {
            // JSON ���Ͽ��� ���� latitude�� longitude ���� ����� �α׷� ����մϴ�.
            Debug.Log("Latitude: " + buildingData.latitude + ", Longitude: " + buildingData.longitude);
            // Resources �������� obj ������ �ε��մϴ�.
            GameObject prefab = Resources.Load<GameObject>(buildingData.filename);
            if (prefab == null)
            {
                Debug.LogError("Prefab not found for filename: " + buildingData.filename);
                continue;
            }

            // �ǹ��� �����ϰ� ��ġ�� �����մϴ�.
            GameObject building = Instantiate(prefab);
            building.transform.SetParent(transform); // ���� ��ũ��Ʈ�� ���� ���� ������Ʈ�� �θ�� �����մϴ�.
            

            // �ǹ��� ��ġ�� �����մϴ�.
            CesiumGlobeAnchor globeAnchor = building.GetComponent<CesiumGlobeAnchor>();

            if (globeAnchor != null)
            {
                double latitude, longitude;
                if (double.TryParse(buildingData.latitude, out latitude) && double.TryParse(buildingData.longitude, out longitude))
                {
                    // Unity ��ǥ�� ��ȯ�� �� CesiumGlobeAnchor�� �����մϴ�.
                    building.transform.rotation = UnityEngine.Quaternion.Euler(new UnityEngine.Vector3(0,0,0));
                    Vector3 unityCoordinates = new Vector3((float)longitude, 0f, (float)latitude);
                    double3 double3Position = new double3(longitude, latitude, 0); // ������ �浵�� �����մϴ�.
                    globeAnchor.longitudeLatitudeHeight = double3Position;
                   
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
    // Unity ��ǥ�� Cesium ��ǥ�� ��ȯ�մϴ�.
    private Vector3 UnityToCesiumCoordinates(Vector3 unityCoordinates)
    {
        // Unity�� y ��ǥ�� Cesium�� z ��ǥ�� ����մϴ�.
        return new Vector3(unityCoordinates.x, unityCoordinates.z, unityCoordinates.y);
    }
}


