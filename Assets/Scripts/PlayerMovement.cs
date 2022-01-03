using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    // Variables
    Rigidbody rb;
    [SerializeField] float movementSpeed = 6f;
    [SerializeField] float jumpForce = 5f;

    [SerializeField] Transform groundCheck;
    [SerializeField] LayerMask ground;

    [SerializeField] AudioSource jumpSound;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called onces per frame
    void Update()
    {
        //Variables
        float horizotalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        // Player Movement
        rb.velocity = new Vector3(horizotalInput * movementSpeed, rb.velocity.y ,verticalInput * movementSpeed);

        // Player Jump
        if (Input.GetButtonDown("Jump") && IsGrounded())
        {
            Jump();
            jumpSound.Play();
        }
    }

    void Jump()
    {
        rb.velocity = new Vector3(rb.velocity.x, jumpForce, rb.velocity.z);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy Head"))
        {
            Destroy(collision.transform.parent.gameObject);
            Jump();
            jumpSound.Play();
        }
    }

        bool IsGrounded()
        {
            return Physics.CheckSphere(groundCheck.position, .1f, ground);
        }
}