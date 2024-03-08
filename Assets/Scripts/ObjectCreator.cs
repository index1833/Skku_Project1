using CesiumForUnity;
using UnityEngine;

public class ObjectCreator : MonoBehaviour
{
    public GameObject cesiumGeoreferenceObject; // CesiumGeoreference ���� ������Ʈ�� ������ ����

    void Start()
    {
        // CesiumGeoreference ���� ������Ʈ ã��
        if (cesiumGeoreferenceObject == null)
        {
            cesiumGeoreferenceObject = GameObject.Find("CesiumGeoreference");
            if (cesiumGeoreferenceObject == null)
            {
                Debug.LogError("CesiumGeoreference ���� ������Ʈ�� ã�� �� �����ϴ�.");
                return;
            }
        }

        // ť�� ����
        GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
        cube.transform.parent = cesiumGeoreferenceObject.transform; // ť�긦 CesiumGeoreference ���� ������Ʈ�� �ڽ����� ����
        cube.transform.localPosition = Vector3.zero; // ť���� ���� �������� �������� ����

        // CesiumGlobeAnchor ��ũ��Ʈ �߰�
        CesiumGlobeAnchor cesiumGlobeAnchor = cube.AddComponent<CesiumGlobeAnchor>();

        // CesiumGlobeAnchor ��ũ��Ʈ�� SetPositionLongitudeLatitudeHeight �޼��� ȣ��
        cesiumGlobeAnchor.SetPositionLongitudeLatitudeHeight(37.4078, 127.1029, 300);
    }
}
