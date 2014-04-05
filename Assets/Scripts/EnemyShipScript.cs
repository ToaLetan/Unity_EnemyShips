using UnityEngine;
using System.Collections;

public class EnemyShipScript : MonoBehaviour 
{
	public float MAX_ACTION_TIME = 1.0f;

	private GameObject[] potentialTargets;
	private GameObject currentTarget = null;

	private float actionTime = 0;
	private bool isActionTimerRunning = false;

	// Use this for initialization
	void Start () 
	{
		potentialTargets = GameObject.FindGameObjectsWithTag ("Player");
		currentTarget = potentialTargets[Random.Range (0, potentialTargets.Length)];

		isActionTimerRunning = true;
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (isActionTimerRunning)
			UpdateActionTimer ();
	}

	private void UpdateActionTimer()
	{
		actionTime += Time.deltaTime;
		if (actionTime >= MAX_ACTION_TIME) 
		{
			Shoot ();
			actionTime = 0;
		}
	}

	private void Shoot()
	{
		GameObject projectile = GameObject.Instantiate (Resources.Load ("Prefabs/Projectile")) as GameObject;
		projectile.transform.position = gameObject.transform.position;

		projectile.GetComponent<ProjectileScript> ().PushTowardsLocation (currentTarget.transform.position, 5.0f);
	}
}
