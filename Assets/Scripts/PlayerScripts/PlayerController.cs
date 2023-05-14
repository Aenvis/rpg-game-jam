using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using DefaultNamespace;

public class PlayerController : MonoBehaviour
{
    public CinemachineFreeLook freeLookCamera;
    public float speed = 6.0f;
    public float rotationSpeed = 5.0f;
    public float jumpSpeed = 8.0f;
    public float gravity = 20.0f;
    public float pickupRange = 2.0f;
    public float pickupAngle = 45.0f;
    public int pickupRays = 10;
    public int verticalPickupRays = 10;
    public float rayOriginHeight = 1.0f;
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
        GameManager.Instance.EndGameEvent.AddListener(KillPlayer);

    }

    void Update()
    {
        AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0);
        // Check if pickup or dead animation is playing
        if (stateInfo.IsName("Pickup") || stateInfo.IsName("Pour") || stateInfo.IsName("Dead") || stateInfo.IsName("Slap"))
        {
            return;
        }

        Vector3 rayOrigin = transform.position + Vector3.up * rayOriginHeight;

        for (int i = 0; i < pickupRays; i++)
        {
            float angle = Mathf.Lerp(-pickupAngle / 2, pickupAngle / 2, (float)i / (pickupRays - 1));
            Vector3 pickupDirection = Quaternion.Euler(0, angle, 0) * transform.forward;

            Debug.DrawRay(transform.position + Vector3.up * rayOriginHeight, pickupDirection * pickupRange, Color.red);

            if (Physics.SphereCast(transform.position + Vector3.up * rayOriginHeight, 0.1f, pickupDirection, out RaycastHit hit, pickupRange, pickupLayer))
            {
                if (Input.GetKeyDown(KeyCode.E))
                {
                    itemToPickUp = hit.collider.gameObject;
                    if (itemToPickUp.layer == LayerMask.NameToLayer("Stary"))
                    {
                        Pour();
                    }
                    else if(itemToPickUp.layer == LayerMask.NameToLayer("Interactable"))
                    {
                        var alcoholData = itemToPickUp.GetComponent<ItemData>();
                        Pickup(alcoholData);
                    }
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
            animator.SetBool("Slap", true);
        }

        moveDirection.y -= gravity * Time.deltaTime;
        characterController.Move(moveDirection * Time.deltaTime);
    }

    private void Pickup(ItemData item)
    {
        if (!GameManager.Instance.PlayerCanPickup) return;

        animator.SetBool("Pickup", true);
        GameManager.Instance.PickupAlcohol(item);
    }

    private void Pour()
    {
        if (!GameManager.Instance.PlayerCanPour) return;
        
        animator.SetBool("Pour", true);  // play the special pickup animation
        SoundManager.Instance.PlaySound();
        GameManager.Instance.PourAlcohol();
    }

    public void EndPickup()
    {
        //TODO PODNOSZENIE DO EQ
        if (itemToPickUp != null)
        {
            Destroy(itemToPickUp);
            itemToPickUp = null;
        }
        animator.SetBool("Pickup", false);
    }

    public void EndSlap()
    {
        animator.SetBool("Slap", false);
    }

    public void EndPour()
    {
        //TODO STARY PIJANY
        animator.SetBool("Pour", false);
    }

    public void KillPlayer()
    {
        animator.SetBool("Dead", true);
    }
}
