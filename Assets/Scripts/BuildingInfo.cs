using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;

public class JsonLoader : MonoBehaviour
{
    // JSON ���� ���
    public string jsonFilePath;

    // Prefab ������ �ִ� ������Ʈ(Prefab)
    public GameObject objPrefab;

    // JSON ���� ���� �����͸� ������ ����ü �迭
    [System.Serializable]
    public class Buildjspec
    {
        public float latitude, longitude;
        public string fileName;
    }

    // JSON ���� ���� ������ �迭
    public Buildjspec[] locationDataArray;

    void Start()
    {
        // JSON ������ �о��
        string jsonString = File.ReadAllText(jsonFilePath);
        locationDataArray = JsonUtility.FromJson<Buildjspec[]>(jsonString);

        // JSON ���� ���� �� �����Ϳ� ���� obj ������Ʈ ����
        foreach (Buildjspec data in locationDataArray)
        {
            // obj ���� �ε�
            GameObject obj = Instantiate(objPrefab);

            // obj ��ġ ����
            obj.transform.position = GetPositionFromLatLong(data.latitude, data.longitude);

            // �߰����� ����
            // ��: obj.transform.rotation = Quaternion.identity; // ȸ�� ����

            // obj�� Hierarchy â�� ǥ��
            obj.transform.parent = this.transform;
        }
    }

    // ������ �浵�κ��� Unity ��ǥ���� ��ȯ �Լ�
    Vector3 GetPositionFromLatLong(float latitude, float longitude)
    {
        // ���⿡ ����, �浵�� Unity ��ǥ�� ��ȯ�ϴ� �ڵ� �ۼ�
        // ��: float x = longitudeToX(longitude);
        //     float z = latitudeToZ(latitude);
        //     return new Vector3(x, 0f, z);

        // �ӽ÷� ������ ����Ͽ� ������ �浵�� 3D ���� ��ǥ�� ��ȯ�ϴ� ����� ����մϴ�.
        return new Vector3(longitude, 0f, latitude);
    }
}