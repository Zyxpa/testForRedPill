using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class CellInAclemyIngredient : CellComponent
{
    [SerializeField] Text countHaveText;

    public UnityEvent UpdateButton = new UnityEvent();

    private int countHave;

    Color colorEhought = new Color(59.2f, 38.4f, 28.2f);
    Color colorNotEhought = new Color(74.2f, 0f, 0f);

    protected override void SetCount(int countToSet)
    {
        base.SetCount(countToSet);

        if (countHaveText != null)
        {
            countHaveText.enabled = true;
            countHave = GetCountInInventory(null);
            ChangedValueCellHave(countHave);
        }
    }

    private int GetCountInInventory(CellComponent cell)
    {
        if(cell == null)
            cell =  InventoryComponent.GetCell(Id);
        if (cell == null)
            return 0;
            cell.OnCountCahge.AddListener(ChangedValueCellHave);
        return cell.Count;
    }

    private void ChangedValueCellHave(int value)
    {
        countHave = value;
        countHaveText.text = value.ToString();
        countHaveText.color = CheckEnough() ? colorEhought : colorNotEhought;
        UpdateButton.Invoke();
    }

    internal bool CheckEnough()
    {
        return countHave >= Count;
    }

    internal void OnAddCell(CellComponent cell)
    {
        ChangedValueCellHave(GetCountInInventory(cell));
    }

    void OnDestroy()
    {
        UpdateButton.RemoveAllListeners();
    }
}
