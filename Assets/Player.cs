using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Player : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 10f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    private void Move()
    {
        // Move Left and Right
        var deltaX = Input.GetAxis("Horizontal") * Time.deltaTime;
        var newXPos = transform.position.x + deltaX * moveSpeed;

        var deltaY = Input.GetAxis("Vertical") * Time.deltaTime;
        var newYPos = transform.position.y + deltaY * moveSpeed;
        transform.position = new Vector2(newXPos, newYPos);
        
        
    }
}
