using UnityEngine;
using System.Collections;

[System.Serializable]
public struct IntVector2
{
	public IntVector2(int x, int y) {
		this.x = x;
		this.y = y;
	}

	public int x;
	public int y;
}

public class MapGenerator : MonoBehaviour 
{
	private static MapGenerator instance;
	public static MapGenerator Instance { get { return instance; } }

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

	public class InstantiatedTile
	{
		public InstantiatedTile(TileType type, GameObject tileGO) {
			this.type = type;
			this.tileGO = tileGO;
		}

		public TileType type;
		public GameObject tileGO;
	}

	[SerializeField] Tile[] styles;

	[SerializeField] private Texture2D sampleMap;
	[SerializeField] private bool debug;

	[SerializeField] Transform maze;

	private Color[,] currentMapPixels;
	private InstantiatedTile[,] tiles;
	public InstantiatedTile[,] Tiles { get { return tiles; } }

	[SerializeField] private IntVector2 spawnPoint;
	public IntVector2 SpawnPoint { get { return spawnPoint; } }

	private void Awake()
	{
		instance = this;

		if(debug == true) {
			GenerateSampleMap();
		}
	}

	private void GenerateSampleMap()
	{
		DrawNewMap(sampleMap);
	}

	public void DrawNewMap(Texture2D map)
	{
		Color[] fullPixels = map.GetPixels();

		currentMapPixels = ConvertColorArrayToMulti(fullPixels, map.height, map.width);
		DrawMap();
	}

	private void DrawMap()
	{
		Vector3 startPosition = GetMapStartPosition();

		GameObject mazeHolder = new GameObject("Maze Holder");

		tiles = new InstantiatedTile[currentMapPixels.GetLength(0), currentMapPixels.GetLength(1)];

		for (int i = 0; i < currentMapPixels.GetLength(0); i++) {
			for (int j = 0; j < currentMapPixels.GetLength(1); j++) {
				Vector3 tilePosition = new Vector3(startPosition.x + (i * TILE_DIMENSIONS), 
				                                   0.0f, 
				                                   startPosition.z + (j * TILE_DIMENSIONS));
				Color currentColor = currentMapPixels[i, j];
				Tile tile = GetTileByColor(currentColor);

				GameObject newTileGO = (GameObject) GameObject.Instantiate(tile.prefabs[Random.Range(0, tile.prefabs.Length)], 
				                                            tilePosition, 
				                                            Quaternion.identity);
				newTileGO.transform.parent = mazeHolder.transform;
				tiles[i, j] = new InstantiatedTile(tile.type, newTileGO);

				if(tile.type == TileType.START_POS) {
					spawnPoint = new IntVector2(i, j);
				}
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
