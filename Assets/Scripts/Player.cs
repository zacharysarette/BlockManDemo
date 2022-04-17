using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class Player : MonoBehaviour
{
    [SerializeField]
    private CharacterController controller;
    [SerializeField]
    private Animator animator;
    private Vector3 direction = Vector3.zero;
    [SerializeField]
    public float gravity = 20.0f;
    public float jumpForce = 10.0f;
    public float speed = 50f;
    public float turnSpeed = 50f;
    public GameObject faceRun;
    public GameObject faceJump;
    public GameObject faceIdle;

    private enum animType {
        idle,
        run,
        jump
    }

    // Update is called once per frame
    void Update()
    {
        if (controller.isGrounded)
        {
            if (Input.GetKey("up"))
            {
                direction = transform.forward * Input.GetAxis("Vertical") * speed;
                float turn = Input.GetAxis("Horizontal");
                transform.Rotate(0,turn * turnSpeed * Time.deltaTime,0);
                setAnim(animType.run);
            }
            else
            {
                direction = transform.forward * Input.GetAxis("Vertical") * 0;
                float turn = Input.GetAxis("Horizontal");
                transform.Rotate(0,turn * turnSpeed * Time.deltaTime,0);
                setAnim(animType.idle);
            }
            if (Input.GetButton("Jump"))
            {
                setAnim(animType.jump);
                direction.y = jumpForce;
            }
        }

        controller.Move(direction * Time.deltaTime);
        direction.y -= gravity * Time.deltaTime;
    }


    private void setFace(animType type) {
        faceIdle.SetActive(type == animType.idle);
        faceRun.SetActive(type == animType.run);
        faceJump.SetActive(type == animType.jump);
    }

    private void setAnim(animType type) {
        animator.SetInteger("AnimParam", (int)type);
        setFace(type);
    }
}
