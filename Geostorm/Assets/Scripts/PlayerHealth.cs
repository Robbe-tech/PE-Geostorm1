using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PlayerHealth : MonoBehaviour
{
    public int startingHealth = 5;                            // The amount of health the player starts the game with.
    public int currentHealth;                                   // The current health the player has.
    public Image[]  health;
    public Image damageImage;                                   // Reference to an image to flash on the screen on being hurt.
    public AudioClip deathClip;                                 // The audio clip to play when the player dies.
    public float flashSpeed = 5f;                               // The speed the damageImage will fade at.
    public Color flashColour = new Color(1f, 0f, 0f, 0.1f);     // The colour the damageImage is set to, to flash.
    private int healingAmount;

    private Image mana;
    Animator anim;                                              // Reference to the Animator component.
    PlayerMovement playerMovement;                              // Reference to the player's movement.
    bool isDead;                                                // Whether the player is dead.
    bool damaged;  
    
    public

    void Awake ()
    {
        this.enabled=true;
        mana = GameObject.Find("ManaFull").GetComponent<Image>();
        // Setting up the references.
        anim = GetComponent <Animator> ();
        playerMovement = GetComponent <PlayerMovement> ();

        // Set the initial health of the player.
        currentHealth = startingHealth;
        healingAmount=1;
        mana.fillAmount=1;

    }


    void Update ()
    {
        // If the player has just been damaged...
        if(damaged)
        {
            // ... set the colour of the damageImage to the flash colour.
            damageImage.color = flashColour;
        }
        // Otherwise...
        else
        {
            // ... transition the colour back to clear.
            damageImage.color = Color.Lerp (damageImage.color, Color.clear, flashSpeed * Time.deltaTime);
        }

        // Reset the damaged flag.
        damaged = false;
        if(Input.GetButtonDown("Heal")&&currentHealth<5)
        {
            if(mana.fillAmount==1){
            RestoreHealth();}
        }
    }


    public void TakeDamage (int amount)
    {
        // Set the damaged flag so the screen will flash.
        damaged = true;

        // Reduce the current health by the damage amount.
                currentHealth -= amount;

        health[currentHealth].enabled =false;


        // Set the health bar's value to the current health.

        // Play the hurt sound effect.

        // If the player has lost all it's health and the death flag hasn't been set yet...
        if(currentHealth <= 0 && !isDead)
        {
            // ... it should die.
            Death ();
        }
    }
    public void RestoreHealth()
    {
        currentHealth +=healingAmount;
        health[currentHealth-1].enabled =true;
        mana.fillAmount=0;


    }


    public void Death ()
    {

        // Set the death flag so this function won't be called again.
        isDead = true;

        // Turn off any remaining shooting effects.

        // Tell the animator that the player is dead.
        anim.SetTrigger ("Die");

        // Turn off the movement and shooting scripts.
        //playerMovement.enabled = false;


    }        
}

