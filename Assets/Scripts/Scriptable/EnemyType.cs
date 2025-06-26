using UnityEngine;

public enum EnemyClass
{
    Grunt,
    Archer,
    Assassin
}

public enum EnemyColor
{
    Red,
    Brown,
    Blue,
    Green,
    Yellow
}

[CreateAssetMenu]
public class EnemyType: ScriptableObject
{
    public EnemyClass enemyClass;
    public EnemyColor color;
    public int power;
    public int health;
    public int speed;

    [Range (0f, 1f)]
    public float spawnRate;

    public Material material;
}
