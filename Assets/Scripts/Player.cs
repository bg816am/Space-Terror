using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Player : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 10f;
    
    //Limit the ship movement
    //[SerializeField] private float screenWidthInUnits = 20f;
    //[SerializeField] private float minX = -10f;
    //[SerializeField] private float maxX = 10f;
    
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
        // MOve Up and Down
        var deltaY = Input.GetAxis("Vertical") * Time.deltaTime;
        var newYPos = transform.position.y + deltaY * moveSpeed;
        
        transform.position = new Vector2(newXPos, newYPos);
        
        
        //Vector2 shipPos = new Vector2(transform.position.x, transform.position.y);
        //Debug.Log(shipPos);
        
        
    }
}
