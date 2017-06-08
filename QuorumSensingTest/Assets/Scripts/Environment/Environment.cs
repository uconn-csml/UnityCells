using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Environment : MonoBehaviour
{
    public float cellSecretedInducer = 0.1f;
    public float inducerUpdate;
    public float newInducer;
    public float oldInducer;
    public double newIL2;
    public double oldIL2;
    public double iL2Update;
    public double recIL2;
    public int iteration;
    


    [HideInInspector] public IEnvironmentState currentState;
    [HideInInspector] public DiffusionState diffusionState;
    [HideInInspector] public CellEffectedEnvState cellEffectedEnvState;

    private void Awake()
    {
        diffusionState = new DiffusionState(this);
        cellEffectedEnvState = new CellEffectedEnvState(this);
    }

    void Start()
    {
        iteration = -1;
        GlobalVariables.envIL2Sum = new double[100000];
        GlobalVariables.envIL2Max = new double[100000];
        currentState = diffusionState;
    }

    void FixedUpdate()
    {
        iteration++;
        currentState.Update();
        Time.fixedDeltaTime = 1f;
    }

    private void OnTriggerStay(Collider other)
    {
        currentState.OnTriggerStay(other);
    }
}
