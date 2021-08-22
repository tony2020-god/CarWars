using UnityEngine;

[CreateAssetMenu(fileName = "怪物資料", menuName = "CarWar/怪物資料")]
public class EnemyData : ScriptableObject
{
    [Header("血量"), Range(1, 1000)]
    public float hp;
    [Header("移動速度"), Range(0, 10)]
    public float speed;
    [Header("重量"), Range(0, 1000)]
    public float weight;
}
