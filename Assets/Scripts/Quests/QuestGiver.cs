using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestGiver : MonoBehaviour
{
    public Quest quest;
    public Friendly friendly;
    public Player player;

    public GameObject questWindow;
    public Text titleText;
    public Text descriptionText;
    public Text goldText;
    public Text itemName;

    public void OpenQuestWindow()
    {
        questWindow.SetActive(true);
        titleText.text = quest.title;
        descriptionText.text = quest.description;
        goldText.text = quest.goldReward.ToString();
        if (quest.itemReward == null)
            itemName.text = "";
        else
            itemName.text = quest.itemReward.name;
    }

    public void AcceptQuest()
    {
        quest.isActive = true;
        player.quest = quest;
        quest.goal.currentAmount = 0;

        questWindow.SetActive(false);
        player.controller.target = null;
        friendly.isTalking = false;
    }

    public void DeclineQuest()
    {
        questWindow.SetActive(false);
        player.controller.target = null;
        friendly.isTalking = false;
    }
}
