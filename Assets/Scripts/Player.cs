using System.Collections;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Ship Stats")]
    [SerializeField] private int health = 200;
    //Configs
    [Header("Movement Parameters")]
    [SerializeField] private float moveSpeed = 10f;
    [SerializeField] private float xPadding = .1f;
    [SerializeField] private float yPadding = .1f;
    //Limit the ship movement
    
    [Header("Audio & Volume Controls")]
    [SerializeField] [Range(0, 1)] private float explosionVolume = 0.5f;
    [SerializeField] [Range(0,1)] private float shootVolume = 0.5f;
    [SerializeField] private AudioClip playerShoot;
    [SerializeField] private AudioClip playerDestroyed;
    
    //Speed of shots
    [Header("Laser Configuration")]
    [SerializeField] private float projectileSpeed = 10f;
    [SerializeField] private GameObject laserPrefab = null;
    [SerializeField] private float projectileFiringPeriod = 0.1f;
    
    [Header("Special Effects")]
    [SerializeField] private float explosionDuration = 1f;
    [SerializeField] private GameObject explosionVFX;
    
    private Coroutine _firingCoroutine;
    
    //Clamp Amounts
    private float _minX;
    private float _maxX;
    private float _minY;
    private float _maxY;

    // Start is called before the first frame update
    void Start()
    {
        SetUpMoveBoundaries();
    } 
    // Update is called once per frame
    void Update()
    {
        
        Move();
        Fire();
    }

    private void Fire()
    {
        if (Input.GetButtonDown("Fire1"))
        {
           _firingCoroutine = StartCoroutine(FireContinuously());
        }

        if (Input.GetButtonUp("Fire1"))
        {
            StopCoroutine(_firingCoroutine);
        }
    }

    IEnumerator FireContinuously()
    {
        while (true)
        {
            GameObject laser = Instantiate(laserPrefab, transform.position, Quaternion.identity) as GameObject;
            laser.GetComponent<Rigidbody2D>().velocity = new Vector2(0, projectileSpeed);
            AudioSource.PlayClipAtPoint(playerShoot, Camera.main.transform.position, shootVolume);
            yield return new WaitForSeconds(projectileFiringPeriod);
        }
    }

    private void SetUpMoveBoundaries()
    {
        Camera gameCamera = Camera.main;
        
        //Set up X values for clamp
        _minX = gameCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).x + xPadding;
        _maxX = gameCamera.ViewportToWorldPoint(new Vector3(1, 0, 0)).x - xPadding;
        
        //Set up y values for clamp
        _minY = gameCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).y + yPadding;
        _maxY = gameCamera.ViewportToWorldPoint(new Vector3(0, 1, 0)).y - yPadding;
    }
    private void Move()
    {
        // Move Left and Right
        var deltaX = Input.GetAxis("Horizontal") * Time.deltaTime;
        var newXPos =Mathf.Clamp(transform.position.x + deltaX * moveSpeed, _minX,_maxX);
        // MOve Up and Down
        var deltaY = Input.GetAxis("Vertical") * Time.deltaTime;
        var newYPos = Mathf.Clamp(transform.position.y + deltaY * moveSpeed, _minY,_maxY);
        transform.position = new Vector2(newXPos, newYPos);
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
            FindObjectOfType<Level>().LoadGameOver();
        }
    }
    public int GetHealth()
    {
        return health;
    }

    private void Explode()
    {
        Destroy(gameObject);
        GameObject explosion = Instantiate(explosionVFX, transform.position, transform.rotation);
        AudioSource.PlayClipAtPoint(playerDestroyed, Camera.main.transform.position, explosionVolume);
        Destroy(explosion, explosionDuration);
    }
    
}

