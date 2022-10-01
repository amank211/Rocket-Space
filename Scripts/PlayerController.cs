using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;
using static UnityEngine.GraphicsBuffer;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    GameObject bullet;
    [SerializeField]
    Transform bulletPosition;
    [SerializeField]
    int rotationSpeed;
    [SerializeField]
    float torque;
    float MAX_TORQUE = 5f;
    [SerializeField]
    float mBulletSpeed;

    List<GameObject> bullets = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        torque = 1f;
        bullets.Add(Instantiate(bullet));
        bullets.Add(Instantiate(bullet));
        bullets.Add(Instantiate(bullet));
        bullets.Add(Instantiate(bullet));
        bullets.Add(Instantiate(bullet));
        bullets.Add(Instantiate(bullet));
        bullets.Add(Instantiate(bullet));
    }

    // Update is called once per frame
    void Update()
    {
        if (torque > 1)
        {
            torque -= 3* Time.deltaTime;
        }
        else { 
            torque = 1f;
        }
        float deltaRotation = rotationSpeed * 10 * Time.deltaTime * torque;
        Vector3 newRotation = transform.eulerAngles;
        newRotation.z += deltaRotation;
        transform.eulerAngles = newRotation;
    }

    public void Shoot() {
        Debug.Log("torque: " + torque + " mx : " + MAX_TORQUE);
        if (torque < MAX_TORQUE) {
            Debug.Log(Mathf.Min(2f, torque - MAX_TORQUE));
            torque += Mathf.Min(2f,  MAX_TORQUE - torque);
        }

        GameObject bull = null;

        for(int i = bullets.Count -1; i >=0; i--) {
            GameObject obj = bullets[i];
            if (!obj.active && obj.transform.position.x == 0f && transform.position.y == 0f) {
                bull = obj;
            }
        }
        if (bull == null) { 
            bull = Instantiate(bullet);
            bullets.Add(bull);
        }

        FindObjectOfType<GameManager>().EnableTrail(bull.GetComponent<TrailRenderer>());
        bull.transform.position = bulletPosition.position;
        bull.SetActive(true);
        Rigidbody2D rigid = bull.GetComponent<Rigidbody2D>();
        float angle = Mathf.Atan(bulletPosition.position.x / bulletPosition.position.y) * Mathf.Rad2Deg;
        Vector3 roatation = bull.transform.eulerAngles;
        roatation.z = angle;
        if (bulletPosition.position.y < 0)
            roatation.z += 180f;
        bull.transform.eulerAngles = roatation;

        rigid.velocity = new Vector3(mBulletSpeed * bulletPosition.position.x, mBulletSpeed * bulletPosition.position.y, 0);
        FindObjectOfType<AudioManager>().Play("shoot");
    }
}
