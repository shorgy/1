using UnityEngine;

public class PlatformManager : MonoBehaviour
{
    public GameObject platformPrefab;
    public GameObject bonusPrefab;
    public int initialNumberOfPlatforms = 5; // Кількість початкових платформ
    public float minPlatformSpacing = 5f; // Мінімальна відстань між платформами
    public float maxPlatformSpacing = 15f; // Максимальна відстань між платформами
    public float minPlatformHeight = -2f; // Мінімальна висота платформ
    public float maxPlatformHeight = 2f; // Максимальна висота платформ
    public float bonusSpawnChance = 0.5f; // Ймовірність спавну бонуса

    private Vector3 spawnPosition = new Vector3(0, 0, 0);
    private int platformsPassed = 0; // Лічильник пройдених платформ

    void Start()
    {
        // Перевірка, чи призначені всі необхідні компоненти
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

        // Генерація початкових платформ
        for (int i = 0; i < initialNumberOfPlatforms; i++)
        {
            SpawnPlatform();
        }
    }

    void Update()
    {
        // Генерація нових платформ у міру просування гравця
        GameObject player = GameObject.FindWithTag("Player");
        if (player != null && player.transform.position.z > spawnPosition.z - (initialNumberOfPlatforms * maxPlatformSpacing))
        {
            SpawnPlatform();
        }
    }

    void SpawnPlatform()
    {
        // Генерація випадкових параметрів платформи
        float platformHeight = Random.Range(minPlatformHeight, maxPlatformHeight);
        float platformSpacing = Random.Range(minPlatformSpacing, maxPlatformSpacing);

        // Створення нової платформи
        Vector3 platformPosition = new Vector3(spawnPosition.x, platformHeight, spawnPosition.z + platformSpacing);
        Instantiate(platformPrefab, platformPosition, Quaternion.identity);

        // Спавн бонуса з певною ймовірністю
        if (Random.value < bonusSpawnChance)
        {
            Vector3 bonusPosition = platformPosition + new Vector3(0, 1, 0); // Позиція бонуса над платформою
            Instantiate(bonusPrefab, bonusPosition, Quaternion.identity);
        }

        // Оновлення позиції для наступної генерації платформи
        spawnPosition = platformPosition;

        // Збільшення лічильника пройдених платформ і збільшення швидкості гравця
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
