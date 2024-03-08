using CesiumForUnity;
using UnityEngine;

public class ObjectCreator : MonoBehaviour
{
    public GameObject cesiumGeoreferenceObject; // CesiumGeoreference 게임 오브젝트를 설정할 변수

    void Start()
    {
        // CesiumGeoreference 게임 오브젝트 찾기
        if (cesiumGeoreferenceObject == null)
        {
            cesiumGeoreferenceObject = GameObject.Find("CesiumGeoreference");
            if (cesiumGeoreferenceObject == null)
            {
                Debug.LogError("CesiumGeoreference 게임 오브젝트를 찾을 수 없습니다.");
                return;
            }
        }

        // 큐브 생성
        GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
        cube.transform.parent = cesiumGeoreferenceObject.transform; // 큐브를 CesiumGeoreference 게임 오브젝트의 자식으로 설정
        cube.transform.localPosition = Vector3.zero; // 큐브의 로컬 포지션을 원점으로 설정

        // CesiumGlobeAnchor 스크립트 추가
        CesiumGlobeAnchor cesiumGlobeAnchor = cube.AddComponent<CesiumGlobeAnchor>();

        // CesiumGlobeAnchor 스크립트의 SetPositionLongitudeLatitudeHeight 메서드 호출
        cesiumGlobeAnchor.SetPositionLongitudeLatitudeHeight(37.4078, 127.1029, 300);
    }
}
