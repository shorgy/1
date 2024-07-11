using UnityEngine;

public class Bonus : MonoBehaviour
{
    public int bonusValue = 10;

    void OnTriggerEnter(Collider other)
    {
        Debug.Log("Triggered by: " + other.gameObject.name);
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player collected bonus");
            // ��������� ���� ������
            ScoreManager.instance.AddScore(bonusValue);

            // ��������� ������
            Destroy(gameObject);
        }
    }
}