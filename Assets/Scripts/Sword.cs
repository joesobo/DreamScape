using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : MonoBehaviour
{
    private bool isRot = false;
    private Vector3 startRot;

    public int speed = 10;
    public GameObject attackParticle;

    private void Start()
    {
        startRot = this.transform.localEulerAngles;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            isRot = true;
        }

        if (isRot)
        {
            rotateSword();
        }

        if((this.transform.localEulerAngles.z < 25)){
            Instantiate(attackParticle, this.transform.position, Quaternion.identity);
        }

        if (this.transform.localEulerAngles.z < startRot.z && this.transform.localEulerAngles.z > 150)
        {
            this.transform.localEulerAngles = startRot;
            isRot = false;
        }
    }

    private void rotateSword()
    {
        this.transform.Rotate(new Vector3(0, 0, 100 * speed * Time.deltaTime));
    }
}
