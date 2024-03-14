using UnityEngine;
using UnityEditor;
using System.IO;
using CesiumForUnity;
using UnityEditor.ShaderGraph.Serialization;



public class GeoJSONLoaderWindow : EditorWindow
{
    private string filePath;
    private TextAsset geoJSONFile; // 로드한 GeoJSON 파일을 저장하는 변수

    [MenuItem("Window/GeoJSON Loader")]
    public static void ShowWindow()
    {
        GetWindow<GeoJSONLoaderWindow>("GeoJSON 로더"); // 에디터 창 이름 설정
    }

    void OnGUI()
    {
        GUILayout.Label("GeoJSON 파일 선택", EditorStyles.boldLabel); // 타이틀

        GUILayout.BeginHorizontal();
        GUILayout.Label("파일 경로:", GUILayout.Width(80)); // 파일 경로 레이블
        EditorGUILayout.TextField(filePath); // 파일 경로 텍스트 필드
        if (GUILayout.Button("찾아보기", GUILayout.Width(80))) // 찾아보기 버튼
        {
            filePath = EditorUtility.OpenFilePanel("GeoJSON 파일 열기", "", "geojson"); // 파일 선택 다이얼로그 열기
        }
        GUILayout.EndHorizontal();

/*        if (GUILayout.Button("GeoJSON 로드")) // GeoJSON 로드 버튼
        {
            LoadGeoJSON(); // GeoJSON 파일 로드 함수 호출
        }*/
    }

/*    private void LoadGeoJSON()
    {
        if (!string.IsNullOrEmpty(filePath)) // 파일 경로가 비어있지 않으면
        {
            geoJSONFile = AssetDatabase.LoadAssetAtPath<TextAsset>(filePath); // GeoJSON 파일 로드

            if (geoJSONFile != null) // 파일이 로드되었으면
            {
                Debug.Log("GeoJSON 로드됨: " + geoJSONFile.text);

                // JSON 문자열을 객체로 파싱
                JSONObject jsonObject = new JSONObject(geoJSONFile.text);

                // "features" 배열에서 geojson 객체들 추출
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
                            JSONArray coords = polygon.list[0].list; // 첫 번째 Polygon 또는 MultiPolygon의 좌표 배열
                            for (int i = 0; i < coords.Count; i++)
                            {
                                // 큐브 생성
                                GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);

                                // CesiumGlobeAnchor 스크립트 추가
                                CesiumGlobeAnchor cesiumGlobeAnchor = cube.AddComponent<CesiumGlobeAnchor>();

                                // 큐브의 위치 설정 (첫 번째 값만 사용)
                                Vector3 position = new Vector3(coords[i][0].f, coords[i][1].f, coords[i][2].f);
                                cube.transform.position = position;

                                // 마지막 값을 제외한 좌표값을 CesiumGlobeAnchor 스크립트에 할당
                                cesiumGlobeAnchor._lastLocalPosition = position;
                            }
                        }
                    }
                }
            }*/
/*            else
            {
                Debug.LogError("GeoJSON 파일 로드 실패."); // 파일 로드 실패 시 오류 메시지 출력
            }
        }
        else
        {
            Debug.LogError("파일 경로가 비어 있습니다."); // 파일 경로가 비어있을 경우 오류 메시지 출력
        }
    }*/
}
