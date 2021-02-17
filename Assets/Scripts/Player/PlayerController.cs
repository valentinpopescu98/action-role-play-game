using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.EventSystems;

public class PlayerController : MonoBehaviour
{
    public Animator animator;
    public Player player;
    public ParticleSystem muzzleFlash;
    public LineRenderer bulletTrail;
    public Transform muzzleExit;
    public GameObject sword;
    public GameObject pistol;
    public int equipSlot = 1;
    [HideInInspector] public Interactable focus;
    [HideInInspector] public GameObject target = null;
    [HideInInspector] public Enemy enemy;
    [HideInInspector] public Friendly friendly;
    [HideInInspector] public bool[] slotEquipped;
    public LayerMask groundMask;

    NavMeshAgent agent;
    HealthBar enemyHealthBar;
    float distance;
    bool keepFollowing = false;
    int damage = 10;
    float attackRate = 1.3f;
    float nextTimeToAttack = 0f;

    void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        slotEquipped = new bool[9];

        slotEquipped[0] = true;
        for (int i = 1; i < 9; i++)
        {
            slotEquipped[i] = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (EventSystem.current.IsPointerOverGameObject())
            return;

        if (target != null)
            distance = Vector3.Distance(target.transform.position, transform.position);

        if (agent != null)
            if (!agent.pathPending)
                if (agent.remainingDistance <= agent.stoppingDistance)
                    if (!agent.hasPath || agent.velocity.sqrMagnitude == 0f)
                        animator.SetBool("IsRunning", false);

        //cat timp ai o tinta si nu esti mort, urmareste tinta
        IEnumerator FollowTarget()
        {
            while (target != null && !player.dead)
            {
                agent.SetDestination(target.transform.position);
                yield return null;
            }

            yield return 0;
        }

        IEnumerator FaceTarget()
        {
            while (target != null && !player.dead)
            {
                Vector3 direction = (target.transform.position - transform.position).normalized;
                Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
                transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
                yield return null;
            }

            yield return 0;
        }

        //daca nu esti mort
        if (!player.dead)
        {
            //daca dezechipezi arma sa echipezi automat pumnii
            if (slotEquipped[equipSlot - 1] == false)
            {
                equipSlot = 1;
                pistol.SetActive(false);
                sword.SetActive(false);
                animator.SetTrigger("IsUnarmed");
                damage = 10;
                attackRate = 1.3f;
            }

            //click stanga
            if (Input.GetButtonDown("Move"))
            {
                agent.isStopped = false;

                RemoveFocus();
                target = null;
                keepFollowing = true;
                animator.SetBool("IsAttacking", false);
                animator.SetBool("IsRunning", true);
            }
            else if (Input.GetButtonUp("Move"))
            {
                keepFollowing = false;
            }

            //click dreapta
            if (Input.GetButtonDown("Interact"))
            {
                agent.isStopped = false;

                Ray mouseClickRay = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;

                if (Physics.Raycast(mouseClickRay, out hit, 100))
                {
                    Interactable interactable = hit.collider.GetComponent<Interactable>();
                    if (interactable != null)
                    {
                        SetFocus(interactable);
                    }

                    //daca ai dat click pe un inamic
                    if (hit.collider.gameObject.layer == LayerMask.NameToLayer("Enemy"))
                    {
                        target = hit.collider.gameObject;
                        distance = Vector3.Distance(target.transform.position, transform.position);
                        enemy = hit.transform.gameObject.GetComponent<Enemy>();
                        enemyHealthBar = enemy.healthBar;
                    }
                    //daca ai dat click pe un prieten
                    else if (hit.collider.gameObject.layer == LayerMask.NameToLayer("Friendly"))
                    {
                        target = hit.collider.gameObject;
                        distance = Vector3.Distance(target.transform.position, transform.position);
                        friendly = hit.transform.gameObject.GetComponent<Friendly>();
                    }
                    //daca ai dat click pe un item sau pe o arma
                    else if (hit.collider.gameObject.layer == LayerMask.NameToLayer("Item") || hit.transform.gameObject.layer == LayerMask.NameToLayer("Weapon"))
                    {
                        agent.SetDestination(hit.point);
                        animator.SetBool("IsRunning", true);
                    }
                    //daca ai dat click pe altceva (pamant), du-te in punctul ala
                    else
                    {
                        RemoveFocus();
                        target = null;
                        agent.SetDestination(hit.point);
                        animator.SetBool("IsAttacking", false);
                        animator.SetBool("IsRunning", true);
                    }
                }
            }

            //S
            if (Input.GetButtonDown("Stop"))
            {
                agent.isStopped = true;
                animator.SetBool("IsRunning", false);
            }

            //1
            if (Input.GetButtonDown("Slot 1"))
            {
                if (slotEquipped[0])
                    if (equipSlot != 1)
                    {
                        equipSlot = 1;
                        pistol.SetActive(false);
                        sword.SetActive(false);
                        animator.SetTrigger("IsUnarmed");
                        damage = 10;
                        attackRate = 1.3f;
                    }
            }

            //2
            if (Input.GetButtonDown("Slot 2"))
            {
                if (slotEquipped[1])
                    if (equipSlot != 2)
                    {
                        equipSlot = 2;
                        pistol.SetActive(false);
                        sword.SetActive(true);
                        animator.SetTrigger("HasEquippedSword");
                        damage = 20;
                        attackRate = 1.5f;
                    }
            }

            //3
            if (Input.GetButtonDown("Slot 3"))
            {
                if (slotEquipped[2])
                    if (equipSlot != 3)
                    {
                        equipSlot = 3;
                        sword.SetActive(false);
                        pistol.SetActive(true);
                        animator.SetTrigger("HasEquippedPistol");
                        damage = 20;
                        attackRate = 1f;
                    }
            }

            //4
            if (Input.GetButtonDown("Slot 4"))
            {
                if (slotEquipped[3])
                    if (equipSlot != 4)
                    {
                        equipSlot = 4;
                    }
            }

            //5
            if (Input.GetButtonDown("Slot 5"))
            {
                if (slotEquipped[4])
                    if (equipSlot != 5)
                    {
                        equipSlot = 5;
                    }
            }

            //6
            if (Input.GetButtonDown("Slot 6"))
            {
                if (slotEquipped[5])
                    if (equipSlot != 6)
                    {
                        equipSlot = 6;
                    }
            }

            //7
            if (Input.GetButtonDown("Slot 7"))
            {
                if (slotEquipped[6])
                    if (equipSlot != 7)
                    {
                        equipSlot = 7;
                    }
            }

            //8
            if (Input.GetButtonDown("Slot 8"))
            {
                if (slotEquipped[7])
                    if (equipSlot != 8)
                    {
                        equipSlot = 8;
                    }
            }

            //9
            if (Input.GetButtonDown("Slot 9"))
            {
                if (slotEquipped[8])
                    if (equipSlot != 9)
                    {
                        equipSlot = 9;
                    }
            }

            //cat timp ai targetat un inamic
            if (target != null && target.gameObject.layer == LayerMask.NameToLayer("Enemy") && !enemy.dead)
            {
                //daca ai echipat pumnul sau sabia
                if (equipSlot == 1 || equipSlot == 2)
                {
                    //daca nu esti langa inamic, urmareste-l
                    if (distance > agent.stoppingDistance + 1.5f)
                    {
                        animator.SetBool("IsAttacking", false);
                        StartCoroutine(FollowTarget());
                        animator.SetBool("IsRunning", true);
                    }
                    //daca esti langa inamic, ataca-l melee
                    else
                    {
                        //daca l-ai omorat deja, nu mai continua sa il ataci
                        if(enemy.dead)
                            animator.SetBool("IsAttacking", false);
                        else
                        {
                            animator.SetBool("IsRunning", false);
                            StartCoroutine(FaceTarget());
                            AttackTargetContinuously();
                            animator.SetBool("IsAttacking", true);
                        }
                    }
                }
                //daca ai echipat pistolul
                else if (equipSlot == 3)
                {
                    if (enemy.dead)
                        animator.SetBool("IsAttacking", false);
                    else
                    {
                        StartCoroutine(FaceTarget());
                        AttackTargetContinuously();
                        animator.SetBool("IsAttacking", true);
                        enemyHealthBar.gameObject.SetActive(true);
                    }
                }
            }

            //cat timp ai targetat un prieten
            if (target != null && target.gameObject.layer == LayerMask.NameToLayer("Friendly"))
            {
                //daca nu esti langa prieten, urmareste-l
                if (distance > agent.stoppingDistance + 1.5f)
                {
                    agent.SetDestination(target.transform.position - new Vector3(1f, 0f, 1f));
                    animator.SetBool("IsRunning", true);
                }
                //daca esti langa prieten, vorbeste cu el
                else
                {
                    friendly.isTalking = true;
                    animator.SetBool("IsRunning", false);
                }
            }
            //daca ai deselectat prietenul sau te-ai indepartat de el, nu mai vorbi cu el
            else
            {
                if (friendly != null)
                    friendly.isTalking = false;
            }

            //tine flag-ul true cat timp tii apasat click stanga
            if (keepFollowing == true)
            {
                Ray mouseClickRay = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;

                if(Physics.Raycast(mouseClickRay, out hit, 100, groundMask))
                {
                    agent.SetDestination(hit.point);
                }
            }
        }
    }

    void SetFocus(Interactable newFocus)
    {
        if (newFocus != focus)
        {
            if (focus != null)
                focus.OnDefocused();

            focus = newFocus;
        }

        newFocus.OnFocused(transform);
    }

    void RemoveFocus()
    {
        if (focus != null)
            focus.OnDefocused();

        focus = null;
    }

    void AttackTargetContinuously()
    {
        if (Time.time >= nextTimeToAttack)
        {
            nextTimeToAttack = Time.time + 1f / attackRate;
            enemy.TakeDamage(damage);
            if (equipSlot == 3)
                BulletEffects();
            animator.SetTrigger("NextAttack");
        }
    }

    void BulletEffects()
    {
        muzzleFlash.Play();

        GameObject bulletTrailEffect = Instantiate(bulletTrail.gameObject, muzzleExit.position, Quaternion.identity);
        LineRenderer lr = bulletTrailEffect.GetComponent<LineRenderer>();
        lr.SetPosition(0, muzzleExit.position);
        lr.SetPosition(1, muzzleExit.position + muzzleExit.forward * 20);

        Destroy(bulletTrailEffect, 1f);
    }
}
