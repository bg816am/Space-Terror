using System.Collections;
using UnityEngine;

public class Player : MonoBehaviour
{
    //Configs
    [Header("Movement Parameters")]
    [SerializeField] private float moveSpeed = 10f;
    //Limit the ship movement
    [SerializeField] private float xPadding = .1f;
    [SerializeField] private float yPadding = .1f;

    [SerializeField] private int health = 200;
    //Speed of shots
    [SerializeField] private float projectileSpeed = 10f;
    [SerializeField] private GameObject laserPrefab = null;
    [SerializeField] private float projectileFiringPeriod = 0.1f;

    private Coroutine firingCoroutine;
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
           firingCoroutine = StartCoroutine(FireContinuously());
        }

        if (Input.GetButtonUp("Fire1"))
        {
            StopCoroutine(firingCoroutine);
        }
    }

    IEnumerator FireContinuously()
    {
        while (true)
        {
            GameObject laser = Instantiate(laserPrefab, transform.position, Quaternion.identity) as GameObject;
            laser.GetComponent<Rigidbody2D>().velocity = new Vector2(0, projectileSpeed);
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
            Destroy(gameObject);
            
        }
    }
}
