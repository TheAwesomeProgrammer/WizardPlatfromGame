using UnityEngine;
using System.Collections;

public class Player : AttackScript {

    public float MAXSPEED = 6.0F;
    public float accSpeed = 0.1f;
    public float jumpSpeed = 8.0F;
    public float gravity = 20.0F;
    public float secoundsToJump = 2;
    public float maxRage = 100;
    public float AttackSpeed = 0.5f;
    public float YPostionToDieAt = 50;

    public bool hasEnemySeenYou { get; set; }

    public float rage { get; set; }
    public float speed { get; set; }
    public float horizontalMovement { get; set; }
    public bool isLatter { get; set; }

    public GameObject health;
   

    private Vector3 moveDirection = Vector3.zero;
    private Vector3 startVector;
    
    private float life;
    private float mTimeToShoot = float.MinValue;

    private bool hasScreenChangedOrIsStart = true;

    private Enemy mEnemy;
    private Enemy target;

    private GameObject[] mtarget;

	// Use this for initialization
	void Start ()
	{
        
      //  Screen.SetResolution(1600, 900, true);
	    life = MAXLIFE;
  
        isLatter = false;
	    startVector = transform.position;
    }
   
    void Update()
    { 
        mtarget = GameObject.FindGameObjectsWithTag("Enemy");
        if (mtarget.Length > 0)
        {
            mEnemy =  findTarget("Enemy").GetComponent<Enemy>();
            target = mEnemy;
            distanceFromTarget = Vector3.Distance(target.transform.position, transform.position);
        }
       
            attack(); 
        
     
        move();
        isDead();
        hasEnemySeenYou = false;
      
    }

   
   

    public GameObject findTarget(string tag)
    {
        float minX = float.MaxValue;
        GameObject tempTarget = null;
        foreach (GameObject targetTemp in GameObject.FindGameObjectsWithTag(tag))
        {
            float tempDistance = Vector2.Distance(transform.position, targetTemp.transform.position);
            if (tempDistance < minX)
            {
                minX = tempDistance;
                tempTarget = targetTemp;
            }

        }

        return tempTarget;
    
    
}

    public void takeDamage(float damage)
    {
        float tSpeedToLoseLifeWith = 15f * (damage / 5); ;
        health.transform.Translate(Vector3.left * Time.deltaTime * tSpeedToLoseLifeWith);
        life -= damage;
        
    }

    void isDead()
    {
        if (life <= 0 || transform.position.y <= -YPostionToDieAt)
        {
            Application.LoadLevel(Application.loadedLevel);
        }
    }

   

    void move()
    {
        CharacterController controller = GetComponent<CharacterController>();
        horizontalMovement = Input.GetAxis("Horizontal");
        
        if (speed < MAXSPEED)
        {
            speed += accSpeed * Time.deltaTime;
        }

        if (controller.isGrounded)
        {
            moveDirection = new Vector3(horizontalMovement * speed * Time.deltaTime, 0, 0);

            moveDirection = transform.TransformDirection(moveDirection);
            moveDirection *= speed;
      
            if (Input.GetButton("Jump") && controller.isGrounded)
            {
                moveDirection.y = jumpSpeed;
            }
        }

        if (isLatter)
        {
            moveDirection.y = 0;
            moveDirection = new Vector3(horizontalMovement * speed * Time.deltaTime, Input.GetAxis("Vertical") * speed * Time.deltaTime, 0);
            moveDirection = transform.TransformDirection(moveDirection);
            moveDirection *= speed;
        }

           moveDirection.y -= gravity * Time.deltaTime;

        controller.Move(moveDirection * Time.deltaTime);
    }

    void OnControllerColliderHit(ControllerColliderHit hit)
    {
        Debug.Log("hit name : " + hit.collider.name);
        if(hit.collider.tag =="Spike")
        {
            takeDamage(100);
        }
        
        if (hit.collider.tag == "FallingIceCube")
        {
            FallingIceCubes tfallingIceCube = hit.collider.GetComponent<FallingIceCubes>();
            if (tfallingIceCube.whenToFall == float.MaxValue)
            {
                tfallingIceCube.whenToFall = Time.time + tfallingIceCube.touchDelay;
            }  
        }

        if (hit.collider.tag == "NormalBox" && GetComponent<AnimationPlayer>().mIsAnimating)
        {
            Destroy(hit.collider.gameObject);
        }

        if (hit.collider.tag == "Latter")
        {
            isLatter = true;
        }
    }

    public void attack()
    {
        if (Input.GetMouseButtonDown(0) && mTimeToShoot < Time.time)
        {
            mTimeToShoot =AttackSpeed + Time.time;
            GameObject.Find("Aim").GetComponent<Aim>().Shot();
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
}
