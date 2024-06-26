using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectDedector : MonoBehaviour
{
    public Camera mainCamera;
    private GUIStyle guiStyle = new GUIStyle(); // GUIStyle oluþtur
    private List<GameObject> seenObjects = new List<GameObject>();

    void Start()
    {
        // GUIStyle özelliklerini ayarla
        guiStyle.fontSize = 30; // Yazý boyutunu ayarla
        guiStyle.normal.textColor = Color.white; // Yazý rengini ayarla
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
        // Nesne sayýsýný ekrana yazdýrma
        Debug.Log("Görülen nesne sayýsý: " + seenObjects.Count);
    }

    void OnGUI()
    {
        // Ekrana nesne sayýsýný yazdýrma
        GUI.Label(new Rect(10, 10, 300, 30), "Görülen nesne sayýsý: " + seenObjects.Count, guiStyle);
    }
}
