using UnityEngine;
using System.Collections;

public class SimpleGelatinousAnimation : MonoBehaviour 
{

	private const float X_MIN = 0.9f;
	private const float X_MAX = 1.0f;
	private const float Y_MIN = 0.9f;
	private const float Y_MAX = 1.0f;
	private const float Z_MIN = 0.9f;
	private const float Z_MAX = 1.0f;
	private const float RANDOM_VARIATION = 0.1f;
	private const float CONTRACT_TIME = 1.0f;
	
	private float lerp;
	private float timeThisContraction = 0.0f;
	private bool contracting = false;

	[SerializeField] private GelatinousCubeMotor motor;

	private void Update()
	{
		if(motor.IsMoving == true) {
			Pulsate();
		}
	}
	
	private void Pulsate()
	{
		lerp += Time.deltaTime;
		
		float xTarget = contracting ? X_MIN : X_MAX;
		float xThisFrame = Mathf.Lerp(this.transform.localScale.x, xTarget, (lerp / timeThisContraction));
		
		float yTarget = contracting ? Y_MIN : Y_MAX;
		float yThisFrame = Mathf.Lerp(this.transform.localScale.y, yTarget, (lerp / timeThisContraction));

		float zTarget = contracting ? Z_MIN : Z_MAX;
		float zThisFrame = Mathf.Lerp(this.transform.localScale.y, yTarget, (lerp / timeThisContraction));

		this.transform.localScale = new Vector3(xThisFrame, yThisFrame, zThisFrame);
		
		if(lerp > timeThisContraction) {
			lerp = 0.0f;
			timeThisContraction = Random.Range(CONTRACT_TIME - RANDOM_VARIATION, CONTRACT_TIME + RANDOM_VARIATION);
			contracting = !contracting;
		}
	}

}
