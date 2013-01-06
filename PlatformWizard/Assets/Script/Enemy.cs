using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour
{
    public float attackDamange = 5;
    public float attackRange = 5;
    public float MAXLIFE = 100;
    public float attackInterval = 1.5f;

    public float speed = 5;

    protected float closesYouCanBeToTarget;
    protected float nextAttack = 0;
    protected float damangeToDeal;
    protected float distanceFromTarget;
    protected float life;

  

    private float closesYouCanBeToPlayer;
    private float lastLife;

    protected Player player;

    private bool hasNotAttacked = true;


	// Use this for initialization
	public void  Start ()
    {
	    life = MAXLIFE;
	    lastLife = 0;
	    
       
	}
	
	// Update is called once per frame
	 public void  Update ()
	{
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        distanceFromTarget = Vector2.Distance(player.transform.position, transform.position);
        
         isDead();
      // locatePlayerAndMoveToHim();
	     howCloseCanWeBeToTarget();
	    attack();

	}


    public void howCloseCanWeBeToTarget()
    {

        closesYouCanBeToPlayer = player.transform.lossyScale.x;


    }


 /*  void locatePlayerAndMoveToHim()
    {
        if (distanceFromTarget < lookRange)
        {
            player.hasEnemySeenYou = true;
           
            if(distanceFromTarget > closesYouCanBeToPlayer)
            {
              if(transform.position.x < player.transform.position.x)
              {
                  transform.Translate(Vector3.right*speed*Time.deltaTime);
              }
              if (transform.position.x > player.transform.position.x)
              {
                  transform.Translate(Vector3.left * speed * Time.deltaTime);
              }
            }
        
           
        }
    } */



   public virtual void takeDamage(float damage)
   {
       life -= damage;
      
   }

   public virtual void  isDead()
   {
       if (life <= 0)
       {
           
   
           Destroy(gameObject);
       }
   }

   public void attack()
    {
        if (distanceFromTarget < attackRange)
        {
            if (hasNotAttacked)
            {
                nextAttack = Time.time + attackInterval-1;
                hasNotAttacked = false;
            }
            if (nextAttack <= Time.time)
            {
                nextAttack = Time.time + attackInterval;
                damangeToDeal = Mathf.Abs(attackDamange -distanceFromTarget);
                Debug.Log("Damage : " + damangeToDeal);
                player.takeDamage(damangeToDeal);
            }
        }
    }

   

 
 

    void OnTriggerStay(Collider collisionObject)
    {
        if (collisionObject.tag == "FallingIceCube")
        {
            takeDamage(100);
        }


       
    }
   
}
