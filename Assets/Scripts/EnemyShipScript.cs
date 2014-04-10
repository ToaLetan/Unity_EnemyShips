using UnityEngine;
using System.Collections;

public class EnemyShipScript : MonoBehaviour 
{
	public enum AI_State { Idle, Aggressive, Defensive, Evasive, Supportive };

	public float MAX_ACTION_TIME = 1.0f;

	private AI_State currentAIState = AI_State.Aggressive;

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
		ProcessState ();
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

	#region AI State Functions
	private void ProcessState()
	{
		switch (currentAIState) 
		{
			case AI_State.Idle:
				IdleAction();
			break;
			case AI_State.Aggressive:
				AggressiveAction();
			break;
			case AI_State.Defensive:
				DefensiveAction();
			break;
			case AI_State.Evasive:
				EvasiveAction();
			break;
			case AI_State.Supportive:
				SupportiveAction();
			break;
			default:
				IdleAction();
			break;
		}
	}

	private void IdleAction()
	{

	}

	private void AggressiveAction()
	{
		//Circle around player and shoot at them
		if (isActionTimerRunning)
			UpdateActionTimer ();

		float angleToTarget = Mathf.Atan2(currentTarget.transform.position.y - gameObject.transform.position.y, currentTarget.transform.position.x - gameObject.transform.position.x) 
			* Mathf.Rad2Deg + 90;

		gameObject.transform.rotation = Quaternion.Euler (0, 0, angleToTarget);

		Vector3 newPos = gameObject.transform.position + (gameObject.transform.right * 1.0f * Time.deltaTime);

		gameObject.transform.position = newPos;
	}

	private void DefensiveAction()
	{
		
	}

	private void EvasiveAction()
	{
		
	}

	private void SupportiveAction()
	{
		
	}

	#endregion
}
