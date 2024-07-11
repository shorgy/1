using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float jumpForce = 10f;
    public float moveSpeed = 5f;
    private bool isGrounded;
    private Rigidbody rb;
    private bool isDead = false;  // ����� ��� ���������� ����� ������
    public float deathHeight = -10f;  // ������, �� ��� ������� ����� �� ��� ������

    void Start()
    {
        rb = GetComponent<Rigidbody>();

        if (rb == null)
        {
            Debug.LogError("Rigidbody component is missing from the Player.");
        }
    }

    void Update()
    {
        if (isDead) return;  // ���� ������� �������, �� �������� ��������� ��

        // ��� ������
        transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime);

        // �������
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            Jump();
        }

        // �������� ������ ������
        if (transform.position.y < deathHeight)
        {
            Die();
        }
    }

    public void Initialize(Rigidbody rigidbody)
    {
        rb = rigidbody;
    }

    public void SetGrounded(bool grounded)
    {
        isGrounded = grounded;
    }

    public void Jump()
    {
        Debug.Log("Jump called");
        rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        isGrounded = false;
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }

    void Die()
    {
        isDead = true;
        GameManager.Instance.ShowDeathScreen();  // �������� ����� ����� ����� GameManager
    }

    public void IncreaseSpeed(int platformsPassed)
    {
        moveSpeed += platformsPassed * 0.1f;  // ��������� �������� ������
    }
}
