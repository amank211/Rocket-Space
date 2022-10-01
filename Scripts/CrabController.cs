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

    // Start is called before the first frame update
    void Start()
    {
        
        mTarget = FindObjectOfType<SaveMe>().gameObject.transform;
       

    }

    // Update is called once per frame
    void Update()
    {
        /*angle -= Time.deltaTime/ 2;
        radius -=   Time.deltaTime / 10f;

        if (angle == 360)
            angle = 0;
        Vector3 newPosition = new Vector3(0,0,0);
        newPosition.x = radius * Mathf.Cos(angle);
        newPosition.y = radius * Mathf.Sin(angle);*/

        transform.position = Vector3.MoveTowards(transform.position, mTarget.position, mSpeed * Time.deltaTime);
        Vector3 offset = mTarget.position - transform.position;

        transform.rotation = Quaternion.LookRotation(
                               Vector3.forward, // Keep z+ pointing straight into the screen.
                               offset           // Point y+ toward the target.
                             );

        if (!GetComponent<Renderer>().isVisible)
        {    
            FindObjectOfType<GameManager>().SpawnRocket(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Bullet"))
        {
            FindObjectOfType<ScoreManager>().IncrementScore();
            FindObjectOfType<AudioManager>().Play("EnemyDie");
            collision.gameObject.GetComponent<TrailRenderer>().emitting = false;
            collision.gameObject.SetActive(false);
            collision.gameObject.transform.position = new Vector3(0, 0, 0);
            FindObjectOfType<GameManager>().SpawnExplosion(transform.position);
            FindObjectOfType<GameManager>().SpawnRocket(gameObject);
        }
    }
}
