using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Enemy : MonoBehaviour
{
    [SerializeField] private float health = 100;
    [SerializeField] private float shotCounter;
    [SerializeField] private float minTimeBetweenShots = 0.2f;
    [SerializeField] private float maxTimeBetweenShots = 3f;
    [SerializeField] private GameObject laserPrefab;
    [SerializeField] private float projectileSpeed = 0.1f;
    [SerializeField] private AudioClip enemyShoot;
    [SerializeField] private AudioClip enemyDestroyed;
    [SerializeField] private GameObject explosionVFX;

    [SerializeField] private float explosionDuration = 1f;
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
    //TODO: Trigger Enemy explode sound
    //TODO: Trigger Player Shoot
    //TODO: Trigger Player Explode
    
    private void Explode()
    {
        Destroy(gameObject);
        GameObject explosion = Instantiate(explosionVFX, transform.position, transform.rotation);
        AudioSource.PlayClipAtPoint(enemyDestroyed, Camera.main.transform.position);
        Destroy(explosion, explosionDuration);
    }

    private void EnemyLaserSound()
    {
        
        AudioSource.PlayClipAtPoint(enemyShoot, Camera.main.transform.position, 0.5f);
    }
}

