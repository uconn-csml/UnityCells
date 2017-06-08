using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CellEffectedEnvState : IEnvironmentState
{
    private readonly Environment env;

    public CellEffectedEnvState(Environment environment)
    {
        env = environment;
    }

    public void Update()
    {
        //ToDiffusionState();
    }

    public void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Cell"))
        {
            //env.inducerUpdate = env.inducerUpdate + env.cellSecretedInducer;
        }
        if (other.gameObject.CompareTag("Env"))
            ToDiffusionState();
    }

    public void ToDiffusionState()
    {
        env.currentState = env.diffusionState;
    }

    public void ToCellEffectedEnvState()
    {
        Debug.Log("Can't transition to same state");
    }
}
