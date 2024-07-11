using UnityEngine;

public class PlatformManager : MonoBehaviour
{
    public GameObject platformPrefab;
    public GameObject bonusPrefab;
    public int initialNumberOfPlatforms = 5; // ʳ������ ���������� ��������
    public float minPlatformSpacing = 5f; // ̳������� ������� �� �����������
    public float maxPlatformSpacing = 15f; // ����������� ������� �� �����������
    public float minPlatformHeight = -2f; // ̳������� ������ ��������
    public float maxPlatformHeight = 2f; // ����������� ������ ��������
    public float bonusSpawnChance = 0.5f; // ��������� ������ ������

    private Vector3 spawnPosition = new Vector3(0, 0, 0);
    private int platformsPassed = 0; // ˳������� ��������� ��������

    void Start()
    {
        // ��������, �� ��������� �� �������� ����������
        if (platformPrefab == null)
        {
            Debug.LogError("Platform prefab is not assigned.");
            return;
        }

        if (bonusPrefab == null)
        {
            Debug.LogError("Bonus prefab is not assigned.");
            return;
        }

        Debug.Log("PlatformManager Start method called");

        // ��������� ���������� ��������
        for (int i = 0; i < initialNumberOfPlatforms; i++)
        {
            SpawnPlatform();
        }
    }

    void Update()
    {
        // ��������� ����� �������� � ��� ���������� ������
        GameObject player = GameObject.FindWithTag("Player");
        if (player != null && player.transform.position.z > spawnPosition.z - (initialNumberOfPlatforms * maxPlatformSpacing))
        {
            SpawnPlatform();
        }
    }

    void SpawnPlatform()
    {
        // ��������� ���������� ��������� ���������
        float platformHeight = Random.Range(minPlatformHeight, maxPlatformHeight);
        float platformSpacing = Random.Range(minPlatformSpacing, maxPlatformSpacing);

        // ��������� ���� ���������
        Vector3 platformPosition = new Vector3(spawnPosition.x, platformHeight, spawnPosition.z + platformSpacing);
        Instantiate(platformPrefab, platformPosition, Quaternion.identity);

        // ����� ������ � ������ ���������
        if (Random.value < bonusSpawnChance)
        {
            Vector3 bonusPosition = platformPosition + new Vector3(0, 1, 0); // ������� ������ ��� ����������
            Instantiate(bonusPrefab, bonusPosition, Quaternion.identity);
        }

        // ��������� ������� ��� �������� ��������� ���������
        spawnPosition = platformPosition;

        // ��������� ��������� ��������� �������� � ��������� �������� ������
        GameObject player = GameObject.FindWithTag("Player");
        if (player != null)
        {
            PlayerController playerController = player.GetComponent<PlayerController>();
            if (playerController != null)
            {
                playerController.IncreaseSpeed(platformsPassed);
            }
            else
            {
                Debug.LogError("PlayerController component not found on Player object.");
            }
        }
        else
        {
            Debug.LogError("Player object not found.");
        }

        platformsPassed++;
    }
}
