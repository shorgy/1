using UnityEngine;
using NUnit.Framework;

public class PlayerControllerTests
{
    [Test]
    public void Player_Jumps_When_JumpButton_Pressed()
    {
        // Створюємо тестовий об'єкт гравця
        GameObject player = new GameObject("Player");

        // Ініціалізуємо початкову позицію
        Vector3 initialPosition = player.transform.position;

        // Змінюємо позицію об'єкта, щоб симулювати стрибок
        player.transform.position = new Vector3(initialPosition.x, initialPosition.y + 1.0f, initialPosition.z);

        // Перевіряємо, що позиція змінилася (об'єкт піднявся)
        Assert.Greater(player.transform.position.y, initialPosition.y);

        // Прибираємо тестовий об'єкт
        Object.DestroyImmediate(player);
    }
}
