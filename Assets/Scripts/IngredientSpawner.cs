using System;
using UnityEngine;

public class IngredientSpawner : MonoBehaviour
{
    [SerializeField] Ingredient[] ingredients;
    [SerializeField] float spawnSpeed = 2f;
    float spawnTimer = 0f;

    private void Update()
    {
        spawnTimer += Time.deltaTime;

        if (spawnTimer >= spawnSpeed)
        {
            spawnTimer = 0f;
            SpawnIngredient();
        }
    }

    private void SpawnIngredient()
    {
        Ingredient ingredient = ingredients[UnityEngine.Random.Range(0, ingredients.Length)];

        Instantiate(ingredient, new Vector3(0, 6), Quaternion.identity);
    }
}
