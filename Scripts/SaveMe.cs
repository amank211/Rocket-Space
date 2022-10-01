using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SaveMe : MonoBehaviour
{

    SpriteRenderer spriteRenderer;
    [SerializeField]
    TextMeshProUGUI text;
    int health;
    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        health = 50;
        text.text = health.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.CompareTag("Enemy")) {
            spriteRenderer.color = Random.ColorHSV(0f, 1f, 1f, 1f, 0.5f, 1f);
            FindObjectOfType<GameManager>().SpawnExplosion(collision.transform.position);

            health--;
            if (health <= 0) {
                Destroy(gameObject);
                FindObjectOfType<GameManager>().SpawnExplosion(transform.position);
                return;
            }
            text.text = health.ToString();

            collision.gameObject.SetActive(false);
            FindObjectOfType<GameManager>().SpawnRocket(collision.gameObject);

        }
    }
}
