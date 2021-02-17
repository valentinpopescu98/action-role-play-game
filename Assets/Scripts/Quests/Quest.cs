using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

[System.Serializable]
public class Quest
{
    public bool isActive;
    public string title;
    public string description;
    public int goldReward;
    public Item itemReward;
    public GameObject questGiver;

    public QuestGoal goal;

    public void Complete()
    {
        isActive = false;
    }
}
