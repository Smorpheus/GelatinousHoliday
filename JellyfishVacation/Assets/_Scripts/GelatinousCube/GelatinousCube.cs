using UnityEngine;
using System.Collections;

public class GelatinousCube : MonoBehaviour 
{
	[SerializeField] private GelatinousCubeMotor motor;
	public GelatinousCubeMotor Motor { get { return motor; } }

	private MapGenerator mapGen;

	public void StartMovement(MapGenerator mapGen)
	{
		this.mapGen = mapGen;
		motor.StartMovement(mapGen, GelatinousCubeMotor.Direction.LEFT);
	}
}
