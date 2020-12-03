﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SandwichCreator : MonoBehaviour
{
    public List<Ingredient.Type> ingredientGoal = new List<Ingredient.Type>();
    [SerializeField] RectTransform panelTransform = null;
    [SerializeField] GameObject ingredientUI = null;
    IngredientSpawner ingredientSpawner = null;
    void Start()
    {
        ingredientSpawner = GetComponent<IngredientSpawner>();
        CreateSandwich();

    }

    public void CreateSandwich()
    {
        // Reset things - Gaige
        ingredientGoal.RemoveRange(0, ingredientGoal.Count);
        foreach (Transform panelChild in panelTransform.transform)
        {
            Destroy(panelChild.gameObject);
        }

        ingredientGoal.Add(Ingredient.Type.BREAD);
        //random stuff
        int numIngredients = Random.Range(3, 6);
        for (int i = 0; i < numIngredients; i++)
        {
            Ingredient.Type ingredient = (Ingredient.Type)Random.Range(1, ((int)Ingredient.Type.TOP_BREAD) + 1);

            ingredientGoal.Add(ingredient);
        }
        ingredientGoal.Add(Ingredient.Type.TOP_BREAD);

        for(int i = 0; i < ingredientGoal.Count; i++)
        {
            GameObject go = Instantiate(ingredientUI, panelTransform);
            RectTransform rt = ingredientUI.GetComponent<RectTransform>();
            int ingredientToGetIndex = (int)ingredientGoal[i];
            Debug.Log(ingredientToGetIndex);
            Debug.Log("Length: " + ingredientSpawner.ingredients.Length);
            SpriteRenderer ingredientToAddSpriteRenderer = ingredientSpawner.ingredients[0].GetComponent<SpriteRenderer>();
            if (ingredientToGetIndex >= ingredientSpawner.ingredients.Length)
            {
                Image tempImage = go.GetComponent<Image>();
                tempImage.sprite = ingredientToAddSpriteRenderer.sprite;
                tempImage.color = ingredientToAddSpriteRenderer.color;
            }
            else
            {
                ingredientToAddSpriteRenderer = ingredientSpawner.ingredients[ingredientToGetIndex].GetComponent<SpriteRenderer>();
                Image tempImage = go.GetComponent<Image>();
                tempImage.sprite = ingredientToAddSpriteRenderer.sprite;
                tempImage.color = ingredientToAddSpriteRenderer.color;
            }
            //this height might need to be calculated in a different way if the height of each prefab ends up different.

            go.transform.position += new Vector3(0, i * 90, 0);

        }
    }

    void Update()
    {
        
    }
}
