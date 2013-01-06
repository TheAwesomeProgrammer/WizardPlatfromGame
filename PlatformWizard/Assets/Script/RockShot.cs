using UnityEngine;
using System.Collections;

public class RockShot : Shot
{

    public float Gravity;

	// Use this for initialization
	public override void Start () {
	base.Start();
	}
	
	// Update is called once per frame
    public override void Update()
    {
	base.Update();
   // transform.Translate(0, 1 * Gravity * Time.deltaTime, 0);
	}
}
