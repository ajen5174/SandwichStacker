using System;
using UnityEngine;

public class IngredientSpawner : MonoBehaviour
{
    [SerializeField] public Ingredient[] ingredients;
    [SerializeField] float spawnSpeed = 2f;
    float spawnTimer = 0f;

    public bool isPaused = false;

    private void Update()
    {
        if(!isPaused)
        {
            spawnTimer += Time.deltaTime;

            if (spawnTimer >= spawnSpeed)
            {
                spawnTimer = 0f;
                SpawnIngredient();
            }
        }
        
    }

    private void SpawnIngredient()
    {
        Ingredient ingredient = ingredients[UnityEngine.Random.Range(0, ingredients.Length)];

        Instantiate(ingredient, new Vector3(UnityEngine.Random.Range(-5f, 5f), 6), Quaternion.identity);
    }
}
