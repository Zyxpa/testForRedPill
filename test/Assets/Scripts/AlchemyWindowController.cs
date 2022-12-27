using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class AlchemyWindowController : WindowController
{
    [SerializeField] GameObject craftWindowModel;
    [SerializeField] Transform layout;

    static List<CraftWindowComponent> craftWindows = new List<CraftWindowComponent>();

    void OnEnable()
    { 
        InitData();
    }

    private void InitData()
    {
        string savePath = Application.dataPath + "/Resources/RecipeData.json";
        if (File.Exists(savePath))
        {
            string readText = File.ReadAllText(savePath);
            Recipes data = JsonUtility.FromJson<Recipes>(readText);
            foreach(RecipeComponent recipe in data.list)
                SetRecipe(recipe);
        }
    }

    void SetRecipe(RecipeComponent recipe)
    {
        var craftWindow = Instantiate(craftWindowModel, layout).GetComponentInChildren<CraftWindowComponent>();
        craftWindows.Add(craftWindow);
        craftWindow.SetRecipe(recipe);
    }

    public override void OnClose()
    {
        base.OnClose();
        ClearRecipes();
    }

    void ClearRecipes()
    {
        foreach (var craftWindow in craftWindows) 
            Destroy(craftWindow.gameObject);
        craftWindows.Clear();
    }

    static public void OnAddCell( CellComponent cell)
    {
        foreach (var craftWindow in craftWindows)
            craftWindow.OnAddCell(cell);
    }
}