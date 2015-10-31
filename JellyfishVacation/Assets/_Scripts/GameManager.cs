using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour 
{
	private static GameManager instance;
	public static GameManager Instance { get { return instance; } }

	[SerializeField] private Texture2D[] levels;

	[SerializeField] private MapGenerator mapGen;
	[SerializeField] private GelatinousCube gCube;
	[SerializeField] private ChaseCam chaseCam;

	private void Awake()
	{
		instance = this;
	}

	private void Start()
	{
		chaseCam.StartChase(gCube.gameObject);
		mapGen.DrawNewMap(levels[0]);
		gCube.StartMovement(mapGen);
	}
}
