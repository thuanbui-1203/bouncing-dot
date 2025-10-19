using System;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerControl : MonoBehaviour
{
    public float tapForce = 15f;
    public bool isTouchingBorder = false;
    private readonly float maxBorderTime = 5.0f;
    public float borderTimer = 0f;
    private Rigidbody2D rb;
    public float gravityDirection = -1f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = 3f * gravityDirection;
    }

    // Update is called once per frame
    void Update()
    {
        if (isTouchingBorder)
        {
            borderTimer += Time.deltaTime;
        }
        transform.position = new Vector3(0, Math.Clamp(transform.position.y, -4.5f, 4.5f), 0);
        if (Input.GetMouseButtonDown(0))
        {
            ChangeDirection();
            rb.linearVelocity = new Vector2(rb.position.x, 0);
            rb.AddForce(Vector2.up * gravityDirection, ForceMode2D.Impulse);
        }
    }
    void FixedUpdate()
    {
        if (borderTimer > maxBorderTime && isTouchingBorder)
        {
            ChangeDirection();
            isTouchingBorder = false;
        }

    }

    void ChangeDirection()
    {
        gravityDirection *= -1;
        rb.gravityScale = gravityDirection * 3f;
        borderTimer = 0f;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.CompareTag("Obstacle"))
        {
            // Destroy(collision.gameObject);
            Debug.Log("Game Over! Hit Obstacle");

            Time.timeScale = 0f; //Stop the game
        }

        if (collision.transform.CompareTag("Border"))
        {
            isTouchingBorder = true;
        }
    }
}
