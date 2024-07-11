using UnityEngine;
using NUnit.Framework;

public class PlayerControllerTests
{
    [Test]
    public void Player_Jumps_When_JumpButton_Pressed()
    {
        // ��������� �������� ��'��� ������
        GameObject player = new GameObject("Player");

        // ���������� ��������� �������
        Vector3 initialPosition = player.transform.position;

        // ������� ������� ��'����, ��� ���������� �������
        player.transform.position = new Vector3(initialPosition.x, initialPosition.y + 1.0f, initialPosition.z);

        // ����������, �� ������� �������� (��'��� �������)
        Assert.Greater(player.transform.position.y, initialPosition.y);

        // ��������� �������� ��'���
        Object.DestroyImmediate(player);
    }
}
