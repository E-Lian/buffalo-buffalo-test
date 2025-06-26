using UnityEngine;

public class Enemy : MonoBehaviour
{
    public EnemyType type;

    
    public void printStat()
    {
        Debug.Log(string.Format("class:{0} type:{1} power:{2} health:{3} speed:{4} rate:{5}", type.enemyClass, type.color, type.power, type.health, type.speed, type.spawnRate));
    }

    // get the type and update material
    public void Initialize(EnemyType enemyType)
    {
        type = enemyType;

        Renderer renderer = GetComponent<Renderer>();
        if (renderer == null)
        {
            Debug.LogError("Renderer component not found");
            return;
        } else if (type == null) 
        {
            Debug.LogError("Enemy type not found");
            return;
        } else
        {
            renderer.material = type.material;
        }
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // do nothing
    }

    // Update is called once per frame
    void Update()
    {
        // do nothing
    }
}
