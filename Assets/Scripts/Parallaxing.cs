using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallaxing : MonoBehaviour {

    //array of backgrounds to parallax
    public Transform[] backgrounds;

    //proportion of cameras movement to move backgrounds by
    private float[] parallaxScales;

    //how smooth the parallax is going to be (0 >)
    public float smoothing = 1;

    //reference to main camera transform
    private Transform cam;

    //store position of camera in previous frame
    private Vector3 prevCamPos;

    private void Awake()
    {
        cam = Camera.main.transform;
    }

    private void Start()
    {
        prevCamPos = cam.position;

        parallaxScales = new float[backgrounds.Length];

        for(int i = 0; i < backgrounds.Length; i++)
        {
            parallaxScales[i] = backgrounds[i].position.z * -1;
        }
    }

    private void Update()
    {
        for(int i = 0; i < backgrounds.Length; i++)
        {
            float parallax = (prevCamPos.x - cam.position.x) * parallaxScales[i];

            float backgroundTargetPosX = backgrounds[i].position.x + parallax;

            Vector3 backgroundTargetPos = new Vector3(backgroundTargetPosX, backgrounds[i].position.y, backgrounds[i].position.z);

            backgrounds[i].position = Vector3.Lerp(backgrounds[i].position, backgroundTargetPos, smoothing * Time.deltaTime);
        }

        prevCamPos = cam.position;
    }
}
