using UnityEngine;
using System.Collections;

public class JellyfishAnimation : MonoBehaviour 
{
	private const float X_MIN = -3.0f;
	private const float X_MAX = 3.0f;
	private const float Y_MIN = -3.0f;
	private const float Y_MAX = 3.0f;
	private const float CONTRACT_TIME = 2.0f;

	private float lerp;

	[SerializeField] private bool contracting = false;

	private void Update()
	{
		Pulsate();
	}

	private void Pulsate()
	{
		lerp += Time.deltaTime;

		float xTarget = contracting ? X_MIN : X_MAX;
		float xThisFrame = Mathf.Lerp(this.transform.localScale.x, xTarget, (lerp / CONTRACT_TIME));

		float yTarget = contracting ? Y_MIN : Y_MAX;
		float yThisFrame = Mathf.Lerp(this.transform.localScale.y, yTarget, (lerp / CONTRACT_TIME));

		this.transform.localScale = new Vector3(xThisFrame, yThisFrame, this.transform.localScale.z);

		if(lerp > CONTRACT_TIME) {
			lerp = 0.0f;
			contracting = !contracting;
		}
	}
}
