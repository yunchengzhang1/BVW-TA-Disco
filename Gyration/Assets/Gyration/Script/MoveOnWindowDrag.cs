using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveOnWindowDrag : MonoBehaviour
{
    private Vector3 previousMousePosition;
    private bool isDraggingWindow = false;

    void Start()
    {
        previousMousePosition = Input.mousePosition;
    }

    void Update()
    {
        // Detect if the application window is not focused (possibly being dragged)
        if (!Application.isFocused)
        {
            isDraggingWindow = true;
        }
        else if (Application.isFocused && isDraggingWindow)
        {
            // Once the window gains focus again, stop detecting window drag
            isDraggingWindow = false;
        }

        // If the window is being dragged, calculate mouse movement and move the object
        //if (isDraggingWindow)
        //{
        //    Vector3 currentMousePosition = Input.mousePosition;

        //    // Get the difference in mouse movement
        //    Vector3 mouseDelta = currentMousePosition - previousMousePosition;

        //    Vector3 movementDelta = new Vector3(mouseDelta.x, mouseDelta.y, 0) * Time.deltaTime;
        //    Debug.Log("movementDelta");

        //    // Move the GameObject by the mouse movement amount
        //    transform.position += movementDelta;

        //    // Update the previous mouse position
        //    previousMousePosition = currentMousePosition;
        //}

        Vector3 currentMousePosition = Input.mousePosition;

        // Get the difference in mouse movement
        Vector3 mouseDelta = currentMousePosition - previousMousePosition;

        Vector3 movementDelta = new Vector3(mouseDelta.x, mouseDelta.y, 0) * Time.deltaTime;
        Debug.Log("movementDelta");

        // Move the GameObject by the mouse movement amount
        transform.position += movementDelta;

        // Update the previous mouse position
        previousMousePosition = currentMousePosition;
    }
}
