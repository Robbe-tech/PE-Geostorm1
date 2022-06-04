using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class PlayerAttack : MonoBehaviour

{
    bool enemyInRange;    
    public int damage;
    private  EnemyAttack enemyAttack;
    public Animator animator;
    GameObject EnemyHitbox;
    [SerializeField] private Image mana;
    [SerializeField] private float fillAdd;
    bool CrystalInRange;
    public CrystalUI CrystalUI;
    private GameObject crystal;
    
     void Awake ()
    {
        // Setting up the references.
        mana.fillAmount=5;

    }
    void Update()
    {

          if(Input.GetButtonDown("Fire1"))
        {
    
		animator.SetBool("Attack", true);
    
        }
        else{
        animator.SetBool("Attack", false);

        }
           if(Input.GetButtonDown("Fire1")&&enemyInRange&&enemyAttack.enemyHealth>0)
        {
           
            mana.fillAmount +=fillAdd;
            Debug.Log(mana.fillAmount);

    		  enemyAttack.enemyHealth -= damage;


        } 
          if(Input.GetButtonDown("Fire1")&&CrystalInRange){
            CrystalUI.crystalAmount +=1;
            Destroy(crystal);

          }
        {
           
         


        }
         }
             void OnTriggerEnter2D (Collider2D other)
                {
                    // If the entering collider is the player...
                    if(other.gameObject.tag == "Hitbox")
                    {
                        enemyInRange = true;
                    }
                      if(other.gameObject.tag == "Enemy")
                    {
                    enemyAttack=other.GetComponent<EnemyAttack>();
                    }
                  
                      if(other.tag == "Crystal")
                    {
                        // ... the player is in range.
                        CrystalInRange = true;
                        crystal= other.gameObject;
                    }
                }
                 
 void OnTriggerExit2D (Collider2D other)
    {
        // If the exiting collider is the player...
        if(other.gameObject == EnemyHitbox)
        {
            // ... the player is no longer in range.
            enemyInRange = false;
        }
        if(other.gameObject.tag == "Crystal")
                    {
                        // ... the player is in range.
                        CrystalInRange = false;
                    }
    }
    

   

}
