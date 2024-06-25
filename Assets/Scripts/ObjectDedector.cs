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
    private GUIStyle guiStyle = new GUIStyle(); // GUIStyle olu�tur

    void Start()
    {
        // GUIStyle �zelliklerini ayarla
        guiStyle.fontSize = 30; // Yaz� boyutunu ayarla
        guiStyle.normal.textColor = Color.white; // Yaz� rengini ayarla
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
        // Nesne say�s�n� ekrana yazd�rma
        Debug.Log("G�r�len nesne say�s�: " + seenObjects.Count);
    }

    void OnGUI()
    {
        // Ekrana nesne say�s�n� yazd�rma
        GUI.Label(new Rect(10, 10, 200, 20), "G�r�len nesne say�s�: " + seenObjects.Count, guiStyle);
    }
}
