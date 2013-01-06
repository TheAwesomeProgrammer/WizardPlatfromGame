using UnityEngine;
using System.Collections;

public class Shot : MonoBehaviour
{
    public float LifeTime = 3;
    public float Speed = 15;
    public float Damage = 20;

	// Use this for initialization
	public virtual void Start () {
        Destroy(gameObject, LifeTime);
	}
	
	// Update is called once per frame
	public virtual void Update () {
        transform.Translate(Vector3.forward * Speed * Time.deltaTime);
	}
}
