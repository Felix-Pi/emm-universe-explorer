﻿using System;
using System.Collections.Generic;

/// <summary>
///   Represents an Inventory
/// </summary>
public class Inventory
{
    public event EventHandler OnItemAdded;
    public event EventHandler OnItemRemoved;

    private List<Item> itemList;

    /// <summary>
    ///   Initialise the Item List
    /// </summary>
    public Inventory()
    {
        itemList = new List<Item>();
    }

    /// <summary>
    ///   Adds item to the Inventory and triggers OnItemAdded Event
    /// </summary>
    /// <param name="item">The Item which is to be added</param>
    public void AddItem(Item item)
    {
        bool itemPresents = false;
        foreach (Item inventoryItem in itemList)
        {
            if (inventoryItem.itemType == item.itemType)
            {
                inventoryItem.amount += item.amount;
                itemPresents = true;
            }
        }
        if (!itemPresents)
        {
            itemList.Add(item);
        }
        OnItemAdded?.Invoke(this, EventArgs.Empty);
    }

    /// <summary>
    ///   Removes item from the Inventory and triggers OnItemRemoved Event
    /// </summary>
    /// <param name="item">The Item which is to be removed</param>
    public void RemoveItem(Item item)
    {
        Item found = null;
        foreach (Item inventoryItem in itemList)
        {
            if (inventoryItem.itemType == item.itemType)
            {
                inventoryItem.amount -= item.amount;
                found = inventoryItem;
            }
        }
        if (found != null && found.amount <= 0)
        {
            itemList.Remove(found);
        }
        OnItemRemoved?.Invoke(this, EventArgs.Empty);
    }

    /// <summary> 
    ///   Returns the item list of this Inventory
    /// </summary>
    public List<Item> GetItemList()
    {
        return itemList;
    }

    /// <summary> 
    ///   Calculates the total number of items based on the amount of each item and returns it 
    /// </summary>
    public int GetTotalItemsCount()
    {
        int count = 0;
        foreach (Item item in itemList)
        {
            count += item.amount;
        }
        return count;
    }
}
