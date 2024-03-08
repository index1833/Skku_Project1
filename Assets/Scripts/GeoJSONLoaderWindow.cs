using UnityEngine;
using UnityEditor;


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

        if (GUILayout.Button("GeoJSON 로드")) // GeoJSON 로드 버튼
        {
            LoadGeoJSON(); // GeoJSON 파일 로드 함수 호출
        }
    }

    private void LoadGeoJSON()
    {
        if (!string.IsNullOrEmpty(filePath)) // 파일 경로가 비어있지 않으면
        {
            int assetsIndex = filePath.IndexOf("Assets"); // 파일 경로에서 "Assets" 문자열의 인덱스를 찾습니다.

            if (assetsIndex != -1) // "Assets" 문자열을 찾은 경우
            {
                string assetPath = filePath.Substring(assetsIndex); // 파일 경로에서 "Assets" 문자열 이후의 부분을 추출하여 Asset 경로로 설정
                geoJSONFile = AssetDatabase.LoadAssetAtPath<TextAsset>(assetPath); // GeoJSON 파일 로드

                if (geoJSONFile != null) // 파일이 로드되었으면
                {
                    Debug.Log("GeoJSON 로드됨: " + geoJSONFile.text);
                    // 여기서 GeoJSON 파일을 파싱하고 건물을 생성하는 등의 작업을 수행할 수 있습니다.
                }
                else
                {
                    Debug.LogError("GeoJSON 파일 로드 실패."); // 파일 로드 실패 시 오류 메시지 출력
                }
            }
            else
            {
                Debug.LogError("파일 경로에 'Assets' 문자열을 찾을 수 없습니다."); // "Assets" 문자열을 찾을 수 없는 경우 오류 메시지 출력
            }
        }
        else
        {
            Debug.LogError("파일 경로가 비어 있습니다."); // 파일 경로가 비어있을 경우 오류 메시지 출력
        }
    }

}
