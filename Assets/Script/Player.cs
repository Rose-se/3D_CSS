using UnityEngine;

public class Player : MonoBehaviour
{
    private Rigidbody rb;
    private float movementSpeed = 20f;
    private float jumpForce = 5f;
    private float horizontal;
    private float vertical;
    private Transform orientation;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        orientation = transform;
        rb.freezeRotation = true; // ป้องกันการหมุนโดยอัตโนมัติจากแรงโน้มถ่วง
    }

    private void Update()
    {
        horizontal = Input.GetAxisRaw("Horizontal");
        vertical = Input.GetAxisRaw("Vertical");

        // กระโดดเมื่อกดปุ่ม Jump
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
        Vector3 moveDirection = orientation.forward * vertical + orientation.right * horizontal;

        RotatePlayer();
        rb.AddForce(moveDirection.normalized * movementSpeed);
    }

    private bool IsGrounded()
    {
        // ตรวจสอบว่าตัวละครอยู่บนพื้นหรือไม่
        return true; // ตรวจสอบจาก Collider หรือใด ๆ ตามที่คุณใช้ในเกมของคุณ
    }
    private void RotatePlayer()
    {
        Vector3 viewDir = new Vector3(horizontal, 0f, vertical).normalized;

        // หาทิศทางของกล้องเทียบกับทิศทางการเคลื่อนที่
        if (viewDir != Vector3.zero)
        {
            float angle = Mathf.Atan2(viewDir.x, viewDir.z) * Mathf.Rad2Deg + orientation.eulerAngles.y;
            Quaternion rotation = Quaternion.Euler(0f, angle, 0f);
            rb.MoveRotation(Quaternion.Slerp(rb.rotation, rotation, Time.fixedDeltaTime * 0.5f));
        }
    }
    private void Jump()
    {
        // ตรวจสอบว่าตัวละครอยู่บนพื้นก่อนทำการกระโดด
        if (IsGrounded())
        {
            // กำหนดความเร็วในแกน y เท่ากับความสูงของการกระโดด
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
    }
}
