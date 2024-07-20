using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Created by Alexander Anokhin

public class GameController : MonoBehaviour {

    // CONSTANTS
    public const int MAP_WIDTH = 10;
    public const int MAP_HEIGHT = 10;

    // CONFIG
    public GameObject plainsTilePrefab;
    public GameObject mountainTilePrefab;
    public GameObject forestTilePrefab;
    public GameObject oceanTilePrefab;
    public UIController UIController;

    // CACHE
    public static GameController primary;
    [HideInInspector] public Camera mainCamera;

    // DATA
    private int turn = 1;
    private GameObject[][] tiles; // x (col), y (row)
    private int xLength;
    private int yLength;
    private GameObject selectedTile;

    // singleton
    public void Awake() {
        int count = FindObjectsOfType<GameController>().Length;

        if (count > 1) {
            gameObject.SetActive(false);
            Destroy(gameObject);
        } else {
            DontDestroyOnLoad(gameObject);
            primary = this;
        }
    }

    public void Start() {
        mainCamera = Camera.main;

        PopulateTiles();
    }

    public void Update() {
        ClearSelectedTile();
    }

    public GameObject[] GetTileNeighbors(int x, int y) {

        GameObject[] output = new GameObject[6];

        // checks to avoid IndexOutOfBounds

        if (x > 0) {
            output[0] = tiles[x - 1][y]; // left

            if (y > 0) {
                output[1] = tiles[x - 1][y - 1]; // bottom
            }
        }

        if (x + 1 < xLength) {
            output[2] = tiles[x + 1][y]; // right

            if (y + 1 < yLength) {
                output[3] = tiles[x + 1][y + 1]; // top right
            }
        }

        if (y > 0) {
            output[4] = tiles[x][y - 1]; // bottom right
        }

        if (y + 1 < yLength) {
            output[5] = tiles[x][y + 1]; // top left
        }

        return output;
    }

    private void PopulateTiles() {

        xLength = MAP_WIDTH;
        yLength = MAP_HEIGHT;

        // add margin
        xLength += (MathUtil.roundUpToEven(MAP_HEIGHT) / 2) - 1;

        // init tiles in form x, y
        tiles = new GameObject[xLength][];

        for (int x = 0; x < xLength; x++) {
            tiles[x] = new GameObject[MAP_HEIGHT];
        }

        // generate tiles in form y, x since its easier to visualize
        // tiles data struct already init in x, y, so no problems

        Vector3 tilePos = Vector3.zero;
        int xOffset = 0;

        for (int y = 0; y < yLength; y++) {
            
            xOffset = (MathUtil.roundUpToEven(y + 1) / 2) - 1;

            for (int x = xOffset; x < MAP_WIDTH + xOffset; x++) {

                // grid positions
                tilePos.x = 26 * x;
                tilePos.y = 23 * y;

                // actual hex offset
                tilePos.x -= 13 * y;

                tiles[x][y] = GenerateTile(x, y, tilePos);
            }
        }
    }

    public void SetSelectedTile(GameObject tile) {
        if (selectedTile != null) {
            selectedTile.GetComponent<Tile>().OnDeselected();
        }

        selectedTile = tile;
        selectedTile.GetComponent<Tile>().OnSelected();

        UIController.EnableBuildPopup(selectedTile.transform);
    }

    private void ClearSelectedTile() {
        if (Input.GetMouseButtonDown(1)) {
            if (selectedTile != null) {
                selectedTile.GetComponent<Tile>().OnDeselected();
            }
            selectedTile = null;
            UIController.DisableBuildPopup();
            //mainUIController.ClearInfoBox();
        }
    }

    private GameObject GenerateTile(int c, int r, Vector3 tilePos) {

        GameObject prefab = oceanTilePrefab;
        float rand = Random.value;

        if (rand >= 0.9) {
            prefab = mountainTilePrefab;
        } else if (rand >= 0.6) {
            prefab = forestTilePrefab;
        } else {
            prefab = plainsTilePrefab;
        }

        GameObject obj = Instantiate(prefab, tilePos, Quaternion.identity);
        obj.GetComponent<Tile>().Init(c, r);

        return obj;
    }

    public void DeleteTile(int c, int r) {
        tiles[c][r] = null;
    }
}
