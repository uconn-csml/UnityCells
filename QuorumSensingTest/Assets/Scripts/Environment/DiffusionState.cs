using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiffusionState : IEnvironmentState
{
    private readonly Environment env;
    private float diff = 0.1f;
    private float conc;
    private int it = 0;

    public DiffusionState(Environment environment)
    {
        env = environment;
    }
    //----------Update environment every iteration----------
    public void Update()
    {
        //env.oldInducer = env.inducerUpdate;
        //Debug.Log("Update" + GlobalVariables.envNumerator + "  " + env.gameObject.name + "  " + env.inducerUpdate);

        if (env.iteration % 2 == 1)
        {
            GlobalVariables.envIL2Sum[it] = GlobalVariables.envIL2Sum[it] + env.oldIL2; //save sum of environment IL2 in array
            if (GlobalVariables.envIL2Max[it] < env.oldIL2)
                GlobalVariables.envIL2Max[it] = env.oldIL2;

            env.oldIL2 = env.iL2Update + env.recIL2; //calculate final environment IL2 from diffusion and cells and set them as IL2 in previous iteration
            
            //set boundary condition such that IL2 of all environment agents that are placed in boundary is set to zero.
            if (env.GetComponent<Transform>().position.x == 0 || env.GetComponent<Transform>().position.x == GlobalVariables.space - 1 ||
                   env.GetComponent<Transform>().position.y == 0 || env.GetComponent<Transform>().position.y == GlobalVariables.space - 1 ||
                   env.GetComponent<Transform>().position.z == 0 || env.GetComponent<Transform>().position.z == GlobalVariables.space - 1)
                env.oldIL2 = 0;

            env.iL2Update = env.oldIL2;

            conc = (float)env.oldIL2 /50f;  //set alpha value for transparency of environment agents
            if (conc > 1)
                conc = 1;
            if (conc < 0)
                conc = 0;
            env.gameObject.GetComponent<Renderer>().material.color = new Color(1, 0, 0, conc);

            env.recIL2 = 0; //set received IL2 to zero to calculate it independently in next iteration.
            it++;
        }
    }

    public void OnTriggerStay(Collider other)
    {
        //-----------environment diffusion equation & set concentration by transparency change
        if (other.gameObject.CompareTag("Env"))
        {
            //env.newInducer = env.inducerUpdate + diff * (other.gameObject.GetComponent<Environment>().oldInducer - env.oldInducer);
            //env.inducerUpdate = env.newInducer;
            //conc = env.inducerUpdate * 0.01f;
            //env.gameObject.GetComponent<Renderer>().material.color = new Color(1, 0, 0, conc);
            if (env.iteration % 2 == 0)
            {
                env.newIL2 = env.iL2Update + diff * (other.gameObject.GetComponent<Environment>().oldIL2 - env.oldIL2);
                env.iL2Update = env.newIL2;
            }
        }

        if (other.gameObject.CompareTag("Cell"))
        {
            if (env.iteration % 2 == 0)
                env.recIL2 = env.recIL2 + other.gameObject.GetComponent<Cell>().iL2 * other.gameObject.GetComponent<Cell>().secRate;
        }
    }

    public void ToDiffusionState()
    {
        Debug.Log("Can't transition to same state");
    }

    public void ToCellEffectedEnvState()
    {
        env.currentState = env.cellEffectedEnvState;
    }

}
