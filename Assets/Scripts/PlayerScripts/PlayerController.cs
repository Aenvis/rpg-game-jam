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

        // Check if the character is grounded
        if (controller.isGrounded)
        {
            // If grounded, we allow to set the jump force
            if (Input.GetButtonDown("Jump"))
            {
                moveDirection.y = jumpForce;
            }
            else
            {
                // Apply a small downward force to keep player grounded
                moveDirection.y = -0.5f;
            }
        }
        else
        {
            // Apply gravity when not grounded
            moveDirection.y += gravity * Time.deltaTime;
        }

        // Set horizontal movement
        moveDirection.x = move.x;
        moveDirection.z = move.z;

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
