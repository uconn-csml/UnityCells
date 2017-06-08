using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityEditor;

public class DivisionState : ICellState
{
    private readonly Cell self;
    private Vector3 pos;
    private GameObject cell;

    public DivisionState(Cell statePatternCell)
    {
        self = statePatternCell;
    }

    public void Update()
    {
        Division();
    }

    public void OnTriggerStay(Collider other)
    {
        
    }

    public void ToDivisionState()
    {
        Debug.Log("Can't transition to same state");
    }

    public void ToGeneralBehaveState()
    {
        self.currentState = self.generalBehaveState;
    }

    void Division()
    {

        //pos = Random.onUnitSphere * 2;

        //Object prefab = AssetDatabase.LoadAssetAtPath("Assets/Resources/cell.prefab", typeof(GameObject));

        //cell = Object.Instantiate(prefab, new Vector3(self.transform.position.x + pos.x, self.transform.position.y + pos.y, 
        //    self.transform.position.z + pos.z), Quaternion.identity) as GameObject;
        //GlobalVariables.cellNumerator = GlobalVariables.cellNumerator + 1;
        //cell.name = "cell" + GlobalVariables.cellNumerator;
        //self.birthTime = Time.time;
        //ToGeneralBehaveState();
    }
}
