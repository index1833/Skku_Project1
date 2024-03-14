using UnityEngine;
using SimpleJSON;
using System;
using System.Collections.Generic;

public class JsonReader : MonoBehaviour
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
            float left, top, right, bottom, id;

            if (!float.TryParse(properties["left"], out left) ||
                !float.TryParse(properties["top"], out top) ||
                !float.TryParse(properties["right"], out right) ||
                !float.TryParse(properties["bottom"], out bottom) ||
                !float.TryParse(properties["id"], out id))
            {
                Debug.LogError("Failed to parse terrain properties!");
                continue; // ���� Ư¡���� �Ѿ
            }

            // ID�� ����Ʈ�� �߰�
            idList.Add(id);

            //�Ʒ��� �ͷ��� ���� �ڵ� �ۼ��� �� ����


            left = left / 1000;
            top = top / 1000;
            right = right / 1000;
            bottom = bottom / 1000;

            // \nLeft: " + left "\nid" + id
            // Debug.Log("Terrain Properties:\nid: " + id + "\nTop: " + top + "\nRight: " + right + "\nBottom: " + bottom + "\nleft" + left);

            // ����Ƽ ��ǥ�迡 �°� ��ȯ
            // Vector3 position = new Vector3(left, 0, Screen.height - top); // ����Ƽ ��ǥ�迡 �°� ��ȯ�� ��ǥ����
            // Terrain ����
            // ����Ƽ ��ǥ�迡 �°� ��ȯ�� ��ġ�� ũ��� Terrain ���� 
            Terrain terrain = CreateTerrain(left, top, right, bottom);
            // ����Ƽ ��ǥ�迡 �°� ��ȯ
/*            float newX = (id / 611) + (left - right) / 2; // X ��ǥ ������
            float newZ = (id % 611) + (top - bottom) / 2; // Z ��ǥ ������*/
            // Terrain �̸� ����
            terrain.name = "Terrin_ID_" + id.ToString(); // ID ���� �̸����� ����

            // �Ӽ��� ���

            //Debug.Log("Terrain Properties:\nLeft: " + left + "\nTop: " + top + "\nRight: " + right + "\nBottom: " + bottom + "\nid" + id);

        }
/*        Debug.Log("ID List:");
        foreach (int id in idList)
        {
            Debug.Log(id);
        }*/

    }
    // int id = properties["id"].AsInt; // ID �� ��������
    Terrain CreateTerrain(float left, float top, float right, float bottom)
    {
        // Terrain ���� �� ��ġ, ũ�� ����
        Terrain terrain = Instantiate(terrainPrefab, Vector3.zero, Quaternion.identity);

        // TerrainData ��������
        TerrainData terrainData = terrain.terrainData;

        // Terrain ��ġ ����
        Vector3 position = new Vector3((left+right) /2 , 0, (top + bottom)/2 ); // ������ �߽��� Terrain�� ��ġ�� ����
        terrain.transform.position = position;

        // Terrain ũ�� ����
        float width = right - left;
        float length = top - bottom;
        terrainData.size = new Vector3(width, 1000f, length); // Terrain�� ũ�⸦ �����ϼ���.

        /*      // Terrain�� ���̸� ���� (���⼭�� ��� 0���� ����)
                float[,] heights = new float[terrainData.heightmapResolution, terrainData.heightmapResolution];

                // ���̰� ����
                float heightValue = 0f; // ���ϴ� ���̰� ����
                for (int x = 0; x < terrainData.heightmapResolution; x++)
                {
                    for (int z = 0; z < terrainData.heightmapResolution; z++)
                    {
                        // �ش� ������ ���̰� ����
                        heights[x, z] = heightValue;
                    }
                }
                terrainData.SetHeights(0, 0, heights);*/
        return terrain;
    }
}