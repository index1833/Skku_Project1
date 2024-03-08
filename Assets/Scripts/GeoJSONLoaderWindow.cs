using UnityEngine;
using UnityEditor;


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

        if (GUILayout.Button("GeoJSON �ε�")) // GeoJSON �ε� ��ư
        {
            LoadGeoJSON(); // GeoJSON ���� �ε� �Լ� ȣ��
        }
    }

    private void LoadGeoJSON()
    {
        if (!string.IsNullOrEmpty(filePath)) // ���� ��ΰ� ������� ������
        {
            int assetsIndex = filePath.IndexOf("Assets"); // ���� ��ο��� "Assets" ���ڿ��� �ε����� ã���ϴ�.

            if (assetsIndex != -1) // "Assets" ���ڿ��� ã�� ���
            {
                string assetPath = filePath.Substring(assetsIndex); // ���� ��ο��� "Assets" ���ڿ� ������ �κ��� �����Ͽ� Asset ��η� ����
                geoJSONFile = AssetDatabase.LoadAssetAtPath<TextAsset>(assetPath); // GeoJSON ���� �ε�

                if (geoJSONFile != null) // ������ �ε�Ǿ�����
                {
                    Debug.Log("GeoJSON �ε��: " + geoJSONFile.text);
                    // ���⼭ GeoJSON ������ �Ľ��ϰ� �ǹ��� �����ϴ� ���� �۾��� ������ �� �ֽ��ϴ�.
                }
                else
                {
                    Debug.LogError("GeoJSON ���� �ε� ����."); // ���� �ε� ���� �� ���� �޽��� ���
                }
            }
            else
            {
                Debug.LogError("���� ��ο� 'Assets' ���ڿ��� ã�� �� �����ϴ�."); // "Assets" ���ڿ��� ã�� �� ���� ��� ���� �޽��� ���
            }
        }
        else
        {
            Debug.LogError("���� ��ΰ� ��� �ֽ��ϴ�."); // ���� ��ΰ� ������� ��� ���� �޽��� ���
        }
    }

}
