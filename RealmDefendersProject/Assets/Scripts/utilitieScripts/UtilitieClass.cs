using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class UtilitieClass
{
    private static Camera mainCamera;
    public static Vector3 GetMouseWorldPosition() // This Function Returns the World Position of the Mouse
    {
        if (mainCamera == null)
        {
            mainCamera = Camera.main;
        }
        // Get Screen Position of the Mouse and Convert it to World Position, Set Z axis to 0
        Vector3 mouseWorldPosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        mouseWorldPosition.z = 0f;
        return mouseWorldPosition;
    }

    public static Vector3 GetRandomDirection()
    {
        return new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f)).normalized;
    }

    public static float GetAngleFromVector(Vector3 vector)
    {
        float radians = Mathf.Atan2(vector.y, vector.x);
        float degrees = radians * Mathf.Rad2Deg;
        return degrees;
    }

}
