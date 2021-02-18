using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] float movementSpeed = 1;

    Rigidbody2D myRigidbody2D;


    // Start is called before the first frame update
    void Start()
    {
        myRigidbody2D = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        HandleMovement();
    }

    private void HandleMovement()
    {
        //transform.Translate(Vector3.right * movementSpeed * Time.deltaTime);
        myRigidbody2D.velocity = new Vector2(movementSpeed * transform.localScale.x, 0f);
    }


    private void OnTriggerExit2D(Collider2D collision)
    {
        Flip();
    }

    private void Flip()
    {
        transform.localScale = new Vector3(-(Mathf.Sign(myRigidbody2D.velocity.x)), 1, 1);
    }

    //private void OnTriggerEnter2D(Collider2D collision)
    //{
    //    Flip();
    //}
}
