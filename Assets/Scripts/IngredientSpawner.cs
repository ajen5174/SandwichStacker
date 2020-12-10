using System;
using UnityEngine;

public class IngredientSpawner : MonoBehaviour
{
    [SerializeField] public Ingredient[] ingredients;
    [SerializeField] float spawnSpeed = 2f;
    [SerializeField] SandwichCreator sandwichCreator = null;
    float spawnTimer = 0f;
    float gravityScale = 0.5f;

    private void Start()
    {
        switch (Difficulty.difficulty)
        {
            case Difficulty.Level.EASY:
                gravityScale = 0.4f;
                break;
            case Difficulty.Level.MEDIUM:
                gravityScale = 0.7f;
                break;
            case Difficulty.Level.HARD:
                gravityScale = 1.0f;
                break;
            default:
                break;
        }
    }

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
        Ingredient ingredient = null;
        // Get the needed ingredients 75% of the time
        if (UnityEngine.Random.Range(0, 100) > 75)
        {
            int index = (int) sandwichCreator.ingredientGoal[UnityEngine.Random.Range(0, sandwichCreator.ingredientGoal.Count)];
            ingredient = ingredients[index];
        }
        else
        {
            ingredient = ingredients[UnityEngine.Random.Range(0, ingredients.Length)];
        }

        Ingredient spawned = Instantiate(ingredient, new Vector3(UnityEngine.Random.Range(-5f, 5f), 6), Quaternion.identity);
        Rigidbody2D rb = spawned.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.gravityScale = gravityScale;
        }
    }
}
