using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Movement : MonoBehaviour
{

    public CharacterController CharacterController;
    public Transform CameraT;
    public float speed;
    float v_move;
    float h_move;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        v_move = Input.GetAxis("Vertical");
        h_move = Input.GetAxis("Horizontal");
        
        Vector3 moveDir = (transform.forward* v_move)+(transform.right*h_move);
        CharacterController.Move(moveDir * speed* Time.deltaTime);
    }
}
