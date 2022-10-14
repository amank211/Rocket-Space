using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveFactory: MonoBehaviour
{

    int mCurrentIndex = 0;
    [SerializeField]
    List<GameObject> Bosses, Enemy;

    [SerializeField]
    Transform SpawnPoints;

    int numberBossWAveCame = 0;

    public Wave GetNewWave() {

        Wave wave = new Wave();
        
        mCurrentIndex++;
        Debug.Log("currentWAcve: " + mCurrentIndex);

        int WaveIndex = mCurrentIndex % 5;

        wave.Enemies = new List<GameObject> ();

        if (WaveIndex == 0)
        {
            numberBossWAveCame++;
            // Get the boss
            int numebrOfBoss = mCurrentIndex / 10 + 1;
            wave.isBossWave = true;
            for (int i = 0; i < numebrOfBoss; i++)
            {
                wave.Enemies.Add(Bosses[Random.Range(0, Bosses.Count - 1)]);
            }
            wave.bulletToBeAdded = numebrOfBoss * 3;
        }
        else {

            int enemiesCount = (WaveIndex + 1) + (numberBossWAveCame) * 2;
            wave.bulletToBeAdded = enemiesCount * 2;

            for (int i = 0; i < enemiesCount; i++)
            {
                wave.Enemies.Add(Enemy[Random.Range(0, Enemy.Count - 1)]);
            }

        }
        wave.randomSpawnPoints = SpawnPoints;
        wave.ID = mCurrentIndex;
        return wave;
    }

}
