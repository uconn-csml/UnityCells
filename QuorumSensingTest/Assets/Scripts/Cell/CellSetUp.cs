using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CellSetUp : MonoBehaviour {

    public GameObject cellAgent;
    private GameObject cell;

    // Use this for initialization
    void Awake() {
        cell = Instantiate(cellAgent, new Vector3(5, 5, 5 ), Quaternion.identity);
        cell.name = "cell";

    }
}
