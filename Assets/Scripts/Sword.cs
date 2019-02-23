using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : MonoBehaviour
{
    private bool isRot = false;
    private Vector3 startRot;

    private int speed = 1000;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            startRot = this.transform.localEulerAngles;
            isRot = true;
        }

        if (isRot)
        {
            rotateSword();
        }

        if (this.transform.localEulerAngles.z < startRot.z && this.transform.localEulerAngles.z > 150)
        {
            this.transform.localEulerAngles = startRot;
            isRot = false;
        }
    }

    private void rotateSword()
    {
        this.transform.Rotate(new Vector3(0, 0, speed * Time.deltaTime));
    }
}
