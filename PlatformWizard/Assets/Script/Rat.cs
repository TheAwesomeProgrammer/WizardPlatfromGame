using UnityEngine;
using System.Collections;

public enum MaterialsRat
{
    Walking1,
    Walking2,
    Walking3,
    Walking4,

}

public class Rat : Enemy
{
    

    public float TimeBetweenFrames = .1f;

    public Material Walking1Left;
    public Material Walking2Left;
    public Material Walking3Left;
    public Material Walking4Left;

    public Material Walking1Right;
    public Material Walking2Right;
    public Material Walking3Right;
    public Material Walking4Right;

    
    private float mAnimateTime = float.MinValue;
  

    private int mDirection = 1;

    private MaterialsRat mMaterials;

	// Use this for initialization
	void Start ()
	{
        life = MAXLIFE;
	   mMaterials = MaterialsRat.Walking1;
	}
	
	// Update is called once per frame
	void Update ()
	{
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
	 isDead();
	Animate();
    Move();
	}

    void Move()
    {
        if(mDirection == 1)
        {
            transform.Translate(Vector3.left * speed * Time.deltaTime);
        }

        if(mDirection == -1)
        {
            transform.Translate(Vector3.right * speed * Time.deltaTime);
        }
        }

    public override void isDead()
    {
        base.isDead();
    }

    void Animate()
    {
        
        if(mAnimateTime < Time.time)
        {
       
            mAnimateTime = TimeBetweenFrames + Time.time;
            
            if (mMaterials == MaterialsRat.Walking1)
            {
              mMaterials = MaterialsRat.Walking2;
              if (mDirection == 1)
                  renderer.material = Walking2Left;
              else
                  renderer.material = Walking2Right;
            }

            else if (mMaterials == MaterialsRat.Walking2)
            {
                mMaterials = MaterialsRat.Walking3;
                    if(mDirection == 1)
              renderer.material = Walking3Left;
                else
                    renderer.material = Walking3Right;
            }

            else if (mMaterials == MaterialsRat.Walking3)
            {
                mMaterials = MaterialsRat.Walking4;
                     if(mDirection == 1)
              renderer.material = Walking4Left;
                else
                     renderer.material = Walking4Right;
            }

            else if (mMaterials == MaterialsRat.Walking4)
            {
                mMaterials = MaterialsRat.Walking1;
                    if(mDirection == 1)
              renderer.material = Walking1Left;
                else
                     renderer.material = Walking1Right;
            }
          
        }
    }

    void OnTriggerEnter(Collider pCollider)
    {
  
        if(pCollider.tag == "Cube")
        {
            mDirection *= -1;
        }

        if (pCollider.tag == "Player")
        {
            pCollider.GetComponent<Player>().takeDamage(attackDamange);
        }

        if (pCollider.tag == "Projectile")
        {
            takeDamage(pCollider.GetComponent<Projectile>().Damage);
        }
    }
}
