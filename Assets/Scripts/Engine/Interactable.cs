using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    public float radius = 1.5f;
    [HideInInspector] public float fixedRadius;
    [HideInInspector] public float distance;

    Transform player;
    bool isFocused = false;
    bool hasInteracted = false;

    private void Awake()
    {
        fixedRadius = radius;
    }

    // Update is called once per frame
    void Update()
    {
        if (isFocused && !hasInteracted)
        {
            distance = Vector3.Distance(player.position, transform.position);
            if (distance <= fixedRadius)
            {
                Interact();
                hasInteracted = true;
            }
        }
    }

    public virtual void Interact()
    {

    }

    public void OnFocused(Transform playerTransform)
    {
        isFocused = true;
        player = playerTransform;
        hasInteracted = false;
    }

    public void OnDefocused()
    {
        isFocused = false;
        player = null;
        hasInteracted = false;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, fixedRadius);
    }
}
