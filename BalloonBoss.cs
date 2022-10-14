using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BalloonBoss : MonoBehaviour
{


    [SerializeField]
    float boundaryX, boundaryY;

    float mSpeedVertical, mSpeedHorizontal;
    float offsetY, offsetX = 0;

    TextMeshProUGUI HealthText;

    float EnemySpawnTimer;

    private WaveManager.SceneWave SceneWave;

    [SerializeField]
    GameObject mEnemy;

    [SerializeField]
    private int mHealth;

    float enemySapwnTime = 0f;

    // Start is called before the first frame update
    void Start()
    {
        offsetY = 20f;
        EnemySpawnTimer = 0f;
        
        boundaryX = Random.Range(10, 15);
        boundaryY = Random.Range(20, 23);

        mSpeedVertical = Random.Range(3, 5);
        mSpeedHorizontal = Random.Range(6, 9);
        enemySapwnTime = 3.0f;

        HealthText = gameObject.GetComponentInChildren<TextMeshProUGUI>();
        HealthText.text = mHealth.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        if (!GameManager.isPlaying())
            return;

        if (Mathf.Abs(transform.position.x) > boundaryX) {
            mSpeedHorizontal = (transform.position.x > 0) ? -(Mathf.Abs(mSpeedHorizontal)) : (Mathf.Abs(mSpeedHorizontal)); 
        }

        if (transform.position.y > boundaryY + offsetY)
        {
            mSpeedVertical = -(Mathf.Abs(mSpeedVertical));
        }
        else if (transform.position.y < offsetY - boundaryY) {
            mSpeedVertical = Mathf.Abs(mSpeedVertical);
        }

        Vector3 newPos = transform.position;
        newPos.x += Time.deltaTime * mSpeedHorizontal;
        newPos.y += Time.deltaTime * mSpeedVertical;

        transform.position = newPos;    

        EnemySpawnTimer += Time.deltaTime;

        if (EnemySpawnTimer >= enemySapwnTime) {
            EnemySpawnTimer = 0f;
            enemySapwnTime = Random.Range(2.5f, 5.0f);
            GameObject enemy = Instantiate(mEnemy, FindObjectOfType<SaveMe>().gameObject.transform);
            enemy.transform.position = transform.position;
            FindObjectOfType<WaveManager>().mCurrentWave.EnemiesAlive.Add(enemy);
            FindObjectOfType<PlayerController>().AddBullets(2);
        }
    }

    public void setWave(ref WaveManager.SceneWave wave) {
        SceneWave = wave;
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.CompareTag("Bullet"))
        {
            FindObjectOfType<ScoreManager>().IncrementScore(4);
            FindObjectOfType<AudioManager>().Play("EnemyDie");
            collision.gameObject.GetComponent<TrailRenderer>().emitting = false;
            collision.gameObject.SetActive(false);
          
            mHealth--;
            if (mHealth <= 0)
            {
                FindObjectOfType<WaveManager>().mCurrentWave.EnemiesAlive.Remove(gameObject);
                Destroy(gameObject);
                FindObjectOfType<GameManager>().SpawnExplosion(collision.gameObject.transform.position, null);
            }
            else {
                FindObjectOfType<GameManager>().SpawnExplosion(collision.gameObject.transform.position, transform);
            }
            collision.gameObject.transform.position = new Vector3(0, 0, 0);
            HealthText.text = mHealth.ToString();
        }
    }
}
