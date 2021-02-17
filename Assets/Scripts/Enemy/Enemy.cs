using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public Animator animator;
    public HealthBar healthBar;
    public int maxHealth = 100;
    public bool dead = false;

    int currentHealth;

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
    }

    public void TakeDamage(int health)
    {
        currentHealth -= health;
        healthBar.SetHealth(currentHealth);
    }

    IEnumerator Die()
    {
        Random.seed = System.DateTime.Now.Millisecond;

        if (Random.Range(0, 2) == 0)
            Instantiate(hpPotion, transform.position, hpPotion.transform.rotation);

        animator.SetTrigger("IsDead");
        dead = true;

        yield return new WaitForSeconds(3f);
        Destroy(gameObject);
    }
}
