﻿using System.Collections;
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
