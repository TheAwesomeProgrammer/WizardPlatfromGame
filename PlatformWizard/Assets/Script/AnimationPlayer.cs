using UnityEngine;
using System.Collections;

public enum PlayerAnimationEnum
{
    leftRotation,
    rightRotation,
    noAnimation
}


public class AnimationPlayer : MonoBehaviour
{

    public Material normalAnimation;

    public Material RightRotation;
    public Material LeftRotation;

    public Material red;


    public float DurationOfRotating;
    

    public bool mIsAnimating { get; set; }


    private PlayerAnimationEnum m_animation = PlayerAnimationEnum.noAnimation;

    private float whenToStopAnimating = float.MaxValue;

    private int randomNumber;
    


	// Use this for initialization
	void Start ()
	{
        
	   
	}
	
	// Update is called once per frame
	void Update ()
	{
        if(whenToStopAnimating < Time.time)
        {
            mIsAnimating = false;
            whenToStopAnimating = float.MaxValue;
            renderer.material = normalAnimation;
         /*   if (m_animation == PlayerAnimationEnum.leftRotation)
            {
                renderer.material = LeftRotation;
            }
            else if (m_animation == PlayerAnimationEnum.rightRotation)
            {
                renderer.material = RightRotation;
            } */
        }
      
    
	   
	}

    public void HitAnimation(float pTimeToBeHitIn)
    {
        whenToStopAnimating = pTimeToBeHitIn + Time.time;
        renderer.material = red;
    }

    void AnimatePlayerRotation()
    {
        if (m_animation == PlayerAnimationEnum.leftRotation)
        {
            m_animation = PlayerAnimationEnum.rightRotation;
            renderer.material = RightRotation;
        }
        else if (m_animation == PlayerAnimationEnum.rightRotation)
        {
            m_animation = PlayerAnimationEnum.leftRotation;
            renderer.material = LeftRotation;
        }    
    }
}
