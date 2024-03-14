using UnityEngine;
using SimpleJSON;
using System;
using System.Collections.Generic;

public class JsonReader1 : MonoBehaviour
{
    public TextAsset jsonFile; // GeoJSON 파일을 할당할 변수입니다.
    public Terrain terrainPrefab; // Terrain 프리팹을 할당할 변수입니다.

    private List<float> idList; // ID 값을 저장할 리스트 설정
    void Start()
    {
        idList = new List<float>();

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
            int id;
            if (!int.TryParse(properties["id"], out id))
            {
                Debug.LogError("Failed to parse terrain ID!");
                continue; // 다음 특징으로 넘어감
            }

            // ID를 리스트에 추가
            idList.Add(id);

            // Terrain의 위치 설정
   


            // Terrain 생성
            CreateTerrain(properties["left"].AsFloat, properties["top"].AsFloat, properties["right"].AsFloat, properties["bottom"].AsFloat, id);
        }
    }

    Terrain CreateTerrain(float left, float top, float right, float bottom, int id)
    {
        // Terrain 생성 및 위치, 크기 설정
        Terrain terrain = Instantiate(terrainPrefab, Vector3.zero, Quaternion.identity);

        // TerrainData 가져오기
        TerrainData terrainData = terrain.terrainData;

        // Terrain 위치 설정
        Vector3 position = new Vector3(0, 0, 0);
        terrain.transform.position = position;

        // Terrain 크기 설정
        float width = right - left;
        float length = top - bottom;
        terrainData.size = new Vector3(width, 1000f, length);

        // Terrain의 높이맵 설정 (여기서는 모두 0으로 설정)
        float[,] heights = new float[terrainData.heightmapResolution, terrainData.heightmapResolution];
        for (int x = 0; x < terrainData.heightmapResolution; x++)
        {
            for (int z = 0; z < terrainData.heightmapResolution; z++)
            {
                heights[x, z] = 0f; // 모두 0으로 설정
            }
        }
        terrainData.SetHeights(0, 0, heights);

        // Terrain 이름 설정
        terrain.name = "Terrain_ID_" + id.ToString();

        return terrain;
    }
}
