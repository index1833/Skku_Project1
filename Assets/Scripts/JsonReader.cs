using UnityEngine;
using SimpleJSON;
using System;
using System.Collections.Generic;

public class JsonReader : MonoBehaviour
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
            float left, top, right, bottom, id;

            if (!float.TryParse(properties["left"], out left) ||
                !float.TryParse(properties["top"], out top) ||
                !float.TryParse(properties["right"], out right) ||
                !float.TryParse(properties["bottom"], out bottom) ||
                !float.TryParse(properties["id"], out id))
            {
                Debug.LogError("Failed to parse terrain properties!");
                continue; // 다음 특징으로 넘어감
            }

            // ID를 리스트에 추가
            idList.Add(id);

            //아래에 터레인 생성 코드 작성할 수 있음


            left = left / 1000;
            top = top / 1000;
            right = right / 1000;
            bottom = bottom / 1000;

            // \nLeft: " + left "\nid" + id
            // Debug.Log("Terrain Properties:\nid: " + id + "\nTop: " + top + "\nRight: " + right + "\nBottom: " + bottom + "\nleft" + left);

            // 유니티 좌표계에 맞게 변환
            // Vector3 position = new Vector3(left, 0, Screen.height - top); // 유니티 좌표계에 맞게 변환된 좌표생성
            // Terrain 생성
            // 유니티 좌표계에 맞게 변환된 위치와 크기로 Terrain 생성 
            Terrain terrain = CreateTerrain(left, top, right, bottom);
            // 유니티 좌표계에 맞게 변환
/*            float newX = (id / 611) + (left - right) / 2; // X 좌표 재조정
            float newZ = (id % 611) + (top - bottom) / 2; // Z 좌표 재조정*/
            // Terrain 이름 설정
            terrain.name = "Terrin_ID_" + id.ToString(); // ID 값을 이름으로 설정

            // 속성값 출력

            //Debug.Log("Terrain Properties:\nLeft: " + left + "\nTop: " + top + "\nRight: " + right + "\nBottom: " + bottom + "\nid" + id);

        }
/*        Debug.Log("ID List:");
        foreach (int id in idList)
        {
            Debug.Log(id);
        }*/

    }
    // int id = properties["id"].AsInt; // ID 값 가져오기
    Terrain CreateTerrain(float left, float top, float right, float bottom)
    {
        // Terrain 생성 및 위치, 크기 설정
        Terrain terrain = Instantiate(terrainPrefab, Vector3.zero, Quaternion.identity);

        // TerrainData 가져오기
        TerrainData terrainData = terrain.terrainData;

        // Terrain 위치 설정
        Vector3 position = new Vector3((left+right) /2 , 0, (top + bottom)/2 ); // 영역의 중심을 Terrain의 위치로 설정
        terrain.transform.position = position;

        // Terrain 크기 설정
        float width = right - left;
        float length = top - bottom;
        terrainData.size = new Vector3(width, 1000f, length); // Terrain의 크기를 조절하세요.

        /*      // Terrain의 높이맵 설정 (여기서는 모두 0으로 설정)
                float[,] heights = new float[terrainData.heightmapResolution, terrainData.heightmapResolution];

                // 높이값 설정
                float heightValue = 0f; // 원하는 높이값 설정
                for (int x = 0; x < terrainData.heightmapResolution; x++)
                {
                    for (int z = 0; z < terrainData.heightmapResolution; z++)
                    {
                        // 해당 지점의 높이값 설정
                        heights[x, z] = heightValue;
                    }
                }
                terrainData.SetHeights(0, 0, heights);*/
        return terrain;
    }
}