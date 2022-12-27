using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CraftWindowComponent : MonoBehaviour
{
    [SerializeField] CellComponent resultCell;
    [SerializeField] Text resultText;
    [SerializeField] Transform igridients;
    [SerializeField] Text price;
    [SerializeField] Button button;
    [SerializeField] GameObject cellPrefub;
    [SerializeField] GameObject plusPrefub;

    List<CellInAclemyIngredient> ingridientsCells = new List<CellInAclemyIngredient>();


    internal void SetRecipe(RecipeComponent recipe)
    {
        var resultDef = DefsFacade.I.Items.Get(recipe.result.Id);

        resultText.text = resultDef.Name.ToUpper();
        price.text = recipe.cost.ToString();
        resultCell.SetCellValue(recipe.result.Id, resultDef.Sprite, recipe.result.Count);
        for (int i = 0; i < recipe.items.Count; i++)
        {
            if (i != 0)
                Instantiate(plusPrefub, igridients);
            var newCell = Instantiate(cellPrefub, igridients).GetComponentInChildren<CellInAclemyIngredient>();
            ingridientsCells.Add(newCell);
            var recipeItemDef = DefsFacade.I.Items.Get(recipe.items[i].Id);
            newCell.SetCellValue(recipe.items[i].Id, recipeItemDef.Sprite, recipe.items[i].Count);
            newCell.UpdateButton.AddListener(UpdateButton);
        }
        UpdateButton();
    }

    bool CheckForButton()
    {
        for (int i = 0; i < ingridientsCells.Count; i++)
            if(!ingridientsCells[i].CheckEnough())
                return false;
        return true;
    }

     void UpdateButton()
     {
        button.gameObject.SetActive(CheckForButton());
     }

    public void OmButtonPress()
    {
        InventoryComponent.ChengeCell.Invoke(true, new IngredientForCraftComponent(resultCell.Id, resultCell.Count));

        foreach(var ingridientsCell in ingridientsCells)
            InventoryComponent.ChengeCell.Invoke(false, new IngredientForCraftComponent(ingridientsCell.Id, ingridientsCell.Count));


    }

    internal void OnAddCell(CellComponent cell)
    {
        foreach (var ingridientsCell in ingridientsCells)
        {
            if(ingridientsCell.Id == cell.Id)
                ingridientsCell.OnAddCell(cell);
        }
    }
}
