using UnityEngine;
using System.Collections;

public enum MaterialsGoblin
{
    Walking1,
    Walking2,
    Walking3,
    Walking4,
    Walking5,
    Standing
}

public class Golin : Enemy {

     public float TimeBetweenFrames = .1f;
     public float ChanceToStop = 20;
     public float ChanceToWalk = 30;
     public float ChanceToTurnAround = 40;
     public float IntervalChance;
     public float WaitForThisLong;


    public Material Walking1Left;
    public Material Walking2Left;
    public Material Walking3Left;
    public Material Walking4Left;
    public Material Walking5Left;

    public Material Walking1Right;
    public Material Walking2Right;
    public Material Walking3Right;
    public Material Walking4Right;
    public Material Walking5Right;

    public Material StandingRight;
    public Material StandingLeft;

    private float mAnimateTime = float.MinValue;
    private float mChangeTime = float.MinValue;

    private int mDirection = 1;

    private MaterialsGoblin mMaterials;

    private bool mWalk = true;

	// Use this for initialization
	void Start ()
	{
        mMaterials = MaterialsGoblin.Walking1;
	    life = MAXLIFE;
	}
	
	// Update is called once per frame
	void Update () {
        isDead();
        Animate();
        if(mWalk)
        {
            Move();
        }
     
        if(mChangeTime <= Time.time)
        {
            mChangeTime = IntervalChance + Time.time;
            mWalk = true;
            mMaterials = MaterialsGoblin.Walking1;
            
            Change();
        }
	}

    void Move()
    {
        if (mDirection == 1)
        {
            transform.Translate(Vector3.left * speed * Time.deltaTime);
        }

        if (mDirection == -1)
        {
            transform.Translate(Vector3.right * speed * Time.deltaTime);
        }
    
        
    }

    void Change()
    {
        float tRandomNumber = Random.Range(1, 100);
        if (tRandomNumber <= ChanceToStop)
        {
            mWalk = false;
            mMaterials = MaterialsGoblin.Standing;
        }
        tRandomNumber = Random.Range(1, 100);
        if (tRandomNumber <= ChanceToWalk)
        {
            
        }
        tRandomNumber = Random.Range(1, 100);
        if (tRandomNumber <= ChanceToTurnAround)
        {
            mDirection *= -1;
        }
    }

    public override void isDead()
    {
        base.isDead();
    }

    void Animate()
    {
      
        if (mAnimateTime < Time.time)
        {
            
            mAnimateTime = TimeBetweenFrames + Time.time;

            if (mMaterials == MaterialsGoblin.Walking1)
            {
                mMaterials = MaterialsGoblin.Walking2;
                if (mDirection == 1)
                    renderer.material = Walking2Left;
                else
                    renderer.material = Walking2Right;
            }
           
            else if (mMaterials == MaterialsGoblin.Walking2)
            {
               
                mMaterials = MaterialsGoblin.Walking3;
                if (mDirection == 1)
                    renderer.material = Walking3Left;
                else
                    renderer.material = Walking3Right;
            }
          
            else if (mMaterials == MaterialsGoblin.Walking3)
            {
                mMaterials = MaterialsGoblin.Walking4;
                if (mDirection == 1)
                    renderer.material = Walking4Left;
                else
                    renderer.material = Walking4Right;
            }
          
            else if (mMaterials == MaterialsGoblin.Walking4)
            {
                mMaterials = MaterialsGoblin.Walking5;
                if (mDirection == 1)
                    renderer.material = Walking5Left;
                else
                    renderer.material = Walking5Right;
            }

            else if (mMaterials == MaterialsGoblin.Walking5)
            {
             
                mMaterials = MaterialsGoblin.Walking1;
                if (mDirection == 1)
                    renderer.material = Walking1Left;
                else
                    renderer.material = Walking1Right;
            }
            else if (mMaterials == MaterialsGoblin.Standing)
            {
                if (mDirection == 1)
                    renderer.material = StandingLeft;
                else
                    renderer.material = StandingRight;
            }

    
        }
    }

    void OnTriggerEnter(Collider pCollider)
    {

        if (pCollider.tag == "Cube")
        {
            mDirection *= -1;
        }

        if (pCollider.tag == "Player")
        {
            pCollider.GetComponent<Player>().takeDamage(attackDamange);
        }

        if (pCollider.tag == "Projectile")
        {
            takeDamage(pCollider.GetComponent<Shot>().Damage);
        }
    }

}
