using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightController : MonoBehaviour
{
    //array of lights
    public GameObject[] Lights; 
    //consistent intervals between toggle
    public float ToggleInterval = 0.5f;

    private int currLightIndex = 0;
    private int loopCounter = 0;

    void Start()
    {
        // Start the toggling coroutine
        Invoke("DelayTrigger", 4f);
    }

    private void DelayTrigger()
    {
        StartCoroutine(ToggleObjects());
    }

    IEnumerator ToggleObjects()
    {
        while (true)
        {
            // Wait for the specified interval before toggling again
            yield return new WaitForSeconds(ToggleInterval);

            //switch old light with new light
            Lights[currLightIndex].SetActive(false); 
            loopCounter++;
            currLightIndex = loopCounter % Lights.Length;
            Lights[currLightIndex].SetActive(true);


        }
    }
}
