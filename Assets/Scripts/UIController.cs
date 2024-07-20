using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Created by Alexander Anokhin

public class UIController : MonoBehaviour {

    public GameObject buildPopup;
    public int buildPopupOffset = 45;

    private void Start() {
        buildPopup.SetActive(false);
    }

    public void EnableBuildPopup(Transform tileTransform) {
        buildPopup.transform.position = new Vector3(tileTransform.position.x, tileTransform.position.y + buildPopupOffset, tileTransform.position.z);
        buildPopup.SetActive(true);
    }

    public void DisableBuildPopup() {
        buildPopup.SetActive(false);
    }
}
