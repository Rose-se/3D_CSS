using UnityEngine;

public class Player : MonoBehaviour
{
    private Rigidbody rb;
    private float movementSpeed = 7f;
    private float jumpForce = 10f;
    private float movementInput = 0f;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        movementInput = Input.GetAxis("Horizontal");

        if (IsGrounded() && Input.GetButtonDown("Jump"))
        {
            Jump();
        }
    }

    private void FixedUpdate()
    {
        PlayerMove();
    }

    private void PlayerMove()
    {
        rb.velocity = new Vector3(movementInput * movementSpeed, rb.velocity.y);
    }

    private bool IsGrounded()
    {
        return true;
    }

    private void Jump()
    {
        rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
    }
}
