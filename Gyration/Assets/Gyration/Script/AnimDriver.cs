using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;
using System.Runtime.InteropServices;

public class AnimDriver : MonoBehaviour
{
    // Importing the GetCursorPos function from user32.dll
    [DllImport("user32.dll")]
    private static extern bool GetCursorPos(out POINT lpPoint);

    // Struct to hold the cursor's x and y position
    public struct POINT
    {
        public int x;
        public int y;
    }
    private POINT lastMousePosition;
    private Vector2 velocity;


    public bool consistent = false;
    public float spinSpeedConsist = 50f; 
    public float floatSpeedConsist = 1f;   
    public float floatHeightConsist = 0.2f;

    public float spinSpeedMax = 100f;
    public float spinSpeedMin = 20f;
    public float floatSpeedMax = 3f;
    public float floatSpeedMin = 1f;
    public float floatHeightMax = 0.5f;
    public float floatHeightMin = 0.1f;

    public Vector3 spinDirection = Vector3.forward;

    public float dragStrength = 0.2f;
    public float lerpSpeed = 0.1f;

    private Vector3 startPosition;
    private float mySpinSpeed;
    private float myFloatSpeed;
    private float myFloatHeight;

    private Vector3 previousMousePosition;
    private Vector3 lerpPosition; 
    private bool isDraggingWindow = true;
    

    void Start()
    {
        startPosition = transform.position;
        if ( consistent )
        {
            mySpinSpeed = spinSpeedConsist;
            myFloatSpeed = floatSpeedConsist;
            myFloatHeight = floatHeightConsist;
        }
        else
        {
            mySpinSpeed = Random.Range(spinSpeedMin, spinSpeedMax);
            myFloatSpeed = Random.Range(floatSpeedMax, floatSpeedMax);
            myFloatHeight = Random.Range(floatHeightMin, floatHeightMax);
        }
        previousMousePosition = Input.mousePosition;
    }

    //void OnApplicationFocus(bool focus)
    //{
    //    if (!focus)
    //    {
    //        isDraggingWindow = true;
    //        mySpinSpeed = spinSpeedMax;
    //    }
    //    else if (focus)
    //    {
    //        // Once the window gains focus again, stop detecting window drag
    //        isDraggingWindow = false;
    //        mySpinSpeed = spinSpeedConsist;
    //    }
    //}



    void FixedUpdate()
    {
        // S P I N
        transform.Rotate(spinDirection, mySpinSpeed * Time.deltaTime);
        // G L I D E
        float newY = Mathf.Sin(Time.time * myFloatSpeed) * myFloatHeight;
        // D R A G
        //if (!Application.isFocused)
        //{
        //    isDraggingWindow = true;
        //    mySpinSpeed = spinSpeedMax;
        //}
        //else if (Application.isFocused && isDraggingWindow)
        //{
        //    // Once the window gains focus again, stop detecting window drag
        //    isDraggingWindow = false;
        //    mySpinSpeed = spinSpeedMin;
        //}
        POINT point;
        if (isDraggingWindow && GetCursorPos(out point))
        {
            POINT currentMousePosition;
            GetCursorPos(out currentMousePosition);

            // Calculate the current mouse position
            Vector2 currentMousePos = new Vector2(currentMousePosition.x, currentMousePosition.y);
            Vector2 lastMousePos = new Vector2(lastMousePosition.x, lastMousePosition.y);

            // Calculate the change in position (movement)
            Vector2 movement = currentMousePos - lastMousePos;

            // Calculate velocity as movement per second
            velocity = movement / Time.deltaTime;

            // Update last mouse position
            lastMousePosition = currentMousePosition;


            //Vector3 currentMousePosition = Input.mousePosition;

            //// Get the difference in mouse movement
            //Vector3 mouseDelta = currentMousePosition - previousMousePosition;

            Vector3 movementDelta = new Vector3(velocity.x * (-1), velocity.y , 0) * Time.deltaTime * dragStrength ;
            lerpPosition = Vector3.MoveTowards(transform.position, movementDelta, lerpSpeed * Time.deltaTime);
            // Update the previous mouse position
            //previousMousePosition = currentMousePosition;

            transform.position = new Vector3(startPosition.x, startPosition.y + newY, startPosition.z) + lerpPosition;
        }
        else
        {
            transform.position = new Vector3(startPosition.x, startPosition.y + newY, startPosition.z);
        }


        
    }
}
