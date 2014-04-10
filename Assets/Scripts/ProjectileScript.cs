using UnityEngine;
using System.Collections;

public class ProjectileScript : MonoBehaviour 
{
	public float ProjectileLifetime = 1.5f; //Object lifetime in seconds.
	
	private float velocity = 0.0f;
	private float currentLifetime = 0.0f;

	private bool isActive = false;

	// Use this for initialization
	void Start () 
	{
	
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (isActive) 
		{
			Vector3 newPos = gameObject.transform.position + (-gameObject.transform.up * velocity * Time.deltaTime);
			gameObject.transform.position = newPos;

			UpdateTimer();
		}
	}

	public void PushTowardsLocation(Vector3 targetPos, float vel)
	{
		velocity = vel;
		isActive = true;

		float rotation = Mathf.Atan2 (targetPos.y - gameObject.transform.position.y, targetPos.x - gameObject.transform.position.x) * Mathf.Rad2Deg;

		gameObject.transform.Rotate (0, 0, rotation + 90); //90 has to be added due to 2D orientation.
	}

	private void UpdateTimer()
	{
		currentLifetime += Time.deltaTime;

		if (currentLifetime >= ProjectileLifetime) 
		{
			isActive = false;
			currentLifetime = 0;
			Destroy(gameObject);
		}
	}
}
