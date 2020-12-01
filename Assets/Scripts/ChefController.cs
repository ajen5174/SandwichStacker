using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChefController : MonoBehaviour
{
    Rigidbody2D rb;
    float dirX;
    float dirZ;
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


        // Check for sandwich completion
        if (Application.platform == RuntimePlatform.Android)
        {
            if (Input.acceleration.z > 0.5f)
            {
                CalculateScore();
            }
        }
        else if (Input.GetKeyDown(KeyCode.Space))
        {
            CalculateScore();

        }
    }

    void FixedUpdate()
    {
        rb.velocity = new Vector2(dirX, 0f);
    }

    private int CalculateScore()
    {
        int score = 0;

        List<Ingredient> stackedIngredients = new List<Ingredient>();

        if (transform.childCount > 0)
        {
            stackedIngredients.Add(transform.GetChild(0).GetComponent<Ingredient>());
            GetIngredients(transform.GetChild(0), stackedIngredients);
        }

        for (int i = 0; i < stackedIngredients.Count; i++)
        {
            Destroy(stackedIngredients[i].gameObject);
        }

        this.GetComponent<BoxCollider2D>().enabled = true;

        return score;
    }

    private void GetIngredients(Transform parent, List<Ingredient> stackedIngredients)
    {
        if (parent.transform.childCount > 0)
        {
            Transform child = parent.transform.GetChild(0);
            if (child != null)
            {
                stackedIngredients.Add(child.GetComponent<Ingredient>());
                GetIngredients(child, stackedIngredients);
            }
        }
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
                this.GetComponent<BoxCollider2D>().enabled = false;
            }
            else
            {
                collidedObject.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.None;
            }
        }
    }
}
