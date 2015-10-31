using UnityEngine;
using System.Collections;

public class ChaseCam : MonoBehaviour 
{
	private const float POS_LERP_SPEED = 0.1f;
	private const float ROT_LERP_SPEED = 0.1f;
	private const bool CHASE_ROTATION = true;

	[SerializeField] private GameObject chaseTarget;

	[SerializeField] private Vector3 chaseDistance;
	[SerializeField] private Vector3 rotOffset;

	private bool chasing = false;

	private void StartChase()
	{
		StartChase(chaseTarget);
	}

	public void StartChase (GameObject target)
	{
		chasing = true;
		chaseTarget = target;
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

		if(chaseTarget == null) {
			chasing = false;
			return;
		}

		this.transform.position = Vector3.Lerp(this.transform.position, (chaseTarget.transform.position + chaseDistance), POS_LERP_SPEED);
		if(CHASE_ROTATION == true) {
			this.transform.eulerAngles = Vector3.Lerp(this.transform.eulerAngles, (chaseTarget.transform.eulerAngles + rotOffset), ROT_LERP_SPEED);
		}
	}

}
