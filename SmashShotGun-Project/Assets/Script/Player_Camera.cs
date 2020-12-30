using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Camera : MonoBehaviour
{
    float x;
    float y;
    float xRotation;

    public float xSen = 180f;
    public float ySen = 100f;
    public Transform orgn;
    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        x = Input.GetAxis("Mouse X") * xSen * Time.deltaTime;
        y = Input.GetAxis("Mouse Y") * ySen * Time.deltaTime;

        Mathf.Clamp(y, 0, 90);
        xRotation -= y;





        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        //transform.Rotate(Vector3.right * y);
        orgn.Rotate(Vector3.up*x);
        
    }
}
