using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class WaveManager : MonoBehaviour
{

    [SerializeField]
    TextMeshProUGUI mWAveText;

    public SceneWave mCurrentWave = new SceneWave();

    [SerializeField]
    Transform saveMe;

    [SerializeField]
    PlayerController playerController;

    [SerializeField]
    Transform bossSpawn;

    IEnumerator coroutine = null;

    // Start is called before the first frame update
    void Start()
    {
        setCurrentWave(FindObjectOfType<WaveFactory>().GetNewWave());
    }

    // Update is called once per frame
    void Update()
    {
        if (mCurrentWave.EnemiesAlive.Count <= 0)
        {
            setCurrentWave(FindObjectOfType<WaveFactory>().GetNewWave());
        }
    }

    void setCurrentWave(Wave wave) {
        mWAveText.enabled = true;
        if (coroutine != null) { 
            StopCoroutine(coroutine);
        }
        coroutine = HideWaveText();
        StartCoroutine(coroutine);
        mWAveText.text ="WAVE : " + (wave.ID);
        mCurrentWave = new SceneWave();
        mCurrentWave.ID = wave.ID;
        mCurrentWave.randomSpawnPoints = wave.randomSpawnPoints;
        mCurrentWave.Enemies = wave.Enemies;
        mCurrentWave.bulletsTobeAdded = wave.bulletToBeAdded;
        mCurrentWave.EnemiesAlive = new List<GameObject>();
        mCurrentWave.isBoss = wave.isBossWave;
        saveMe.GetComponent<SaveMe>().AddHealth(1);
        if (mCurrentWave.isBoss)
            StartBossWave();
        else
            StartNewWave();
    }

    void StartBossWave() {  
        playerController.AddBullets(mCurrentWave.bulletsTobeAdded);
        for (int i = 0; i < mCurrentWave.Enemies.Count; i++) { 
            GameObject enemy = Instantiate(mCurrentWave.Enemies[i], bossSpawn.GetChild(i));
            Vector3 pos = enemy.transform.position;
            pos.x = Random.Range(-15.0f, 15.0f);
            pos.y = Random.Range(10f, 20f);
            enemy.transform.position = pos;
            mCurrentWave.EnemiesAlive.Add(enemy);
            enemy.GetComponent<BalloonBoss>().setWave(ref mCurrentWave);
        }
    }

    IEnumerator HideWaveText() {
        yield return new WaitForSeconds(2);
        mWAveText.enabled = false;
    }

    public void StartNewWave() {
        
        playerController.AddBullets(mCurrentWave.bulletsTobeAdded);
        for (int i = 0; i < mCurrentWave.Enemies.Count; i++) { 
            GameObject enemy = Instantiate(mCurrentWave.Enemies[i], saveMe);
            Vector3 pos = enemy.transform.position;
            pos.x = Random.Range(-15.0f, 15.0f);
            pos.y = Random.Range(50f, 100f);
            enemy.transform.position = pos;
            mCurrentWave.EnemiesAlive.Add(enemy);
            enemy.GetComponent<CrabController>().setWave(ref mCurrentWave);
        }
        

    }

    public class SceneWave {
        public int ID;
        public Transform randomSpawnPoints;
        public List<GameObject> Enemies;
        public int bulletsTobeAdded;
        public List<GameObject> EnemiesAlive;
        public bool isBoss;
    }

}
