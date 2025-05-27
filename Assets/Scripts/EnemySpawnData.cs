using UnityEngine;

[System.Serializable]
public class EnemySpawnData
{
    public GameObject enemyInstance; // The enemy GameObject in the scene
    public GameObject enemPrefab; // The enemy Prefab in the prefab folder

    [HideInInspector] public Vector3 spawnPointPos; // Where to respawn
    [HideInInspector] public Quaternion spawnPointRot;
    [HideInInspector] public Vector3 spawnPointScale;

    public float respawnDelay = 5f; // Time before respawn
    public int maxRespawns = 10; // Total allowed respawns

    [HideInInspector] public int currentRespawns = 0;
    [HideInInspector] public bool isRespawning = false;

    public void SetOriginalTransform()
    {
        spawnPointPos = enemyInstance.transform.position;
        spawnPointRot = enemyInstance.transform.rotation;
        spawnPointScale = enemyInstance.transform.localScale;

    }
}
