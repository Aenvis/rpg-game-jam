using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class PlayerController : MonoBehaviour
{
    public CinemachineFreeLook freeLookCamera;
    public float speed = 6.0f;
    public float rotationSpeed = 5.0f; // Adjust this value to make rotation slower or faster
    public float jumpSpeed = 8.0f;
    public float gravity = 20.0f;

    private CharacterController characterController;
    private Vector3 moveDirection = Vector3.zero;
    private Animator animator; // Animator component reference

    void Start()
    {
        if (freeLookCamera == null)
        {
            Debug.LogError("Free Look Camera is not attached.");
        }

        characterController = GetComponent<CharacterController>();
        animator = GetComponent<Animator>(); // Get the Animator component
    }

    void Update()
    {
        // Get the input direction relative to the camera's rotation
        Vector3 direction = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        direction = Quaternion.Euler(0, freeLookCamera.m_XAxis.Value, 0) * direction;

        if (characterController.isGrounded)
        {
            // We are grounded, so recalculate move direction based on axes
            moveDirection = direction * speed;

            // Rotate the player to the direction of movement
            if (direction != Vector3.zero)
            {
                Quaternion toRotation = Quaternion.LookRotation(direction, Vector3.up);
                transform.rotation = Quaternion.Slerp(transform.rotation, toRotation, Time.deltaTime * rotationSpeed);
            }

            // Set the Speed parameter for the Animator
            animator.SetFloat("Speed", direction.magnitude);

            if (Input.GetButtonDown("Jump"))
            {
                moveDirection.y = jumpSpeed;
                // Set the Jump parameter for the Animator
                animator.SetBool("Jump", true);
            }
            else
            {
                // Reset the Jump parameter when not jumping
                animator.SetBool("Jump", false);
            }

            if (Input.GetMouseButtonDown(0)) // Assuming left mouse button for attack
            {
                // Set the Attack parameter for the Animator
                animator.SetBool("Attack", true);
            }
            else
            {
                // Reset the Attack parameter when not attacking
                animator.SetBool("Attack", false);
            }
        }
        else
        {
            // Apply gravity. Gravity is multiplied by deltaTime twice (once here, and once below
            // when the moveDirection is multiplied by deltaTime). This is because gravity should be applied
            // as an acceleration (ms^-2).
            moveDirection.y -= gravity * Time.deltaTime;

            // Reset the Jump parameter when in the air
            animator.SetBool("Jump", false);
        }

        // Move the controller
        characterController.Move(moveDirection * Time.deltaTime);
    }
}
