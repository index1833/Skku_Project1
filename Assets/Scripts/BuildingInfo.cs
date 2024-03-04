using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;

public class JsonLoader : MonoBehaviour
{
    // JSON 파일 경로
    public string jsonFilePath;

    // Prefab 폴더에 있는 오브젝트(Prefab)
    public GameObject objPrefab;

    // JSON 파일 내의 데이터를 저장할 구조체 배열
    [System.Serializable]
    public class Buildjspec
    {
        public float latitude, longitude;
        public string fileName;
    }

    // JSON 파일 내의 데이터 배열
    public Buildjspec[] locationDataArray;

    void Start()
    {
        // JSON 파일을 읽어옴
        string jsonString = File.ReadAllText(jsonFilePath);
        locationDataArray = JsonUtility.FromJson<Buildjspec[]>(jsonString);

        // JSON 파일 내의 각 데이터에 대해 obj 오브젝트 생성
        foreach (Buildjspec data in locationDataArray)
        {
            // obj 파일 로드
            GameObject obj = Instantiate(objPrefab);

            // obj 위치 설정
            obj.transform.position = GetPositionFromLatLong(data.latitude, data.longitude);

            // 추가적인 설정
            // 예: obj.transform.rotation = Quaternion.identity; // 회전 설정

            // obj를 Hierarchy 창에 표시
            obj.transform.parent = this.transform;
        }
    }

    // 위도와 경도로부터 Unity 좌표로의 변환 함수
    Vector3 GetPositionFromLatLong(float latitude, float longitude)
    {
        // 여기에 위도, 경도를 Unity 좌표로 변환하는 코드 작성
        // 예: float x = longitudeToX(longitude);
        //     float z = latitudeToZ(latitude);
        //     return new Vector3(x, 0f, z);

        // 임시로 원점을 사용하여 위도와 경도를 3D 공간 좌표로 변환하는 방법을 사용합니다.
        return new Vector3(longitude, 0f, latitude);
    }
}