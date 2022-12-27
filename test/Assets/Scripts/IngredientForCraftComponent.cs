using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class IngredientForCraftComponent
{
    public int id;
    public int count;

    public int Id => id;
    public int Count => count;

    public IngredientForCraftComponent(int id, int count)
    {
        this.id = id;
        this.count = count;
    }
}

[Serializable]
public class IngredientsForCraftComponent
{
    public List<IngredientForCraftComponent> list = new List<IngredientForCraftComponent>();
}
