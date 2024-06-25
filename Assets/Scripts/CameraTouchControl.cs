using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraTouchControl : MonoBehaviour
{
    public float rotateSpeed = 0.2f; // Kameranýn döndürme hýzý
    public float pinchZoomSpeed = 0.5f; // Kameranýn zoom (ileri geri) hýzý
    public Camera mainCamera;

    private Vector2 touchStartPos;
    private bool isDragging = false;
    private float initialPinchDistance;
    private Vector3 initialCameraPosition;

    void Update()
    {
        HandleTouchInput();
        LimitVisibleObjects();
    }

    void HandleTouchInput()
    {
        if (Input.touchCount == 1)
        {
            Touch touch = Input.GetTouch(0);
            switch (touch.phase)
            {
                case TouchPhase.Began:
                    touchStartPos = touch.position;
                    isDragging = true;
                    break;

                case TouchPhase.Moved:
                    if (isDragging)
                    {
                        Vector2 touchDelta = touch.position - touchStartPos;
                        // Kamerayý döndür
                        float rotationX = touchDelta.y * rotateSpeed;
                        float rotationY = touchDelta.x * rotateSpeed;
                        transform.eulerAngles += new Vector3(-rotationX, rotationY, 0);
                        touchStartPos = touch.position;
                    }
                    break;

                case TouchPhase.Ended:
                case TouchPhase.Canceled:
                    isDragging = false;
                    break;
            }
        }
        else if (Input.touchCount == 2)
        {
            Touch touch1 = Input.GetTouch(0);
            Touch touch2 = Input.GetTouch(1);

            if (touch1.phase == TouchPhase.Began || touch2.phase == TouchPhase.Began)
            {
                initialPinchDistance = Vector2.Distance(touch1.position, touch2.position);
                initialCameraPosition = transform.position;
            }
            else if (touch1.phase == TouchPhase.Moved || touch2.phase == TouchPhase.Moved)
            {
                float currentPinchDistance = Vector2.Distance(touch1.position, touch2.position);
                float pinchDifference = currentPinchDistance - initialPinchDistance;

                // Kamerayý ileri veya geri hareket ettir
                transform.position = initialCameraPosition + transform.forward * pinchDifference * pinchZoomSpeed;

                // Kameranýn yeni pozisyonuna göre görüþ açýsýnda olan/olmayan nesneleri güncelle
                LimitVisibleObjects();
            }
        }
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
