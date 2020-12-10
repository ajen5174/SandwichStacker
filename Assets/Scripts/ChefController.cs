using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ChefController : MonoBehaviour
{
    Rigidbody2D rb;
    float dirX;
    float dirZ;
    float moveSpeed = 20f;

    [SerializeField] SandwichCreator sandwichCreator = null;
    AudioSource submitChimeSound = null;

    [SerializeField] TextMeshProUGUI scoreText = null;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        submitChimeSound = gameObject.AddComponent<AudioSource>();
        submitChimeSound.volume = 0.07f;
        submitChimeSound.clip = Resources.Load<AudioClip>("Audio/chime");
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

        transform.position = new Vector2(Mathf.Clamp(transform.position.x, -5f, 5f), transform.position.y);


        // Check for sandwich completion
        if (Application.platform == RuntimePlatform.Android)
        {
            if (Input.acceleration.z > 0.5f)
            {
                submitChimeSound.Play();
                int.TryParse(scoreText.text, out int score);
                scoreText.text = (score + CalculateScore()).ToString();
            }
        }
        else if (Input.GetKeyDown(KeyCode.Space))
        {

            submitChimeSound.Play();
            int.TryParse(scoreText.text, out int score);
            scoreText.text = (score + CalculateScore()).ToString();
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

        // Get the list of all ingredients on plate
        if (transform.childCount > 0)
        {
            stackedIngredients.Add(transform.GetChild(0).GetComponent<Ingredient>());
            GetIngredients(transform.GetChild(0), stackedIngredients);
        }


        // Calculate score
        for (int i = 0; i < stackedIngredients.Count; i++)
        {
            bool matched = false;
            for (int x = 0; x < sandwichCreator.ingredientGoal.Count; x++)
            {
                if (sandwichCreator.ingredientGoal[x] == stackedIngredients[i].ingredientType)
                {
                    matched = true;
                    sandwichCreator.ingredientGoal.RemoveAt(x);
                    break;
                }
            }

            // Score up if matched
            if (matched)
            {
                score += 100;
            }
            else
            {
                score -= 50;
            }
        }

        foreach (Ingredient ingredient in stackedIngredients)
        {
            Destroy(ingredient.gameObject);
        }

        this.GetComponent<BoxCollider2D>().enabled = true;
        // Remake the required sandwich
        sandwichCreator.CreateSandwich();
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
