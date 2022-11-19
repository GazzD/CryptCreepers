using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] float speed = 5;
    [SerializeField] int health = 3;
    public bool powerShot;

    private void Start()
    {
        Destroy(gameObject, 5); // Destroy bullet after 5 seconds
    }

    void Update()
    {
        transform.position += transform.right * Time.deltaTime * speed;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //print("Triggering....");
        if (collision.CompareTag("Enemy")) {
            collision.GetComponent<Enemy>().TakeDamage();
            if(!powerShot)
            {
                Destroy(gameObject); // Destroy bullet
            }
            if(--health <= 0)
            {
                Destroy(gameObject);
            }
        }
    }
}
