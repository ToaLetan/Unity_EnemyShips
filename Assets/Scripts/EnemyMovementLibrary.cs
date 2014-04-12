using UnityEngine;
using System.Collections;

public class EnemyMovementLibrary //Used to handle enemy movement functions
{
	public enum Direction
	{  
		Left = -1, Right = 1
	};

	private static Vector3 Move(Vector3 direction, float velocity)
	{
		return direction * velocity * Time.deltaTime;
	}

	public static float DistanceBetweenPoints(Vector2 point1, Vector2 point2)
	{
		return Mathf.Sqrt ( Mathf.Pow(point2.x - point1.x, 2) +  Mathf.Pow(point2.y - point1.y, 2));
	}

	public static Vector3 MoveToTarget(GameObject owner, GameObject target)
	{
		owner.transform.rotation = RotateToTarget (owner, target);

		return owner.transform.position + Move(-owner.transform.up, 1.0f);
	}

	public static Quaternion RotateToTarget(GameObject owner, GameObject target)
	{
		float angleToTarget = Mathf.Atan2(target.transform.position.y - owner.transform.position.y, target.transform.position.x - owner.transform.position.x) 
			* Mathf.Rad2Deg + 90;
		
		return Quaternion.Euler (0, 0, angleToTarget);
	}

	public static Vector3 CircleAroundTarget(GameObject owner, GameObject target, Direction moveDirection)
	{
		owner.transform.rotation = RotateToTarget (owner, target);

		//TEMP MOVESPEED
		return owner.transform.position + Move(owner.transform.right, 1.0f);
	}
}
