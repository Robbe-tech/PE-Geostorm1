using UnityEngine;
using System.Collections;


[SerializeField]public class  EnemyAttack : MonoBehaviour
{
    public float timeBetweenAttacks = 1f;     // The time in seconds between each attack.
       bool playerInRange;    
    public int attackDamage = 1;               // The amount of health taken away per attack.


    private Animator anim;  
    GameObject player;      
  
                      // Reference to the player GameObject.
    PlayerHealth playerHealth;                  // Reference to the player's health.
                      // Whether player is within the trigger collider and can be attacked.
    float timer;    
    public int enemyHealth;   
    [SerializeField] AudioSource damageFX;                          // Timer for counting up to the next attack.

    void Awake ()
    {
        anim = this.GetComponent<Animator>();
        // Setting up the references.
        player = GameObject.FindGameObjectWithTag ("Player");
        playerHealth = player.GetComponent <PlayerHealth> ();
    }


   private void Update ()
    {
        // Add the time since Update was last called to the timer.
         timer += Time.deltaTime;

        // If the timer exceeds the time between attacks, the player is in range and this enemy is alive...
        if(timer >= timeBetweenAttacks && playerInRange && enemyHealth > 0)
        {
            // ... attack.
            Attack ();
        }
            if(enemyHealth<=0){
                    anim.SetBool("Dead",true);
     if (anim.GetCurrentAnimatorStateInfo(0).IsName("Bat Die"))
        {
        this.gameObject.SetActive(false);
        };

            } 
    }
       
   
    
    
    void OnTriggerEnter2D (Collider2D other)
    {
        // If the entering collider is the player...
        if(other.gameObject == player)
        {
            // ... the player is in range.
            playerInRange = true;
        }
        timer = 0f;

    }


    void OnTriggerExit2D (Collider2D other)
    {
        // If the exiting collider is the player...
        if(other.gameObject == player)
        {
            // ... the player is no longer in range.
            playerInRange = false;
        }
    }




    void Attack ()
    {
        // Reset the timer.
        timer = 0f;

        // If the player has health to lose...
        if(playerHealth.currentHealth > 0)
        {
            // ... damage the player.
            playerHealth.TakeDamage (attackDamage);
            damageFX.Play();
        }
    }
    
}
