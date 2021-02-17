using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class QuestGoal
{
    public PlayerController playerController;
    public InventoryManager inventoryManager;

    public GoalType goalType;
    public string killTag;
    public string gatheredItemName;
    public List<GameObject> targetNpcs = new List<GameObject>();

    public int requiredAmount;
    public int currentAmount;

    public bool IsReached()
    {
        return (currentAmount >= requiredAmount);
    }

    public void EnemyKilled()
    {
        if (playerController.target != null)
            if (playerController.target.tag == killTag && playerController.enemy.dead)
            {
                currentAmount++;
            }
    }

    public void ItemGathered()
    {
        foreach(Item item in inventoryManager.items)
        {
            if (item != null && item.name == gatheredItemName)
                currentAmount++;
        }
    }

    public void TalkedToNPC()
    {
        if (targetNpcs.Contains(playerController.target) && playerController.friendly.isTalking)
            currentAmount++;
    }
}

public enum GoalType
{
    Kill,
    Gather,
    Talk
}