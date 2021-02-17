using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyBehavior : MonoBehaviour
{
    public Animator animator;
    public HealthBar healthBar;
    public Transform target;
    public Player player;
    public Enemy enemy;
    public float lookRadius = 10f;
    public float searchRadius = 15.0f;
    public int damage = 10;
    public float attackRate = 1f;

    NavMeshAgent agent;
    Vector3 searchPoint;
    float nextTimeToAttack = 0f;
    float distance;
    bool hasStopped = false;
    bool isSearching = false;

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        //daca inamicul nu e mort
        if (!enemy.dead)
        {
            //daca nu esti mort
            if(!player.dead)
            {
                distance = Vector3.Distance(target.position, transform.position);

                //daca te vede
                if (distance <= lookRadius)
                {
                    healthBar.gameObject.SetActive(true);
                    animator.SetBool("IsAware", true);
                    agent.SetDestination(target.position);
                    hasStopped = false;

                    if (distance > agent.stoppingDistance + 0.5f)
                    {
                        animator.SetBool("IsAttacking", false);
                    }
                    else
                    {
                        if(player.dead)
                            animator.SetBool("IsAttacking", false);
                        else
                        {
                            FaceTarget();
                            AttackTargetContinuously();
                            animator.SetBool("IsAttacking", true);
                        }
                    }
                }

                //daca nu te vede
                else
                {
                    healthBar.gameObject.SetActive(false);
                    animator.SetBool("IsAware", false);

                    if (!hasStopped)
                    {
                        agent.SetDestination(transform.position);
                        hasStopped = true;
                    }
                    Invoke("SearchTarget", 2f);
                }
            }

            //daca esti mort
            else
            {
                healthBar.gameObject.SetActive(false);
                animator.SetBool("IsAware", false);

                if (!hasStopped)
                {
                    agent.SetDestination(transform.position);
                    hasStopped = true;
                }
                Invoke("SearchTarget", 2f);
            }
        }

        //daca inamicul e mort
        else
            healthBar.gameObject.SetActive(false);
    }

    void FaceTarget()
    {
        Vector3 direction = (target.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
    }

    void AttackTargetContinuously()
    {
        if (Time.time >= nextTimeToAttack)
        {
            nextTimeToAttack = Time.time + 1f / attackRate;
            player.TakeDamage(damage);
            animator.SetTrigger("NextAttack");
        }
    }

    void SearchTarget()
    {
        //calculeaza coordonate pozitie random
        float randX = Random.Range(-searchRadius, searchRadius);
        float randZ = Random.Range(-searchRadius, searchRadius);
        searchPoint = new Vector3(transform.position.x + randX, transform.position.y, transform.position.z + randZ);

        //daca nu ai o pozitie random
        if (!isSearching)
        {
            //mergi la pozitia random
            agent.SetDestination(searchPoint);
            //acum ai o pozitie random
            isSearching = true;
        }

        //seteaza o noua pozitie random
        if (Vector3.Distance(searchPoint, transform.position) < 1f)
            isSearching = false;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, lookRadius);

        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, searchRadius);
    }
}
