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
        GameObject collidedObject = collision.gameObject;
        Ingredient ingredient = collidedObject.GetComponent<Ingredient>();
        BoxCollider2D boxCollider = collidedObject.GetComponent<BoxCollider2D>();

        //Make sure collision is above
        if (collidedObject.transform.position.y - boxCollider.bounds.extents.y >= this.transform.position.y + this.GetComponent<BoxCollider2D>().bounds.extents.y)
        {
            if (Mathf.Abs(transform.position.x - collidedObject.transform.position.x) < this.GetComponent<BoxCollider2D>().bounds.extents.x + 0.1f)
            {
                collidedObject.transform.parent = this.transform;
                ingredient.stackedOffset = collidedObject.transform.localPosition;
                collidedObject.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation | RigidbodyConstraints2D.FreezePositionY;
                Destroy(this.GetComponent<BoxCollider2D>());
            }
            else
            {
                collidedObject.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.None;
            }
        }
    }
}
