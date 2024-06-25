using System.Collections.Generic;
using UnityEngine;

public class VisibilityLimiter : MonoBehaviour
{
    public Camera mainCamera;

    void Update()
    {
        LimitVisibleObjects();
    }

    void LimitVisibleObjects()
    {
        Plane[] planes = GeometryUtility.CalculateFrustumPlanes(mainCamera);
        Renderer[] allRenderers = FindObjectsOfType<Renderer>();

        foreach (Renderer renderer in allRenderers)
        {
            if (GeometryUtility.TestPlanesAABB(planes, renderer.bounds))
            {
                renderer.enabled = true;  // Kamera g�r�� a��s�nda olan nesneleri etkinle�tir
            }
            else
            {
                renderer.enabled = false; // Kamera g�r�� a��s�nda olmayan nesneleri devre d��� b�rak
            }
        }
    }
}
