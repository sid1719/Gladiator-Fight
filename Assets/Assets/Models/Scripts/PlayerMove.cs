using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    private CharacterAnim playerAnimations;
    private CharacterController charController;
    public float movement_speed = 3f;

    public float gravity = 9.8f;
    public float rotation_speed=0.15f;
    public float rotateDegreesPerSecond = 180f;
    // Start is called before the first frame update
    void Awake()
    {
        charController = GetComponent<CharacterController>();
        playerAnimations = GetComponent<CharacterAnim>();
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        Rotate();
        Animatewalk();
    }

    private void Move()
    {
        if(Input.GetAxis(Axis.VERTICAL_AXIS)>0)
        {
            Vector3 moveDirection = transform.forward;
            moveDirection.y -= gravity * Time.deltaTime;

            charController.Move(moveDirection * movement_speed * Time.deltaTime);
        }
        else if(Input.GetAxis(Axis.VERTICAL_AXIS)<0)
        {
            Vector3 moveDirection = transform.forward * -1;
            moveDirection.y -= gravity * Time.deltaTime;

            charController.Move(moveDirection * movement_speed * Time.deltaTime);
        }
        else
        {
            charController.Move(Vector3.zero);
        }
    }

   void Rotate()
    {
        Vector3 rotation_Direction = Vector3.zero;

        if(Input.GetAxis(Axis.HORIZONTAL_AXIS)<0)
        {
            rotation_Direction = transform.TransformDirection(Vector3.left);
        }
        if(Input.GetAxis(Axis.HORIZONTAL_AXIS)>0)
        {
            rotation_Direction = transform.TransformDirection(Vector3.right);
        }

        if(rotation_Direction!=Vector3.zero)
        {
            transform.rotation = Quaternion.RotateTowards(
                transform.rotation,Quaternion.LookRotation(rotation_Direction),rotateDegreesPerSecond/2 * Time.deltaTime);
            
        }
    }
    void Animatewalk()
    {
// print("The velocity is " + charController.velocity.sqrMagnitude);
        if(charController.velocity.sqrMagnitude!=0)
        {
            playerAnimations.Walk(true);
        }
        else
        {
            playerAnimations.Walk(false);
        }
    }
}
