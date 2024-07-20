using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Created by Alexander Anokhin

public class Tile : MonoBehaviour {

    public string info = "template";

    private int myColumn;
    private int myRow;

    private SpriteRenderer mySpriteRenderer;

    public void Init(int column, int row) {
        mySpriteRenderer = GetComponent<SpriteRenderer>();

        myColumn = column;
        myRow = row;
        mySpriteRenderer.sortingOrder = row * -1;
    }

    public void OnSelected() {
        mySpriteRenderer.color = Color.gray;
        mySpriteRenderer.sortingOrder = 1;
    }

    public void OnDeselected() {
        mySpriteRenderer.color = Color.white;
        mySpriteRenderer.sortingOrder = myRow * -1;
    }

    public string GetInfo() {
        return info;
    }

    public void OnMouseDown() {
        /*if (GameController.primary.mainEventSystem.IsPointerOverGameObject()) {
            return;
        }*/

        GameController.primary.SetSelectedTile(gameObject);
    }
}
