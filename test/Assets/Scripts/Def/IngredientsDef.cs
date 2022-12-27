using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Defs/IngredientsDef", fileName = "IngredientsDef")]
public class IngredientsDef : ScriptableObject
{
    [SerializeField] private IngredientDef[] ingredients;

    public IngredientDef Get(int id)
    {
        foreach (var ingredient in ingredients)
        {
            if (ingredient.Id == id)
                return ingredient;
        }

        return default;
    }


    [Serializable]
    public struct IngredientDef
    {
        [SerializeField] private int _id;
        [SerializeField] private string name;
        [SerializeField] private Sprite sprite;


        public int Id => _id;
        public string Name => name;
        public Sprite Sprite => sprite;
    }
}
