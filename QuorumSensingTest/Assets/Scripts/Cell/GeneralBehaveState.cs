using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneralBehaveState : ICellState
{
    private readonly Cell cell;
    private float currentTime;
    private double u;
    private float conc;
    private int it = 0;
    public double mu;

    public GeneralBehaveState(Cell statePatternCell)
    {
        cell = statePatternCell;
    }

    //----------Update cell general behaviour every iteration----------
    public void Update()
    {
        //cell.cellReceivedInducer = 0;         //updating cell received inducer to 0 for next iteration (quorum sensing)
        //MoveCell();                           //slightly move cell

        if (cell.iteration % 2 == 0)            //run regulation algorithms (NLMS, Positive/negative feedback)
        {
            IL2_NLMS_Regulation();
            //L2_positiveFeedBack_Regulation();
            //IL2_NegativeFeedBack_Regulation();
        }       
    }

    //----------Update cell-environment molecule share in each collision----------
    public void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Env"))
        {
            // cell.cellReceivedInducer = cell.cellReceivedInducer + cell.recPercentage * other.gameObject.GetComponent<Environment>().inducerUpdate;
            // QuorumSensing();
            if (cell.iteration % 2 == 1)
            {
                System.Random rand = new System.Random(); //reuse this if you are generating many
                double u1 = 1.0 - rand.NextDouble(); //uniform(0,1] random doubles
                double u2 = 1.0 - rand.NextDouble();
                double randStdNormal = System.Math.Sqrt(-2.0 * System.Math.Log(u1)) *
                             System.Math.Sin(2.0 * System.Math.PI * u2); //random normal(0,1)
                double randNormal = 5 + 1 * randStdNormal; //random normal(mean,stdDev^2)
                randNormal = 0;
                cell.recIL2 = cell.recIL2 + cell.recPercentage * other.gameObject.GetComponent<Environment>().oldIL2 + randNormal;
            }
                
        }
    }

    public void ToDivisionState()
    {
        cell.currentState = cell.divisionState;
    }

    public void ToGeneralBehaveState()
    {
        Debug.Log("Can't transition to same state");
    }

    void MoveCell()
    {
        cell.transform.position = new Vector3(Mathf.Clamp(cell.transform.position.x + Random.Range(-0.5f, 0.5f), 0, GlobalVariables.space - 1),
            Mathf.Clamp(cell.transform.position.y + Random.Range(-0.5f, 0.5f), 0, GlobalVariables.space - 1)/*,
            Mathf.Clamp(cell.transform.position.z + Random.Range(-0.25f, 0.25f), 0, GlobalVariables.space - 1)*/);    //set random movement of the cell within a limit space
    }

    void QuorumSensing()
    {
        currentTime = Time.time;
        cell.currentAge = currentTime - cell.birthTime;
        if (cell.cellReceivedInducer < 0.05 && cell.currentAge >= cell.age) //start cell division due to quorum sensing and if/then condition
            ToDivisionState();
    }

    void IL2_NLMS_Regulation()
    {
        if (cell.gameObject.name == "cell49") //collect information of a specific cell to plot its behavior
        {
            GlobalVariables.cellIL2Positive[it] = cell.iL2;
            GlobalVariables.antigenWeightPositive[it] = cell.weightAntigen;
            GlobalVariables.recWeightPositive[it] = cell.weightRecIL2;
            GlobalVariables.recIL2Positive[it] = cell.recIL2;
            it++;
        }
        if (cell.gameObject.name == "cell50") //collect information of a specific cell to plot its behavior
        {
            GlobalVariables.cellIL2Negative[it] = cell.iL2;
            GlobalVariables.antigenWeightNegative[it] = cell.weightAntigen;
            GlobalVariables.recWeightNegative[it] = cell.weightRecIL2;
            GlobalVariables.recIL2negative[it] = cell.recIL2;
            it++;
        }

        cell.error = cell.recIL2 - cell.iL2; //calculate error
        cell.iL2 = cell.weightRecIL2 * cell.recIL2 + cell.weightAntigen * cell.antigen; //update intracellular IL2
        u = System.Math.Sqrt(System.Math.Pow(cell.antigen, 2) + System.Math.Pow(cell.recIL2 , 2)); //u=(antigen^2 + recIL2^2)^(1/2)
        mu = cell.mu0 / (cell.epsilon + System.Math.Pow(u,2)); //calculate mu normalized factor of LMS algorythm
        cell.weightRecIL2 = cell.weightRecIL2 + mu * cell.error * cell.recIL2 ; //update coefficient of received IL2
        cell.weightAntigen = cell.weightAntigen + mu * cell.error * cell.antigen; //update coefficient of antigen

        cell.recIL2 = 0; //set received IL2 to zero to calculate it independently in next iteration.
    }

    void IL2_NegativeFeedBack_Regulation()
    {
        if (cell.gameObject.name == "cell49")
        {
            GlobalVariables.cellIL2Positive[it] = cell.iL2;
            GlobalVariables.recIL2Positive[it] = cell.recIL2;
            it++;
        }
        if (cell.gameObject.name == "cell50")
        {
            GlobalVariables.cellIL2Negative[it] = cell.iL2;
            GlobalVariables.recIL2negative[it] = cell.recIL2;
            it++;
        }
        cell.iL2 = (-0.1) * cell.recIL2 + 0.1 * cell.antigen; //update intracellular IL2
        cell.recIL2 = 0; //set received IL2 to zero to calculate it independently in next iteration.
    }

    void IL2_positiveFeedBack_Regulation()
    {
        if (cell.gameObject.name == "cell49")
        {
            GlobalVariables.cellIL2Positive[it] = cell.iL2;
            GlobalVariables.recIL2Positive[it] = cell.recIL2;
            it++;
        }
        if (cell.gameObject.name == "cell50")
        {
            GlobalVariables.cellIL2Negative[it] = cell.iL2;
            GlobalVariables.recIL2negative[it] = cell.recIL2;
            it++;
        }
        cell.iL2 = (0.1) * cell.recIL2 + 0.1 * cell.antigen; //update intracellular IL2
        cell.recIL2 = 0; //set received IL2 to zero to calculate it independently in next iteration.
    }
}
