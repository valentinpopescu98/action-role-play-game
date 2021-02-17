using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AcceptQuest : MonoBehaviour
{
	public Button button;
	public Player player;

	QuestGiver questGiver;

	void Start()
	{
		questGiver = player.quest.questGiver.GetComponent<QuestGiver>();

		Button btn = button.GetComponent<Button>();
		btn.onClick.AddListener(TaskOnClick);
	}

	void Update()
	{
		questGiver = player.quest.questGiver.GetComponent<QuestGiver>();
	}

	void TaskOnClick()
	{
		questGiver.AcceptQuest();
	}
}
