using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class RecipeComponent
{
    public List<IngredientForCraftComponent> items;
    public int cost;
    public IngredientForCraftComponent result;

    public RecipeComponent(List<IngredientForCraftComponent> items, int cost, IngredientForCraftComponent result)
    {
        this.items = items;
        this.cost = cost;
        this.result = result;
    }

}

[Serializable]
public class Recipes
{
    public List<RecipeComponent> list = new List<RecipeComponent>();
}
