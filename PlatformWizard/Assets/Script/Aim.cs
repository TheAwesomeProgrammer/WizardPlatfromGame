using UnityEngine;
using System.Collections;



public class Aim : MonoBehaviour
{
    public GameObject Magic;
    public GameObject Fireball;
    public GameObject Boulder;
    public GameObject MeatMissile;

    private GameObject mCrossHair;



	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update ()
	{
	    mCrossHair = GameObject.FindGameObjectWithTag("CrossHair");

	
         Vector3 relativePos = mCrossHair.transform.position - transform.position;
        Quaternion rotation = Quaternion.LookRotation(relativePos);

	    transform.rotation = rotation;

	}
        

    public void Shot(ShotType pShotType)
    {
       
      if(pShotType == ShotType.Magic)
      {
          Instantiate(Magic, transform.position, transform.rotation);
      }
      if (pShotType == ShotType.Fireball)
      {
          Instantiate(Fireball, transform.position, transform.rotation);
      }
      if (pShotType == ShotType.Boulder)
      {
          Instantiate(Boulder, transform.position, transform.rotation);
      }
      if (pShotType == ShotType.Meatmissile)
      {
          Instantiate(MeatMissile, transform.position, transform.rotation);
          Instantiate(MeatMissile, transform.position, new Quaternion(transform.rotation.x,transform.rotation.y,transform.rotation.z+ 0.1f,transform.rotation.w));
          Instantiate(MeatMissile, transform.position, new Quaternion(transform.rotation.x, transform.rotation.y, transform.rotation.z - 0.1f, transform.rotation.w));
      }
     
    
    }
}
