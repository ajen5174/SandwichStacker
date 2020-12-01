using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ingredient : MonoBehaviour
{
    public enum Type
    {
        BREAD,
        LETTUCE,
        TOMATO,
        ONION,
        PICKLE,
        BACON,
        BEEF,
        LUNCHMEAT,
        CHICKEN,
        CHEESE
    };

    public Type ingredientType;

    [HideInInspector] public Vector3 stackedOffset = Vector3.zero;

    private void Update()
    {
        if (stackedOffset != Vector3.zero)
        {
            transform.localPosition = stackedOffset;
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
