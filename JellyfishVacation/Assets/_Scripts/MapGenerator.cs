using UnityEngine;
using System.Collections;

public class MapGenerator : MonoBehaviour 
{
	private const float TILE_DIMENSIONS = 1.0f;

	public enum TileType
	{
		START_POS,
		EMPTY,
		WALL
	}

	[System.Serializable]
	public class Tile
	{
		public TileType type;
		public Color color;
		public GameObject[] prefabs;
	}

	[SerializeField] Tile[] styles;

	[SerializeField] private Texture2D sampleMap;
	[SerializeField] private bool debug;

	[SerializeField] Transform maze;

	private Color[,] currentMapPixels;
	private TileType[,] tiles;

	private void Awake()
	{
		if(debug == true) {
			GenerateSampleMap();
		}
	}

	private void GenerateSampleMap()
	{
		GenerateMapArray(sampleMap);
		DrawMap();
	}

	private void GenerateMapArray(Texture2D map)
	{
		Color[] fullPixels = map.GetPixels();

		currentMapPixels = ConvertColorArrayToMulti(fullPixels, map.height, map.width);
	}

	private void DrawMap()
	{
		Vector3 startPosition = GetMapStartPosition();

		GameObject mazeHolder = new GameObject("Maze Holder");

		for (int i = 0; i < currentMapPixels.GetLength(0); i++) {
			for (int j = 0; j < currentMapPixels.GetLength(1); j++) {
				Vector3 tilePosition = new Vector3(startPosition.x + (i * TILE_DIMENSIONS), 
				                                   0.0f, 
				                                   startPosition.z + (j * TILE_DIMENSIONS));

				Color currentColor = currentMapPixels[i, j];
				Tile tile = GetTileByColor(currentColor);
				GameObject.Instantiate(tile.prefabs[Random.Range(0, tile.prefabs.Length)], tilePosition, Quaternion.identity);
			}
		}
	}

	private Tile GetTileByColor(Color color)
	{
		foreach(Tile t in styles) {
			if(t.color == color) {
				return t;
			}
		}

		Debug.LogError("Unknown Color in Map Array!");
		return null;
	}

	private Vector3 GetMapStartPosition()
	{
		float xStartPos = maze.position.x - ((float) currentMapPixels.GetLength(1) * TILE_DIMENSIONS) / 2.0f;
		float zStartPos = maze.position.z - ((float) currentMapPixels.GetLength(0) * TILE_DIMENSIONS) / 2.0f;
		return new Vector3(xStartPos, 0.0f, zStartPos);
	}

	private Color[,] ConvertColorArrayToMulti(Color[] sourcePixels, int imageHeight, int imageWidth)
	{
		Color[,] multiPixelArray = new Color[imageWidth, imageHeight];
		for (int i = 0; i < imageHeight; i++) {
			for (int j = 0; j < imageWidth; j++) {
				int currentPix = (i * imageWidth) + j;
				multiPixelArray[i, j] = sourcePixels[currentPix];
			}
		}

		return multiPixelArray;
	}

}
