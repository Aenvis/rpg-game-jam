using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 5.0f;
    public float jumpForce = 2.0f;
    public float gravity = -9.8f;
    private Vector3 moveDirection = Vector3.zero;
    private CharacterController controller;
    public Animator animator;
    public string attackAnimationName = "Attack";

    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        float moveX = Input.GetAxis("Horizontal");
        float moveZ = Input.GetAxis("Vertical");
        Vector3 move = new Vector3(moveX, 0, moveZ);
        move *= speed;

        if (controller.isGrounded)
        {
            moveDirection = move;

            if (Input.GetButtonDown("Jump"))
            {
                moveDirection.y = jumpForce;
            }
        }
        else
        {
            moveDirection.x = move.x;
            moveDirection.z = move.z;
        }

        // Apply gravity
        moveDirection.y += gravity * Time.deltaTime;

        // Move the controller
        controller.Move(moveDirection * Time.deltaTime);

        // Attack
        if (Input.GetMouseButtonDown(0))
        {
            Attack();
        }
    }

    void Attack()
    {
        if (animator != null)
        {
            animator.Play(attackAnimationName);
        }
    }
}
