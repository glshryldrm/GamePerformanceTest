using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectDedector : MonoBehaviour
{
    public Camera mainCamera;
    public int horizontalRays = 10;
    public int verticalRays = 10;
    public float maxDistance = 100f;

    private HashSet<GameObject> seenObjects = new HashSet<GameObject>();
    private GUIStyle guiStyle = new GUIStyle(); // GUIStyle oluþtur

    void Start()
    {
        // GUIStyle özelliklerini ayarla
        guiStyle.fontSize = 30; // Yazý boyutunu ayarla
        guiStyle.normal.textColor = Color.white; // Yazý rengini ayarla
    }
    void Update()
    {
        RaycastFromCamera();
        DisplaySeenObjectCount();
    }

    void RaycastFromCamera()
    {
        seenObjects.Clear();

        for (int y = 0; y < verticalRays; y++)
        {
            for (int x = 0; x < horizontalRays; x++)
            {
                float u = (float)x / (horizontalRays - 1);
                float v = (float)y / (verticalRays - 1);

                Ray ray = mainCamera.ViewportPointToRay(new Vector3(u, v, 0));
                RaycastHit hit;

                if (Physics.Raycast(ray, out hit, maxDistance))
                {
                    seenObjects.Add(hit.collider.gameObject);
                }
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
        GUI.Label(new Rect(10, 10, 200, 20), "Görülen nesne sayýsý: " + seenObjects.Count, guiStyle);
    }
}
