using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChefController : MonoBehaviour
{
    Rigidbody2D rb;
    float dirX;
    float moveSpeed = 20f;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (Application.platform == RuntimePlatform.Android)
        {
            dirX = Input.acceleration.x * moveSpeed;
        }
        else
        {
            dirX = Input.GetAxis("Horizontal") * moveSpeed;
        }

        transform.position = new Vector2(Mathf.Clamp(transform.position.x, -7.5f, 7.5f), transform.position.y);
    }

    void FixedUpdate()
    {
        rb.velocity = new Vector2(dirX, 0f);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<Ingredient>() != null)
        {
            if (GetComponent<BoxCollider2D>() != null)
            {
                if (Mathf.Abs(transform.position.x - collision.transform.position.x) < GetComponent<BoxCollider2D>().bounds.extents.x + 0.1f)
                {
                    collision.transform.parent = this.transform;
                    Destroy(collision.gameObject.GetComponent<Rigidbody2D>());
                    Destroy(GetComponent<BoxCollider2D>());
                }
            }
        }
    }
}
