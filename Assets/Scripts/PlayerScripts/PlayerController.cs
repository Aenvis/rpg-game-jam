using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class PlayerController : MonoBehaviour
{
    public CinemachineFreeLook freeLookCamera;
    public float speed = 6.0f;
    public float rotationSpeed = 5.0f;
    public float jumpSpeed = 8.0f;
    public float gravity = 20.0f;
    public float pickupRange = 5.0f;
    public float pickupAngle = 45.0f;
    public int pickupRays = 10;
    public LayerMask pickupLayer;     

    private CharacterController characterController;
    private Vector3 moveDirection = Vector3.zero;
    private Animator animator;
    private GameObject itemToPickUp;

    void Start()
    {
        if (freeLookCamera == null)
        {
            Debug.LogError("Free Look Camera is not attached.");
        }

        characterController = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        // Check if pickup or dead animation is playing
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("Pickup") || animator.GetCurrentAnimatorStateInfo(0).IsName("Dead"))
        {
            return; // Do not execute further code in the Update function
        }

        for (int i = 0; i < pickupRays; i++)
        {
            float angle = i * pickupAngle / (pickupRays - 1) - pickupAngle / 2;
            Vector3 pickupDirection = Quaternion.Euler(0, angle, 0) * transform.forward;

            // Debug draw the ray
            Debug.DrawRay(transform.position, pickupDirection * pickupRange, Color.red);

            if (Physics.Raycast(transform.position, pickupDirection, out RaycastHit hit, pickupRange, pickupLayer))
            {
                if (Input.GetKeyDown(KeyCode.E))
                {
                    animator.SetBool("Pickup", true);
                    //TODO Add code to pick up the item, e.g.:
                    //Destroy(hit.collider.gameObject);
                    itemToPickUp = hit.collider.gameObject;
                }
            }
        }

        Vector3 direction = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        direction = Quaternion.Euler(0, freeLookCamera.m_XAxis.Value, 0) * direction;

        // If grounded
        if (characterController.isGrounded)
        {
            moveDirection = new Vector3(direction.x * speed, moveDirection.y, direction.z * speed);
            moveDirection.y = 0f;

            if (direction != Vector3.zero)
            {
                Quaternion toRotation = Quaternion.LookRotation(direction, Vector3.up);
                transform.rotation = Quaternion.Slerp(transform.rotation, toRotation, Time.deltaTime * rotationSpeed);
            }

            animator.SetFloat("Speed", direction.magnitude);
        }

        if (Input.GetButtonDown("Jump") && characterController.isGrounded)
        {
            moveDirection.y = jumpSpeed;
            animator.SetBool("Jump", true);
        }
        else
        {
            animator.SetBool("Jump", false);
        }

        if (Input.GetMouseButtonDown(0)) // Assuming left mouse button for attack
        {
            animator.SetBool("Attack", true);
        }
        else
        {
            animator.SetBool("Attack", false);
        }
        if (Input.GetKeyDown(KeyCode.B))
        {
            animator.SetBool("Dead", true);
        }
        else
        {
            animator.SetBool("Dead", false);
        }

        moveDirection.y -= gravity * Time.deltaTime;
        characterController.Move(moveDirection * Time.deltaTime);
    }

    public void EndPickup()
    {
        if (itemToPickUp != null)
        {
            Destroy(itemToPickUp);
            itemToPickUp = null;
        }
        animator.SetBool("Pickup", false);
    }
}
