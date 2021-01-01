using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class Player_Movement : MonoBehaviour
{

    public CharacterController CharacterController;
    public Transform groundCheck;
    public Transform CameraT;
    public float speed = 12f;
    public bool Jumpable;
    public float JumpHieght = 5f;
    public int JumpAmount = 2;
    public Vector3 vilocity;
    public bool isGrounded;
    public float isGroundedRadius=.3f;

    public Vector3 gravity; 

    float v_move;
    float h_move;

    Vector2 moveDirtion;

    public InputActionAsset actionsAssests;
    InputActionMap onFootMap;
    InputAction moveAction;
    InputAction jumpAction;

    // Start is called before the first frame update


    void Start()
    {

        gravity = Physics.gravity;

        onFootMap = actionsAssests.FindActionMap("OnFoot");

        onFootMap.Enable();

        moveAction = onFootMap.FindAction("Move");

        jumpAction = onFootMap.FindAction("Jump");

        moveAction.performed += context => OnMove(context);
        moveAction.canceled += ctx => OnMove(ctx);

        jumpAction.performed += context => OnJump(context);
        jumpAction.canceled += ctx => OnJump(ctx);
    }

    // Update is called once per frame

    private void FixedUpdate()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, isGroundedRadius);
 
        
    }

    void Update()
    {
        Move(moveDirtion);
        
        if (isGrounded == true)
        {
            
            vilocity.y = -2f;
            

        }
        else
        {
            
            vilocity += gravity*2*Time.deltaTime;
        }
        Jump();
        CharacterController.Move(vilocity* Time.deltaTime);
    }



    private void OnMove(InputAction.CallbackContext context)
    {

        moveDirtion = context.ReadValue<Vector2>();
    }

    private void Move(Vector2 D)
    {
        v_move = D.y * speed * Time.deltaTime;
        h_move = D.x * speed * Time.deltaTime;

        Vector3 moveDir = (transform.forward * v_move) + (transform.right * h_move);
        CharacterController.Move(moveDir);

    }

    void OnJump(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            Jumpable = true;
           
        }
        else if (context.canceled)
        {
            Jumpable = false;
            
        }
    }

    void Jump()
    {
        if (Jumpable)
        {

            if (isGrounded == true)
                {
                vilocity.y += Mathf.Sqrt(JumpHieght * -2 * gravity.y);
                print(3);
                }
         
        }
    }
}
