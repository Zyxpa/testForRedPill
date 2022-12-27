using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;


public class InventoryComponent : MonoBehaviour
{

    [SerializeField]  RectTransform parent;
    [SerializeField]  GameObject prefabOfCelll;

    static List<CellComponent> cells = new List<CellComponent>();
    static int lenght;

    static public UnityAction<bool, IngredientForCraftComponent> ChengeCell;

    void Start()
    {
        cells = GetComponentsInChildren<CellComponent>().ToList<CellComponent>();
        InitData();
        ChengeCell += ChangeCell;
    }

    private void InitData()
    {
        string savePath = Application.dataPath + "/Resources/InventoryData.json";

        if (File.Exists(savePath))
        {
            string readText = File.ReadAllText(savePath);
            IngredientsForCraftComponent data = JsonUtility.FromJson<IngredientsForCraftComponent>(readText);
            AddItems(data.list);
        }
    }

    public void AddItems(List<IngredientForCraftComponent> items)
    { 
        foreach(var item in items)
        {
            AddItem(item);
        }
    }

    public void  AddItem(IngredientForCraftComponent item)
    {
        
        if (TryAddCount(item.Id, item.Count)) return;

        if (lenght >= cells.Count)
        { 
            var newCell = Instantiate(prefabOfCelll, parent).GetComponentInChildren<CellComponent>() ;
            cells.Add(newCell);
        }

        var defOfItem = DefsFacade.I.Items.Get(item.Id);
        cells[lenght].SetCellValue(defOfItem.Id, defOfItem.Sprite, item.Count);
        AlchemyWindowController.OnAddCell(cells[lenght]);
        lenght++;

    }

    bool TryAddCount(int idOfItem, int countOfItem)
    {
        for (int i = 0; i < cells.Count; i++)
        {
            if (cells[i].Id == idOfItem)
            {
                cells[i].ChengeCount(countOfItem);
                return true;
            }
        }
        return false;
    }

    [ContextMenu("Test InitData")]
    public void Test()
    {
        InitData();
    }

    static public CellComponent GetCell(int id)
    {
        for (int i = 0; i < cells.Count; i++)
            if (cells[i].Id == id)
                return cells[i];
        return null;
    }

    public void Subtract(int id, int value)
    {
        for (int i = 0; i < cells.Count; i++)
            if (cells[i].Id == id)
            {
                cells[i].ChengeCount(-value);
                if (cells[i].Count == 0)
                    RemoveCell(i);
            }
            
    }

    void RemoveCell(int cellIndex)
    {
        lenght--;
        cells[cellIndex].DestroyCell();
        var removedCell = cells[cellIndex];
        cells.Remove(removedCell);
        cells.Add(removedCell);
    }

    public void ChangeCell(bool add, IngredientForCraftComponent ingredient)
    {
        if (add)
            AddItem(ingredient);
        else
            Subtract(ingredient.id, ingredient.count);
    }

    void OnDestroy()
    {
        ChengeCell -= ChangeCell;
    }

}
