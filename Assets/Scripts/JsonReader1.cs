using UnityEngine;
using SimpleJSON;
using System;
using System.Collections.Generic;

public class JsonReader1 : MonoBehaviour
{
    public TextAsset jsonFile; // GeoJSON ������ �Ҵ��� �����Դϴ�.
    public Terrain terrainPrefab; // Terrain �������� �Ҵ��� �����Դϴ�.

    private List<float> idList; // ID ���� ������ ����Ʈ ����
    void Start()
    {
        idList = new List<float>();

        if (jsonFile == null)
        {
            Debug.LogError("GeoJSON file is not assigned!");
            return;
        }

        // JSON ���� �б�
        string jsonString = jsonFile.text;

        // JSON �Ľ�
        JSONNode jsonData = JSON.Parse(jsonString);

        // features �迭�� ��� Ư¡�� ���� ó��
        JSONArray features = jsonData["features"].AsArray;
        foreach (JSONNode feature in features)
        {
            // �Ӽ� ��������
            JSONNode properties = feature["properties"];
            int id;
            if (!int.TryParse(properties["id"], out id))
            {
                Debug.LogError("Failed to parse terrain ID!");
                continue; // ���� Ư¡���� �Ѿ
            }

            // ID�� ����Ʈ�� �߰�
            idList.Add(id);

            // Terrain�� ��ġ ����
   


            // Terrain ����
            CreateTerrain(properties["left"].AsFloat, properties["top"].AsFloat, properties["right"].AsFloat, properties["bottom"].AsFloat, id);
        }
    }

    Terrain CreateTerrain(float left, float top, float right, float bottom, int id)
    {
        // Terrain ���� �� ��ġ, ũ�� ����
        Terrain terrain = Instantiate(terrainPrefab, Vector3.zero, Quaternion.identity);

        // TerrainData ��������
        TerrainData terrainData = terrain.terrainData;

        // Terrain ��ġ ����
        Vector3 position = new Vector3(0, 0, 0);
        terrain.transform.position = position;

        // Terrain ũ�� ����
        float width = right - left;
        float length = top - bottom;
        terrainData.size = new Vector3(width, 1000f, length);

        // Terrain�� ���̸� ���� (���⼭�� ��� 0���� ����)
        float[,] heights = new float[terrainData.heightmapResolution, terrainData.heightmapResolution];
        for (int x = 0; x < terrainData.heightmapResolution; x++)
        {
            for (int z = 0; z < terrainData.heightmapResolution; z++)
            {
                heights[x, z] = 0f; // ��� 0���� ����
            }
        }
        terrainData.SetHeights(0, 0, heights);

        // Terrain �̸� ����
        terrain.name = "Terrain_ID_" + id.ToString();

        return terrain;
    }
}
