using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class RBPlayer_Movement : MonoBehaviour
{

    public Rigidbody rb;
    public Transform groundCheck;
    public Transform CameraT;
    public float speed = 12f;

    public Vector3 gravity;
    float v_move;
    float h_move;
    Vector2 moveDirtion;
    Vector3 moveDir;
    public Vector3 vilocity;
    public bool isGrounded;
    public float isGroundedRadius = .3f;


    public int Jumpable;
    public float MinJumpHieght = 2f;
    public float JumpHieght = 5f;
    // int JumpAmount = 2;
    private bool fall;
    Vector3 FJumpPosition;
    Vector3 LJumpPosition;
    public float JumpBhold;


    public int crouchable;
    public bool Cboost;

    public InputActionAsset actionsAssests;
    InputActionMap onFootMap;
    InputAction moveAction;
    InputAction jumpAction;
    InputAction crouchAction;



    // Start is called before the first frame update


    void Start()
    {
       //  Pjum = JumpAmount;
        gravity = Physics.gravity;

        onFootMap = actionsAssests.FindActionMap("OnFoot");

        onFootMap.Enable();

        moveAction = onFootMap.FindAction("Move");

        jumpAction = onFootMap.FindAction("Jump");

        crouchAction = onFootMap.FindAction("Crouch");

        moveAction.performed += context => OnMove(context);
        moveAction.canceled += ctx => OnMove(ctx);

        jumpAction.performed += context => OnJump(context);
        jumpAction.canceled += ctx => OnJump(ctx);

        crouchAction.performed += context => OnCrouch(context);
        crouchAction.canceled += ctx => OnCrouch(ctx);
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
            vilocity.y = 0;
        }
        else
        {
            vilocity += gravity*2*Time.deltaTime ;
        }

        Jump();
        Crouch();
        rb.velocity = vilocity+moveDir;
        
        
    }



    private void OnMove(InputAction.CallbackContext context)
    {

        moveDirtion = context.ReadValue<Vector2>();
    }

    private void Move(Vector2 D)
    {
        v_move = D.y * speed ;
        h_move = D.x * speed ;

        moveDir = (transform.forward * v_move) + (transform.right * h_move);
    }


    void OnJump(InputAction.CallbackContext context)
    {
        
        if (context.performed)
        {
            Jumpable = 1;
            FJumpPosition = transform.position;
           // JumpAmount-=1;
          //  print(JumpAmount);

        }
        else if (context.canceled)
        {
            Jumpable = 0;
            JumpBhold = 0;

            fall = true;
            LJumpPosition = transform.position;
        }

        
    }

    void Jump()
    {
        

        //bool fall;
        if (Jumpable == 1)
        {
            JumpBhold -= Time.deltaTime;

            if (isGrounded == true)
            {
                vilocity.y += Mathf.Sqrt(JumpHieght * -2 * gravity.y);
                
            }
           
        }

        else if (Jumpable == 0)
        {
            
            LJumpPosition = transform.position;
            float DealtY=Mathf.Abs( FJumpPosition.y - LJumpPosition.y);
            
            if (DealtY >= MinJumpHieght && fall == true)
            {
                vilocity.y = 0;
                fall = false;
        
            }
            
        }
        //print(JumpBhold);
    }


    void OnCrouch (InputAction.CallbackContext context)
    {

        if (context.performed)
        {
            crouchable = 1;

        }
        else if (context.canceled)
        {
            crouchable = 0;
        }

    }

    void Crouch()
    {

        //power bosst
        if (isGrounded == false && !Cboost)
        {
            Cboost = true;
        }
        
        if (crouchable==1)
        {
            transform.localScale = new Vector3(1, .5f, 1);
           
            if (Cboost && isGrounded&& Jumpable==1)
            {
                Cboost = false;
                print("boost");
                vilocity.y += 120f;
            }

        }
        else
        {
           
            transform.localScale = new Vector3(1, 1, 1);
        }
    }
}
