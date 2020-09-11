using UnityEngine;

public class Player : MonoBehaviour
{
    //Configs
    [SerializeField] private float moveSpeed = 10f;
    //Limit the ship movement
    [SerializeField] private float xPadding = .1f;
    [SerializeField] private float yPadding = .1f;
    //Speed of shots
    [SerializeField] private float projectileSpeed = 10f;
    [SerializeField] private GameObject laserPrefab;
    
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
            GameObject laser = Instantiate(laserPrefab, transform.position, Quaternion.identity) as GameObject;
            laser.GetComponent<Rigidbody2D>().velocity = new Vector2(0, projectileSpeed);
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
}
