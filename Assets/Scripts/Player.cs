using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Player : MonoBehaviour
{

    Vector2 facingDirection;
    bool gunLoaded = true;
    bool powerShotEnable = false;
    int invulnerabilityTime = 3;
    CameraController cameraController;

    [SerializeField] int health = 10;
    [SerializeField] float speed = 7;
    [SerializeField] Transform aim;
    [SerializeField] Camera mainCamera;
    [SerializeField] Transform bulletPrefab;
    [SerializeField] float fireRate = 1;
    [SerializeField] bool isInvulnerable = false;
    [SerializeField] Animator anim;
    [SerializeField] SpriteRenderer spriteRendered;
    [SerializeField] float blinkRate = 0.01f;
    [SerializeField] AudioClip itemAudioClip;


    public int Health {
        get => health;
        set {
            health = value;
            UIManager.Instance.UpdateUIHealth(health);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        Health = health;
        cameraController = FindObjectOfType<CameraController>();
    }

    // Update is called once per frame
    void Update()
    {

        Vector3 moveDirection = ReadPlayerInput();

        // Player movement
        transform.position += moveDirection * Time.deltaTime * speed;

        // Aim movement
        //aim.position = mainCamera.ScreenToWorldPoint(Input.mousePosition); // set aim position where the mouse is pointing
        facingDirection = mainCamera.ScreenToWorldPoint(Input.mousePosition) - transform.position; // Resto la posición del jugador
        aim.position = transform.position + (Vector3) facingDirection.normalized;

        //gunLoaded = shootsMade < clipSize;

        if (Input.GetMouseButton(0) && gunLoaded && Time.timeScale == 1)
        {
            Shoot();
        }
        anim.SetFloat("Speed", moveDirection.magnitude); // Magnitude = speed at that moment

        spriteRendered.flipX = aim.position.x > transform.position.x; // Flip Player where the AIM is pointing


    }

    Vector3 ReadPlayerInput() {
        float horizontalMove = (Input.GetAxis("Horizontal"));
        float vertialMove = (Input.GetAxis("Vertical"));
        return new Vector3(horizontalMove, vertialMove);
    }

    void Shoot()
    {
        gunLoaded = false;
        float angle = Mathf.Atan2(facingDirection.y, facingDirection.x) * Mathf.Rad2Deg;

        Quaternion targetRotation = Quaternion.AngleAxis(angle, Vector3.forward);
        Transform bulletClone = Instantiate(bulletPrefab, transform.position, targetRotation);

        if (powerShotEnable)
        {
            bulletClone.GetComponent<Bullet>().powerShot = true;
        }

        StartCoroutine(ReloadGun());
    }

    public void TakeDamage()
    {
        //print("Player is taking damage..." + health);

        if (isInvulnerable) return;

        cameraController.Shake();

        fireRate = 1;
        powerShotEnable = false;

        Health--;
        
        if (Health <= 0)
        {
            print("Game Over :c");
            GameManager.Instance.gameOver = true;
            UIManager.Instance.ShowGameOverScreen();
        };

        isInvulnerable = true;
        StartCoroutine(Invulnerability());
        


    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("PowerUp")) {
            AudioSource.PlayClipAtPoint(itemAudioClip, transform.position);
            switch (collision.GetComponent<PowerUp>().powerUpType) {
                case PowerUp.PowerUpType.FireRateIncrease:
                    fireRate++;
                    break;
                case PowerUp.PowerUpType.PowerShot:
                    powerShotEnable = true;
                    break;
            }
            Destroy(collision.gameObject, 0.1f);
        }
    }

    IEnumerator ReloadGun() {
        yield return new WaitForSeconds(1 / fireRate);
        gunLoaded = true;
    }

    IEnumerator Invulnerability()
    {
        StartCoroutine(BlinkRoutine());
        yield return new WaitForSeconds(invulnerabilityTime);
        isInvulnerable = false;
    }

    IEnumerator BlinkRoutine()
    {
        int numberOfBlink = 10;
        while (numberOfBlink > 0)
        {
            spriteRendered.enabled = false;
            yield return new WaitForSeconds(numberOfBlink * blinkRate);
            spriteRendered.enabled = true;
            yield return new WaitForSeconds(numberOfBlink * blinkRate);
            numberOfBlink--;

        }
    }
}
