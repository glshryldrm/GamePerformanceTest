using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject parentObject;
    public Collider[] childsList;
    public Vector3 areaSize = new Vector3(10, 10, 10); // Çocuk nesnelerin daðýtýlacaðý alan boyutu

    void Start()
    {
        takeChildObject();
        RandomizeChildPositions();
    }
    private void Awake()
    {
        QualitySettings.vSyncCount = 0;
        Application.targetFrameRate = 60;
    }
    void takeChildObject()
    {
        childsList = parentObject.GetComponentsInChildren<Collider>();
    }

    void RandomizeChildPositions()
    {
        foreach (var child in childsList)
        {
            // Rastgele bir pozisyon belirle
            Vector3 randomPosition = new Vector3(
                Random.Range(-areaSize.x / 2, areaSize.x / 2),
                Random.Range(-areaSize.y / 2, areaSize.y / 2),
                Random.Range(-areaSize.z / 2, areaSize.z / 2)
            );

            // Child nesnenin pozisyonunu güncelle
            child.transform.localPosition = randomPosition;
        }
    }
}
