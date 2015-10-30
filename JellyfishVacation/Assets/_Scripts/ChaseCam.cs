using UnityEngine;
using System.Collections;

public class ChaseCam : MonoBehaviour 
{
	private const float POS_LERP_SPEED = 0.1f;
	private const float ROT_LERP_SPEED = 0.1f;
	private const bool CHASE_ROTATION = true;

	[SerializeField] private GameObject chaseTarget;

	private Vector3 chaseDistance;
	private Vector3 rotOffset;

	private bool chasing = false;


	public void StartChase ()
	{
		chasing = true;
		chaseDistance = this.transform.position - chaseTarget.transform.position;
		rotOffset = this.transform.eulerAngles - chaseTarget.transform.eulerAngles;
	}

	private void Start()
	{
		if(chaseTarget != null) {
			StartChase();
		}
	}


	private void Update()
	{
		if(chasing == false) {
			return;
		}

		if(chaseTarget == null) 
		{
			chasing = false;
			return;
		}

		this.transform.position = Vector3.Lerp(this.transform.position, (chaseTarget.transform.position + chaseDistance), POS_LERP_SPEED);
		this.transform.eulerAngles = Vector3.Lerp(this.transform.eulerAngles, (chaseTarget.transform.eulerAngles + rotOffset), ROT_LERP_SPEED);
	}

}
