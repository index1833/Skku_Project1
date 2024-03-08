using UnityEngine;
using Unity.Mathematics;
using CesiumForUnity; // CesiumForUnity 네임스페이스를 사용합니다.

public class CesiumToUnityConverter : MonoBehaviour
{
   // CesiumGeoreference 컴포넌트에 대한 참조
    public CesiumGeoreference cesiumGeoreference;

    // 세슘 좌표를 유니티 좌표로 변환하는 메서드
    public Vector3 ConvertCesiumToUnity(double3 cesiumPosition)
    {
        // CesiumGeoreference 컴포넌트가 초기화되었는지 확인
        if (cesiumGeoreference == null || !cesiumGeoreference.enabled)
        {
            Debug.LogError("CesiumGeoreference 컴포넌트가 없거나 비활성화되어 있습니다.");
            return Vector3.zero;
        }

        // Cesium 좌표를 Unity 좌표로 변환
        double3 ecefPosition = cesiumGeoreference.TransformEarthCenteredEarthFixedPositionToUnity(cesiumPosition);
        Vector3 unityPosition = new Vector3((float)ecefPosition.x, (float)ecefPosition.y, (float)ecefPosition.z);

        return unityPosition;
    }
}
