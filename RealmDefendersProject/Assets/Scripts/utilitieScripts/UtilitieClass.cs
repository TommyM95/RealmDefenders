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
}
