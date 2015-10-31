using UnityEngine;
using System.Collections;

public class GelatinousCubeMotor : MonoBehaviour 
{
	public enum Direction
	{
		UP,
		DOWN,
		LEFT,
		RIGHT
	}

	[System.Serializable]
	private class ValidDirection
	{
		public Direction dir;
		public string inputVal;
		public Vector2 vectorDir;
	}

	[SerializeField] private ValidDirection[] validDirections;


	private const string HORIZONTAL_AXIS = "Horizontal";
	private const string VERTICAL_AXIS = "Vertical";

	//This is a multiplier based on one TILE taking one second to traverse.
	private const float BASE_SPEED = 0.5f;

	private Vector2 moveDir = Vector2.zero;
	[SerializeField] private IntVector2 currentTile;
	[SerializeField] private IntVector2 targetTile;

	private bool running;
	[SerializeField] private float moveProgress;

	public bool IsMoving { get { return (moveProgress < 1.0f); } }

	MapGenerator mapGen;

	public void StartMovement(MapGenerator mapGen, Direction dir)
	{
		this.mapGen = mapGen;

		GameObject spawnTile = mapGen.Tiles[mapGen.SpawnPoint.x, mapGen.SpawnPoint.y].tileGO;
		this.transform.position = spawnTile.transform.position;
		currentTile = new IntVector2(mapGen.SpawnPoint.x, mapGen.SpawnPoint.y);
		MoveInDirection(dir);
		running = true;
	}

	private void Update()
	{
		if(running == true) {
			if(moveProgress < 1.0f) {
				ProcessMove();
			}

			ProcessInput();
		}
	}

	private void ProcessMove()
	{
		moveProgress += Time.deltaTime * BASE_SPEED;
		if(moveProgress >= 1.0f) {
			moveProgress = 1.0f;
			MoveComplete();
		}

		Vector3 startPos = mapGen.Tiles[currentTile.x, currentTile.y].tileGO.transform.position;
		Vector3 endPos = mapGen.Tiles[targetTile.x, targetTile.y].tileGO.transform.position;

		this.transform.position = Vector3.Lerp(startPos, endPos, moveProgress);
	}

	private bool ProcessInput()
	{
		foreach(ValidDirection inputDir in validDirections) {
			if(Input.GetButton(inputDir.inputVal)) {
				//If we're already moving this way, just ignore.
				if(moveDir == inputDir.vectorDir) {
					continue;
				}

				if(CheckIfValidDirection(inputDir.vectorDir) == true) {
					MoveInDirection(inputDir.vectorDir);
					return true;
				}
			}
		}

		return false;
	}
	


	private void MoveInDirection(Vector2 newDirection)
	{
		moveDir = newDirection;
		if(moveProgress >= 1.0f)
		{
			moveProgress = 0.0f;
		}
		targetTile = new IntVector2(currentTile.x + Mathf.RoundToInt(newDirection.x), currentTile.y + Mathf.RoundToInt(newDirection.y));
	}

	private void MoveComplete()
	{
		currentTile = new IntVector2(targetTile.x, targetTile.y);

		if(ProcessInput() == true) {
			return;
		}

		if(CheckIfValidDirection(moveDir)) {
			MoveInDirection(moveDir);
		}
	}

	private bool CheckIfValidDirection(Vector2 newDirection)
	{
		IntVector2 checkTile = new IntVector2(currentTile.x + (int) newDirection.x, currentTile.y + (int) newDirection.y);
		switch(mapGen.Tiles[checkTile.x, checkTile.y].type) {
		case MapGenerator.TileType.WALL:
			return false;
		}
		
		return true;
	}

	private void MoveInDirection(Direction dir)
	{
		foreach(ValidDirection vd in validDirections) {
			if(vd.dir == dir) {
				MoveInDirection(vd.vectorDir);
				return;
			}
		}
		
		Debug.LogWarning("Invalid Direction Passed. Setup Directions.");
	}

//	private void ProcessInput()
//	{
//		if(Input.GetAxis(HORIZONTAL_AXIS) != 0)
//		{
//			//float xMovementThisFrame = ((Input.GetAxis(HORIZONTAL_AXIS) * BASE_SPEED) * Time.deltaTime);
//			float xMovementThisFrame = ((Input.GetAxis(HORIZONTAL_AXIS) * BASE_SPEED));
//			this.gameObject.GetComponent<Rigidbody>().AddForce(Vector3.right * xMovementThisFrame);
//		}
//
//		if(Input.GetAxis(VERTICAL_AXIS) != 0)
//		{
//			//float yMovementThisFrame = ((Input.GetAxis(VERTICAL_AXIS) * BASE_SPEED) * Time.deltaTime);
//			float yMovementThisFrame = ((Input.GetAxis(VERTICAL_AXIS) * BASE_SPEED));
//			this.gameObject.GetComponent<Rigidbody>().AddForce(Vector3.forward * yMovementThisFrame);
//		}
//	}
}
