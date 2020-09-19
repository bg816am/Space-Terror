using UnityEngine;
using Random = UnityEngine.Random;

public class Enemy : MonoBehaviour
{
    [Header("Enemy Stats")]
    [SerializeField] private float health = 100;
    [SerializeField] private int scoreValue = 20;
    [Header("Effects")]
    [SerializeField] private AudioClip enemyDestroyed;
    [SerializeField] private GameObject explosionVFX;
    [SerializeField][Range(0,1)] private float enemyLaserVolume = 0.5f;
    [SerializeField] private float explosionDuration = 1f;
    [SerializeField] [Range(0, 1)] private float explosionVolume;
    [Header("Weapon Stats")]
    [SerializeField] private float minTimeBetweenShots = 0.2f;
    [SerializeField] private float maxTimeBetweenShots = 3f;
    [SerializeField] private GameObject laserPrefab;
    [SerializeField] private float projectileSpeed = 0.1f;
    [SerializeField] private AudioClip enemyShoot;
    
    
    private float shotCounter;
    
    // Start is called before the first frame update
    void Start()
    {
        shotCounter = Random.Range(minTimeBetweenShots, maxTimeBetweenShots);
    }

    // Update is called once per frame
    void Update()
    {
        CountDownAndShoot();

    }

    private void CountDownAndShoot()
    {
        shotCounter -= Time.deltaTime;
        if (shotCounter <= 0)
        {
            Fire();
            shotCounter = Random.Range(minTimeBetweenShots, maxTimeBetweenShots);
        }
    }

    private void Fire()
    {
       GameObject laser = Instantiate(laserPrefab, transform.position, Quaternion.identity) as GameObject;
       laser.GetComponent<Rigidbody2D>().velocity = new Vector2(0, -projectileSpeed);
       EnemyLaserSound();
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        DamageDealer damageDealer = other.gameObject.GetComponent<DamageDealer>();
        if (!damageDealer) { return; }
            ProcessHit(damageDealer);
    }

    private void ProcessHit(DamageDealer damageDealer)
    {
        health -= damageDealer.GetDamage();
        damageDealer.Hit();
        if (health <= 0)
        {
            Explode();
        }
    }

    private void Explode()
    {
        FindObjectOfType<GameSession>().AddToScore(scoreValue);
        Destroy(gameObject);
        GameObject explosion = Instantiate(explosionVFX, transform.position, transform.rotation);
        AudioSource.PlayClipAtPoint(enemyDestroyed, Camera.main.transform.position, explosionVolume);
        Destroy(explosion, explosionDuration);
    }

    private void EnemyLaserSound()
    {
        
        AudioSource.PlayClipAtPoint(enemyShoot, Camera.main.transform.position, enemyLaserVolume);
    }
}

