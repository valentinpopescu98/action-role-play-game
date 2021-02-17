using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Player : MonoBehaviour
{
    public Transform player;
    public PlayerController controller;
    public Quest quest;
    public Animator animator;
    public NavMeshAgent agent;
    public HealthBar healthBar;
    public int maxHealth = 100;
    public int gold;
    public bool dead = false;

    int currentHealth;

    public GameObject sword;
    public GameObject pistol;
    public GameObject hpPotion;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
    }

    // Update is called once per frame
    void Update()
    {
        if (currentHealth <= 0 && !dead)
        {
            StartCoroutine(Die());
        }

        if(quest.isActive)
        {
            if (quest.goal.goalType == GoalType.Kill)
                quest.goal.EnemyKilled();
            if(quest.goal.goalType == GoalType.Gather)
                quest.goal.ItemGathered();
            if (quest.goal.goalType == GoalType.Talk)
                quest.goal.TalkedToNPC();

            if (quest.goal.IsReached())
            {
                gold += quest.goldReward;
                if (quest.itemReward != null)
                {
                    if (quest.itemReward.name == "Sword")
                        Instantiate(sword, player.transform.position, sword.transform.rotation);
                    else if (quest.itemReward.name == "Pistol")
                        Instantiate(pistol, player.transform.position, pistol.transform.rotation);
                    else if (quest.itemReward.name == "Health Potion")
                        Instantiate(hpPotion, player.transform.position, hpPotion.transform.rotation);
                }

                quest.Complete();

                if (quest.goal.goalType == GoalType.Gather)
                    foreach (Item item in InventoryManager.instance.items)
                        if (item.name == quest.goal.gatheredItemName)
                            if (quest.goal.requiredAmount > 0)
                            {
                                quest.goal.requiredAmount--;
                                InventoryManager.instance.Remove(item);
                            }
            }
        }
    }

    public void Heal(int health)
    {
        currentHealth += health;
        if (currentHealth > maxHealth)
            currentHealth = maxHealth;
        healthBar.SetHealth(currentHealth);
    }

    public void TakeDamage(int health)
    {
        currentHealth -= health;
        healthBar.SetHealth(currentHealth);
    }

    IEnumerator Die()
    {
        animator.SetTrigger("IsDead");
        dead = true;
        Destroy(agent);

        yield return new WaitForSeconds(3f);
        Destroy(gameObject);
    }
}
