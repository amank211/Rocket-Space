using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class CrabController : MonoBehaviour
{
    Transform mTarget;
    [SerializeField]
    float mSpeed;

    public WaveManager.SceneWave mWave;

    public void setWave(ref WaveManager.SceneWave wave) { 
        this.mWave = wave;
    }

    // Start is called before the first frame update
    void Start()
    {
        mTarget = FindObjectOfType<SaveMe>().gameObject.transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (!GameManager.isPlaying())
            return;

            Vector3 pos = transform.position;
        if (transform.position.y > -5)
        {
            pos.y -= mSpeed * Time.deltaTime;
            transform.position = pos;
        }
        else {
            transform.position = Vector3.MoveTowards(transform.position, mTarget.transform.position, Time.deltaTime * mSpeed / 2);
            Vector3 angles = transform.eulerAngles;
            angles.z = - Mathf.Atan(transform.localPosition.x / transform.localPosition.y) * Mathf.Rad2Deg + 180;
            transform.eulerAngles = angles;
        }
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Bullet"))
        {
            FindObjectOfType<ScoreManager>().IncrementScore(1);
            FindObjectOfType<AudioManager>().Play("EnemyDie");
            collision.gameObject.GetComponent<TrailRenderer>().emitting = false;
            collision.gameObject.SetActive(false);
            collision.gameObject.transform.position = new Vector3(0, 0, 0);
            FindObjectOfType<GameManager>().SpawnExplosion(transform.position, null);
            FindObjectOfType<WaveManager>().mCurrentWave.EnemiesAlive.Remove(gameObject);
            Destroy(gameObject);
        }
    }
}
