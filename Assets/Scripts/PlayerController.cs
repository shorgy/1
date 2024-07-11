using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float jumpForce = 10f;
    public float moveSpeed = 5f;
    private bool isGrounded;
    private Rigidbody rb;
    private bool isDead = false;  // «м≥нна дл€ в≥дстеженн€ стану гравц€
    public float deathHeight = -10f;  // ¬исота, на €к≥й гравець вмираЇ п≥д час пад≥нн€

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
        if (isDead) return;  // якщо гравець мертвий, не виконуЇмо подальших д≥й

        // –ух уперед
        transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime);

        // —трибок
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            Jump();
        }

        // ѕерев≥рка пад≥нн€ гравц€
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
        GameManager.Instance.ShowDeathScreen();  // ѕоказати екран смерт≥ через GameManager
    }

    public void IncreaseSpeed(int platformsPassed)
    {
        moveSpeed += platformsPassed * 0.1f;  // «б≥льшенн€ швидкост≥ гравц€
    }
}
