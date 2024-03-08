using UnityEngine;

public class ObjectSplitter : MonoBehaviour
{
    public LayerMask layerMask; // Raycast가 충돌할 레이어를 지정합니다.
    private GameObject selectedObject; // 선택된 객체를 저장하기 위한 변수

    void Update()
    {
        if (Input.GetMouseButtonDown(0)) // 마우스 왼쪽 버튼이 클릭되었는지 확인합니다.
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition); // 카메라에서 마우스 위치로 Ray를 발사합니다.
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, Mathf.Infinity, layerMask)) // Raycast가 충돌한 경우
            {
                if (selectedObject != null) // 이전에 선택된 객체가 있으면 선택 해제합니다.
                {
                    Debug.Log("이전 선택 해제됨: " + selectedObject.name); // 이전 선택이 해제되었음을 출력합니다.
                }

                selectedObject = hit.collider.gameObject; // 충돌한 객체를 선택합니다.
                Debug.Log("선택됨: " + selectedObject.name); // 선택된 객체의 이름을 출력합니다.
            }
        }
    }
}
