using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Friendly : MonoBehaviour
{
    public Animator animator;
    public QuestGiver questGiver;
    public bool isQuestGiver;
    public bool isTalking = false;

    GameObject player;
    Vector3 lookVector;
    Quaternion oldRot;
    Quaternion newRot;
    Vector3 oldPos;
    Vector3 newPos;

    void Start()
    {
        oldRot = transform.rotation;

        if (GameObject.Find("Player") != null)
            player = GameObject.Find("Player");

        if (gameObject.name == "erika_archer_bow_arrow")
        {
            oldPos = new Vector3(transform.position.x, 0f, transform.position.z);
            newPos = new Vector3(transform.position.x, 0.225f, transform.position.z);
        }
        else if (gameObject.name == "brute")
        {
            oldPos = new Vector3(transform.position.x, 0.1f, transform.position.z);
            newPos = new Vector3(transform.position.x, 0f, transform.position.z);
        }
        else if (gameObject.name == "peasant_girl")
        {
            oldPos = new Vector3(transform.position.x, 0f, transform.position.z);
            newPos = new Vector3(transform.position.x, 0.245f, transform.position.z);
        }
        else if (gameObject.name== "peasant_man")
        {
            oldPos = new Vector3(transform.position.x, -0.25f, transform.position.z);
            newPos = new Vector3(transform.position.x, 0f, transform.position.z);
        }
        else if (gameObject.name == "eve_j_gonzales")
        {
            oldPos = new Vector3(transform.position.x, -0.27f, transform.position.z);
            newPos = new Vector3(transform.position.x, 0f, transform.position.z);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (GameObject.Find("Player") != null)
            lookVector = player.transform.position - transform.position;
        else
            lookVector = transform.position;

        newRot = Quaternion.LookRotation(lookVector);

        if (isTalking && !questGiver.quest.isActive)
        {
            transform.rotation = Quaternion.Slerp(newRot, oldRot, 1f * Time.deltaTime);
            transform.position = newPos;
            animator.SetBool("IsTalking", true);
            if (isQuestGiver)
            {
                questGiver.OpenQuestWindow();
                player.GetComponent<Player>().quest.questGiver = questGiver.quest.questGiver;
            }
        }
        else
        {
            transform.rotation = Quaternion.Slerp(oldRot, newRot, 1f * Time.deltaTime);
            transform.position = oldPos;
            animator.SetBool("IsTalking", false);
        }
    }
}
