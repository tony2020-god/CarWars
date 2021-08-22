using UnityEngine;

[CreateAssetMenu(fileName = "盟友資料", menuName = "CarWars/盟友角色資料")]
public class SpawnAlliance : ScriptableObject
{
    public SpawnAllianceObject[] Spawn;
}

[System.Serializable] //序列化:讓底下class顯示在屬性面板(class專用)
public class SpawnAllianceObject
{
    public GameObject Alliance;
}

