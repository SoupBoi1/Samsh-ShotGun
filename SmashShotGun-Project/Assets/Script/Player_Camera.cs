using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player_Camera : MonoBehaviour
{
    

    public float xSen = 180f;
    public float ySen = 100f;
    public Transform orgn;
    public Transform offest;
    public float mincamlp = -89;
    public float maxcamlp = 90;
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
        CemaraCollison();
        Look(LookD);
        //transform.position =  offest.position;
    }

    void OnLook(InputAction.CallbackContext context)
    {
        LookD = context.ReadValue<Vector2>();
    }

    void Look(Vector2 L)
    {
        x = L.x * xSen * Time.deltaTime;
        y = L.y * ySen * Time.deltaTime;

        y = Mathf.Clamp(y, mincamlp, maxcamlp);

        xRotation -= y;

        xRotation = Mathf.Clamp(xRotation, mincamlp, maxcamlp);

        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        //transform.Rotate(Vector3.right * y);
        orgn.Rotate(Vector3.up * x);
    }

    void CemaraCollison()
    {
        RaycastHit hit;
        if (Physics.Linecast(orgn.position,transform.position,out hit))
        {
            
            transform.position = hit.point;
        }
        else
        {
            transform.position = offest.position;
        }
    }
}
