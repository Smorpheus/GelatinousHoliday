using UnityEngine;
using System.Collections;

public class GelatinousCubeMotor : MonoBehaviour 
{
	private const string HORIZONTAL_AXIS = "Horizontal";
	private const string VERTICAL_AXIS = "Vertical";
	private const float BASE_SPEED = 2.0f;

	private void Update()
	{
		ProcessInput();
	}

	private void ProcessInput()
	{
		if(Input.GetAxis(HORIZONTAL_AXIS) != 0)
		{
			//float xMovementThisFrame = ((Input.GetAxis(HORIZONTAL_AXIS) * BASE_SPEED) * Time.deltaTime);
			float xMovementThisFrame = ((Input.GetAxis(HORIZONTAL_AXIS) * BASE_SPEED));
			this.gameObject.GetComponent<Rigidbody>().AddForce(Vector3.right * xMovementThisFrame);
		}

		if(Input.GetAxis(VERTICAL_AXIS) != 0)
		{
			//float yMovementThisFrame = ((Input.GetAxis(VERTICAL_AXIS) * BASE_SPEED) * Time.deltaTime);
			float yMovementThisFrame = ((Input.GetAxis(VERTICAL_AXIS) * BASE_SPEED));
			this.gameObject.GetComponent<Rigidbody>().AddForce(Vector3.forward * yMovementThisFrame);
		}
	}
}
