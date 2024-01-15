using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class FPSDisplay : MonoBehaviour
{

    int frameCount;
    float prevTime;
    float fps;

    void Start()
    {
        // ïœêîÇÃèâä˙âª
        frameCount = 0;
        prevTime = 0.0f;
    }

    // Update is called once per frame
    void Update()
    {
        frameCount++;
        float time = Time.realtimeSinceStartup - prevTime;

        if (time >= 0.5f)
        {
            fps = frameCount / time;
            Debug.Log(fps);

            frameCount = 0;
            prevTime = Time.realtimeSinceStartup;
        }
    }

    // ï\é¶èàóù
    private void OnGUI()
    {
        GUILayout.Label(fps.ToString());
    }
}
