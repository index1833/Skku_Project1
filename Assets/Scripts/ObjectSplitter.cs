using UnityEngine;

public class ObjectSplitter : MonoBehaviour
{
    public LayerMask layerMask; // Raycast�� �浹�� ���̾ �����մϴ�.
    private GameObject selectedObject; // ���õ� ��ü�� �����ϱ� ���� ����

    void Update()
    {
        if (Input.GetMouseButtonDown(0)) // ���콺 ���� ��ư�� Ŭ���Ǿ����� Ȯ���մϴ�.
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition); // ī�޶󿡼� ���콺 ��ġ�� Ray�� �߻��մϴ�.
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, Mathf.Infinity, layerMask)) // Raycast�� �浹�� ���
            {
                if (selectedObject != null) // ������ ���õ� ��ü�� ������ ���� �����մϴ�.
                {
                    Debug.Log("���� ���� ������: " + selectedObject.name); // ���� ������ �����Ǿ����� ����մϴ�.
                }

                selectedObject = hit.collider.gameObject; // �浹�� ��ü�� �����մϴ�.
                Debug.Log("���õ�: " + selectedObject.name); // ���õ� ��ü�� �̸��� ����մϴ�.
            }
        }
    }
}
