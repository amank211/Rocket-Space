using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SaveMe : MonoBehaviour
{

    SpriteRenderer spriteRenderer;
    [SerializeField]
    TextMeshProUGUI text;
    float fullHealth;
    float remainingHealth;

    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        fullHealth = 3;
        remainingHealth = fullHealth;
        text.text = remainingHealth.ToString();
        GetComponent<Renderer>().material.SetFloat("_Health", remainingHealth / fullHealth);
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void AddHealth(int health) { 
        remainingHealth += health;
        GetComponent<Renderer>().material.SetFloat("_Health", remainingHealth / fullHealth);
        text.text = remainingHealth.ToString();
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.CompareTag("Enemy")) {
            spriteRenderer.color = Random.ColorHSV(0f, 1f, 1f, 1f, 0.5f, 1f);
            FindObjectOfType<GameManager>().SpawnExplosion(collision.transform.position, transform);
            GetComponent<Renderer>().material.SetFloat("_Health", remainingHealth / fullHealth);
            remainingHealth--;
            if (remainingHealth <= 0) {
                Destroy(gameObject);
                FindObjectOfType<GameManager>().SpawnExplosion(transform.position, transform);
                FindObjectOfType<GameManager>().GameOver();
                return;
            }
            text.text = remainingHealth.ToString();
            FindObjectOfType<WaveManager>().mCurrentWave.EnemiesAlive.Remove(collision.gameObject);
            Destroy(collision.gameObject);
            FindObjectOfType<Background>().Shake();

        }
    }
}
