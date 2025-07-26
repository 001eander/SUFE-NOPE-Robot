using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketControl : MonoBehaviour
{
    private const float RushTime = 10f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            float maxSpeed = collision.gameObject.GetComponent<RobotControl>().maxSpeed;
            Rigidbody2D rg = collision.gameObject.GetComponent<Rigidbody2D>();
            rg.velocity = new Vector2(maxSpeed*2, rg.velocity.y);
            RobotControl.RushStopTime = Time.time + RushTime;
            gameObject.SetActive(false);
        }
    }
}
