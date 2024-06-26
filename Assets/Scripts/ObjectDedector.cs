using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectDedector : MonoBehaviour
{
    public Camera mainCamera;
    private GUIStyle guiStyle = new GUIStyle(); // GUIStyle olu�tur
    private List<GameObject> seenObjects = new List<GameObject>();

    void Start()
    {
        // GUIStyle �zelliklerini ayarla
        guiStyle.fontSize = 30; // Yaz� boyutunu ayarla
        guiStyle.normal.textColor = Color.white; // Yaz� rengini ayarla
    }

    void Update()
    {
        takeSceneGameObjects();
        DisplaySeenObjectCount();
    }

    void takeSceneGameObjects()
    {
        seenObjects.Clear(); // Clear the list to avoid counting the same objects multiple times
        GameObject[] allObjects = FindObjectsOfType<GameObject>();
        Plane[] frustumPlanes = GeometryUtility.CalculateFrustumPlanes(mainCamera);

        foreach (GameObject obj in allObjects)
        {
            Renderer renderer = obj.GetComponent<Renderer>();
            if (renderer != null && GeometryUtility.TestPlanesAABB(frustumPlanes, renderer.bounds))
            {
                seenObjects.Add(obj);
            }
        }
    }

    void DisplaySeenObjectCount()
    {
        // Nesne say�s�n� ekrana yazd�rma
        Debug.Log("G�r�len nesne say�s�: " + seenObjects.Count);
    }

    void OnGUI()
    {
        // Ekrana nesne say�s�n� yazd�rma
        GUI.Label(new Rect(10, 10, 300, 30), "G�r�len nesne say�s�: " + seenObjects.Count, guiStyle);
    }
}
