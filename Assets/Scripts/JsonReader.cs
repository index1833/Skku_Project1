using UnityEngine;
using SimpleJSON;
using System;


public class JsonReader : MonoBehaviour
{
    public TextAsset jsonFile; // GeoJSON 파일을 할당할 변수입니다.
    public Terrain terrainPrefab; // Terrain 프리팹을 할당할 변수입니다.
    public Vector3 position = new Vector3(1.5f, 0.5f, 2.7f); // x=1.5, y=0.5, z=2.7 위치에 오브젝트를 생성

    void Start()
    {
        if (jsonFile == null)
        {
            Debug.LogError("GeoJSON file is not assigned!");
            return;
        }

        // JSON 파일 읽기
        string jsonString = jsonFile.text;

        // JSON 파싱
        JSONNode jsonData = JSON.Parse(jsonString);

        // features 배열의 모든 특징에 대해 처리
        JSONArray features = jsonData["features"].AsArray;
        foreach (JSONNode feature in features)
        {
            // 속성 가져오기
            JSONNode properties = feature["properties"];
            float left, top, right, bottom;
            int id;
            if (!float.TryParse(properties["left"], out left) ||
                !float.TryParse(properties["top"], out top) ||
                !float.TryParse(properties["right"], out right) ||
                !float.TryParse(properties["bottom"], out bottom) ||
                !int.TryParse(properties["id"], out id))
            {
                Debug.LogError("Failed to parse terrain properties!");
                continue; // 다음 특징으로 넘어감
            }

            // 각 변수에 소수점 3번째 자리까지 나오도록 반올림
            left = (float)Math.Round(left, 3);
            top = (float)Math.Round(top, 3);
            right = (float)Math.Round(right, 3);
            bottom = (float)Math.Round(bottom, 3);
            left = left/1000;
            top = top /1000;
            right = right /1000;
            bottom = bottom/1000;

            Debug.Log("Terrain Properties:\nLeft: " + left + "\nTop: " + top + "\nRight: " + right + "\nBottom: " + bottom + "\nid" + id);

            // 유니티 좌표계에 맞게 변환
            // Vector3 position = new Vector3(left, 0, Screen.height - top); // 유니티 좌표계에 맞게 변환된 좌표생성
            // Terrain 생성
            // 유니티 좌표계에 맞게 변환된 위치와 크기로 Terrain 생성 
            // Terrain terrain = CreateTerrain(left, top, right - left, top - bottom);
            Terrain terrain = CreateTerrain(left, top, right, bottom);
            // Terrain 이름 설정
            terrain.name = "Terrin_ID_" + id.ToString(); // ID 값을 이름으로 설정

            // 속성값 출력
            
             //Debug.Log("Terrain Properties:\nLeft: " + left + "\nTop: " + top + "\nRight: " + right + "\nBottom: " + bottom + "\nid" + id);

        }     
    }
    // int id = properties["id"].AsInt; // ID 값 가져오기
    Terrain CreateTerrain(float left, float top, float right, float bottom)
    {
        // Terrain 생성 및 위치, 크기 설정
        Terrain terrain = Instantiate(terrainPrefab, Vector3.zero, Quaternion.identity);

        // TerrainData 가져오기
        TerrainData terrainData = terrain.terrainData;

        // Terrain 위치 설정

        Vector3 position = new Vector3((left + right) / 2, 0, (top + bottom) / 2); // 영역의 중심을 Terrain의 위치로 설정
        terrain.transform.position = position;

        // Terrain 크기 설정
        float width = right - left;
        float length = top - bottom;
        terrainData.size = new Vector3(width, 1000f, length); // Terrain의 크기를 조절하세요.

        // Terrain의 높이맵 설정 (여기서는 모두 0으로 설정)
        float[,] heights = new float[terrainData.heightmapResolution, terrainData.heightmapResolution];

        // 높이값 설정
        float heightValue = 0.5f; // 원하는 높이값 설정
        for (int x = 0; x < terrainData.heightmapResolution; x++)
        {
            for (int z = 0; z < terrainData.heightmapResolution; z++)
            {
                // 해당 지점의 높이값 설정
                heights[x, z] = heightValue;
            }
        }
        terrainData.SetHeights(0, 0, heights);
        return terrain;
    }
}