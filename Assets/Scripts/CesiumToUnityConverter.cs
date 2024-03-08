using UnityEngine;
using Unity.Mathematics;
using CesiumForUnity; // CesiumForUnity ���ӽ����̽��� ����մϴ�.

public class CesiumToUnityConverter : MonoBehaviour
{
   // CesiumGeoreference ������Ʈ�� ���� ����
    public CesiumGeoreference cesiumGeoreference;

    // ���� ��ǥ�� ����Ƽ ��ǥ�� ��ȯ�ϴ� �޼���
    public Vector3 ConvertCesiumToUnity(double3 cesiumPosition)
    {
        // CesiumGeoreference ������Ʈ�� �ʱ�ȭ�Ǿ����� Ȯ��
        if (cesiumGeoreference == null || !cesiumGeoreference.enabled)
        {
            Debug.LogError("CesiumGeoreference ������Ʈ�� ���ų� ��Ȱ��ȭ�Ǿ� �ֽ��ϴ�.");
            return Vector3.zero;
        }

        // Cesium ��ǥ�� Unity ��ǥ�� ��ȯ
        double3 ecefPosition = cesiumGeoreference.TransformEarthCenteredEarthFixedPositionToUnity(cesiumPosition);
        Vector3 unityPosition = new Vector3((float)ecefPosition.x, (float)ecefPosition.y, (float)ecefPosition.z);

        return unityPosition;
    }
}
