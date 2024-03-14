using UnityEngine;
using UnityEditor;
using System.IO;
using CesiumForUnity;
using UnityEditor.ShaderGraph.Serialization;



public class GeoJSONLoaderWindow : EditorWindow
{
    private string filePath;
    private TextAsset geoJSONFile; // �ε��� GeoJSON ������ �����ϴ� ����

    [MenuItem("Window/GeoJSON Loader")]
    public static void ShowWindow()
    {
        GetWindow<GeoJSONLoaderWindow>("GeoJSON �δ�"); // ������ â �̸� ����
    }

    void OnGUI()
    {
        GUILayout.Label("GeoJSON ���� ����", EditorStyles.boldLabel); // Ÿ��Ʋ

        GUILayout.BeginHorizontal();
        GUILayout.Label("���� ���:", GUILayout.Width(80)); // ���� ��� ���̺�
        EditorGUILayout.TextField(filePath); // ���� ��� �ؽ�Ʈ �ʵ�
        if (GUILayout.Button("ã�ƺ���", GUILayout.Width(80))) // ã�ƺ��� ��ư
        {
            filePath = EditorUtility.OpenFilePanel("GeoJSON ���� ����", "", "geojson"); // ���� ���� ���̾�α� ����
        }
        GUILayout.EndHorizontal();

/*        if (GUILayout.Button("GeoJSON �ε�")) // GeoJSON �ε� ��ư
        {
            LoadGeoJSON(); // GeoJSON ���� �ε� �Լ� ȣ��
        }*/
    }

/*    private void LoadGeoJSON()
    {
        if (!string.IsNullOrEmpty(filePath)) // ���� ��ΰ� ������� ������
        {
            geoJSONFile = AssetDatabase.LoadAssetAtPath<TextAsset>(filePath); // GeoJSON ���� �ε�

            if (geoJSONFile != null) // ������ �ε�Ǿ�����
            {
                Debug.Log("GeoJSON �ε��: " + geoJSONFile.text);

                // JSON ���ڿ��� ��ü�� �Ľ�
                JSONObject jsonObject = new JSONObject(geoJSONFile.text);

                // "features" �迭���� geojson ��ü�� ����
                JSONArray features = jsonObject["features"].list;
                foreach (JSONObject feature in features)
                {
                    JSONObject geometry = feature["geometry"];
                    string type = geometry["type"].str;
                    if (type == "Polygon" || type == "MultiPolygon")
                    {
                        JSONArray coordinates = geometry["coordinates"].list;
                        foreach (JSONObject polygon in coordinates)
                        {
                            JSONArray coords = polygon.list[0].list; // ù ��° Polygon �Ǵ� MultiPolygon�� ��ǥ �迭
                            for (int i = 0; i < coords.Count; i++)
                            {
                                // ť�� ����
                                GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);

                                // CesiumGlobeAnchor ��ũ��Ʈ �߰�
                                CesiumGlobeAnchor cesiumGlobeAnchor = cube.AddComponent<CesiumGlobeAnchor>();

                                // ť���� ��ġ ���� (ù ��° ���� ���)
                                Vector3 position = new Vector3(coords[i][0].f, coords[i][1].f, coords[i][2].f);
                                cube.transform.position = position;

                                // ������ ���� ������ ��ǥ���� CesiumGlobeAnchor ��ũ��Ʈ�� �Ҵ�
                                cesiumGlobeAnchor._lastLocalPosition = position;
                            }
                        }
                    }
                }
            }*/
/*            else
            {
                Debug.LogError("GeoJSON ���� �ε� ����."); // ���� �ε� ���� �� ���� �޽��� ���
            }
        }
        else
        {
            Debug.LogError("���� ��ΰ� ��� �ֽ��ϴ�."); // ���� ��ΰ� ������� ��� ���� �޽��� ���
        }
    }*/
}
