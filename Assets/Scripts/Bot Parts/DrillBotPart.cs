using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//inherihting botpart
public class DrillBotPart : BotPart
{
    //TODO: create "private Animator drillAnimation;" // Animates the drill part when attack is active.

    [SerializeField] private Transform attackPoint; // References the attack point of the drill in the scene.
    [SerializeField] private float attackRange = 0.0f; // Range for attack to initiate.

    [SerializeField] private LayerMask enemyLayers;
    [SerializeField] private bool isRunning;
    // Used to determine which objects are enemies by assigning all ememies to a layer using a layermask.

    private float timer;

    // Inherited from BotPart
    public override void SetState(State state)
    {
        isRunning = state.isActive;
    }

    // Start is called before the first frame update
    void Start()
    {
        timer = 0.0f;
        return;
    }

    // Update is called once per frame
    void Update()
    {
        drillAttack();
    }

    // drillAttack
    private void drillAttack()
    {
        if (isRunning) {
            if (timer > 0) {
                timer -= Time.deltaTime;
            }
            else {
                timer = GetCoolDown();
                // TODO:  Play the drill attack animation.

                // Detect enemy in range of attack.
                Collider2D enemy = Physics2D.OverlapCircle(attackPoint.position, attackRange, enemyLayers);

                // Damage enemy
                // TODO: Implement damage to enemy health.

                if(enemy)
                {
                    // Outputs message to Unity Editor Console to verify the attack.
                    Debug.Log(enemy.name + " was attacked by drill.");
                }
            }
        }
    }

    // Allows developer/user to see the attack radius in Unity editor.
    void OnDrawGizmosSelected()
    {
        if (attackPoint == null) return; //Return if attackPoint has not been set.

        // Draw a wire sphere at attack position to show its range in Unity editor.
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
}
