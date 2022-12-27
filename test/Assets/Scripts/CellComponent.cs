using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class CellComponent : MonoBehaviour
{
    [SerializeField] Image image;
    [SerializeField] Text countText;
    [SerializeField] Image countImage;

    public CellEvent OnCountCahge = new CellEvent();

    private int id = -1;
    private int count;

    public int Id => id;
    public int Count => count;


    void SetImage(Sprite other)
    {
        image.enabled = true;
        image.sprite = other;
    }

    protected virtual void SetCount(int countToSet)
    {
        count = countToSet;

        if (countImage != null)
            countImage.enabled = true;

        if (countText != null)
        {
                countText.enabled = true;
                countText.text = countToSet.ToString();
        }
    }

    internal void SetCellValue(int id, Sprite sprite, int count)
    {
        this.id = id;
        SetImage(sprite);
        SetCount(count);
    }

    internal void ChengeCount(int countToAdd)
    {
        SetCount(count + countToAdd);
        OnCountCahge.Invoke(count);
    }

    internal void DestroyCell()
    {
        id = -1;
        count = 0;
        image.enabled = false;
        countImage.enabled = false;
        countText.enabled = false;
        transform.SetAsLastSibling();
    }


    void OnDestroy()
    {
        OnCountCahge.RemoveAllListeners();
    }

    public class CellEvent : UnityEvent<int> { }
}
