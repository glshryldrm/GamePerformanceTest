using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraTouchControl : MonoBehaviour
{
    public float rotateSpeed = 0.2f; // Kameran�n d�nd�rme h�z�
    public float pinchZoomSpeed = 0.5f; // Kameran�n zoom (ileri geri) h�z�
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
                        // Kameray� d�nd�r
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

                // Kameray� ileri veya geri hareket ettir
                transform.position = initialCameraPosition + transform.forward * pinchDifference * pinchZoomSpeed;

                // Kameran�n yeni pozisyonuna g�re g�r�� a��s�nda olan/olmayan nesneleri g�ncelle
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
                renderer.enabled = true;  // Kamera g�r�� a��s�nda olan nesneleri etkinle�tir
            }
            else
            {
                renderer.enabled = false; // Kamera g�r�� a��s�nda olmayan nesneleri devre d��� b�rak
            }
        }
    }
}
