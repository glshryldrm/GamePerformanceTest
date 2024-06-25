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
                renderer.enabled = true;  // Kamera görüþ açýsýnda olan nesneleri etkinleþtir
            }
            else
            {
                renderer.enabled = false; // Kamera görüþ açýsýnda olmayan nesneleri devre dýþý býrak
            }
        }
    }
}
