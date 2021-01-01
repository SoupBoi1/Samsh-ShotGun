using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player_Camera : MonoBehaviour
{
    

    public float xSen = 180f;
    public float ySen = 100f;
    public Transform orgn;

    
    float x;
    float y;
    float xRotation;
    Vector2 LookD;

    public InputActionAsset actionsAssests;
    InputActionMap onFootMap;
    InputAction lookAction;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;


        onFootMap = actionsAssests.FindActionMap("OnFoot");

        onFootMap.Enable();

        lookAction = onFootMap.FindAction("look");

        lookAction.performed += context => OnLook(context);
        lookAction.canceled += canx => OnLook(canx);

    }

    // Update is called once per frame
    void Update()
    {
        Look(LookD);
    }

    void OnLook(InputAction.CallbackContext context)
    {
        LookD = context.ReadValue<Vector2>();
    }

    void Look(Vector2 L)
    {
        x = L.x * xSen * Time.deltaTime;
        y = L.y * ySen * Time.deltaTime;

        
        xRotation -= y;
        Mathf.Clamp(xRotation, 0, 90);
        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        //transform.Rotate(Vector3.right * y);
        orgn.Rotate(Vector3.up * x);
    }

}
