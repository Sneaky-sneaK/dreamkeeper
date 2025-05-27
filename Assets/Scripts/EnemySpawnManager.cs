using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnManager : MonoBehaviour
{
    public List<EnemySpawnData> enemiesToTrack;

    private void Start()
    {
        foreach (var enemyData in enemiesToTrack)
        {
            enemyData.SetOriginalTransform();
        }
    }

    public void OnEnemyDeath(GameObject enemy)
    {
        foreach (var data in enemiesToTrack)
        {
            if (data.enemyInstance == enemy && !data.isRespawning && data.currentRespawns < data.maxRespawns)
            {
                StartCoroutine(RespawnAfterDelay(data));
                break;
            }
        }
    }

    private System.Collections.IEnumerator RespawnAfterDelay(EnemySpawnData data)
    {
        data.isRespawning = true;
        yield return new WaitForSeconds(data.respawnDelay);

        if (data.currentRespawns >= data.maxRespawns)
            yield break;

        GameObject newEnemy = Instantiate(data.enemPrefab, data.spawnPointPos, data.spawnPointRot);
        newEnemy.GetComponent<ShadowEnemyAI>().spawner = this;
        newEnemy.transform.localScale = data.spawnPointScale;

        data.enemyInstance = newEnemy;
        data.currentRespawns++;
        data.isRespawning = false;
    }
}
