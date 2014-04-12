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

	private EnemyMovementLibrary.Direction currentMoveDirection;

	// Use this for initialization
	void Start () 
	{
		potentialTargets = GameObject.FindGameObjectsWithTag ("Player");
		currentTarget = potentialTargets[Random.Range (0, potentialTargets.Length)];

		isActionTimerRunning = true;

		if (Random.value > 0.5f)
			currentMoveDirection = EnemyMovementLibrary.Direction.Left;
		else
			currentMoveDirection = EnemyMovementLibrary.Direction.Right;
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
		//Move towards the player, circle around them when in range while constantly shooting.
		if (isActionTimerRunning)
			UpdateActionTimer ();

		if(EnemyMovementLibrary.DistanceBetweenPoints(gameObject.transform.position, currentTarget.transform.position) >= 0.5f)
			gameObject.transform.position = EnemyMovementLibrary.MoveToTarget (gameObject, currentTarget);
		else
			gameObject.transform.position = EnemyMovementLibrary.CircleAroundTarget(gameObject, currentTarget, currentMoveDirection);
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
