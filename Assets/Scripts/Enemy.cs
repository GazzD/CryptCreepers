 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    Transform player;
    [SerializeField] int health = 1;
    [SerializeField] float speed = 1;
    [SerializeField] int enemyPoints = 100;
    [SerializeField] AudioClip impactAudioClip;
    [SerializeField] AudioClip enemyDeathAudioClip;

    private void Start()
    {
        player = FindObjectOfType<Player>().transform;
        GameObject[] spawnPoints = GameObject.FindGameObjectsWithTag("SpawnPoint");

        int randomSpawnPoint = Random.Range(0, spawnPoints.Length);
        transform.position = spawnPoints[randomSpawnPoint].transform.position; // Make enemy spawn in the spawn point
    }

    private void Update()
    {
        Vector2 direction = player.position - transform.position;
        transform.position += (Vector3) direction.normalized * Time.deltaTime * speed;
    }

    public void TakeDamage()
    {
        AudioSource.PlayClipAtPoint(impactAudioClip, transform.position);

        if (--health <= 0) { 
            Destroy(gameObject, 0.1f);

            AudioSource.PlayClipAtPoint(enemyDeathAudioClip, transform.position);

            GameManager.Instance.Score += enemyPoints;
            
        };

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //print("Enemy is colliding with someohne");
        if (collision.CompareTag("Player"))
        {
            collision.GetComponent<Player>().TakeDamage();
        }
    }
}
