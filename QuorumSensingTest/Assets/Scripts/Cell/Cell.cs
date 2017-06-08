using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cell : MonoBehaviour
{
    public float sensedInducer;
    public float cellSize;
    public float age;
    public float currentAge;
    public float birthTime;
    public float ageRange;
    public float cellReceivedInducer;
    public double mu0;
    public double epsilon;
    public double antigen;
    public double secRate;
    public float recPercentage;
    public double error;
    public double iL2;
    public double weightAntigen;
    public double recIL2;
    public double weightRecIL2;
    public int iteration;



    [HideInInspector] public ICellState currentState;
    [HideInInspector] public GeneralBehaveState generalBehaveState;
    [HideInInspector] public DivisionState divisionState;

    private void Awake()
    {
        generalBehaveState = new GeneralBehaveState(this);
        divisionState = new DivisionState(this);
    }


    // Use this for initialization
    void Start()
    {
        age = 5;
        ageRange = 2;
        iteration = -1;
        birthTime = Time.time;
        age = age + Random.Range(0, ageRange);
        cellSize = 1;
        currentState = generalBehaveState;
        weightAntigen = 1;
        weightRecIL2 = 0;
        GlobalVariables.cellIL2Negative = new double[100000];
        GlobalVariables.antigenWeightNegative = new double[100000];
        GlobalVariables.recWeightNegative = new double[100000];
        GlobalVariables.recIL2negative = new double[100000];
        GlobalVariables.cellIL2Positive = new double[100000];
        GlobalVariables.antigenWeightPositive = new double[100000];
        GlobalVariables.recWeightPositive = new double[100000];
        GlobalVariables.recIL2Positive = new double[100000];

        System.Random rand = new System.Random(); //reuse this if you are generating many
        double u1 = 1.0 - rand.NextDouble(); //uniform(0,1] random doubles
        double u2 = 1.0 - rand.NextDouble();
        double randStdNormal = System.Math.Sqrt(-2.0 * System.Math.Log(u1)) *
                     System.Math.Sin(2.0 * System.Math.PI * u2); //random normal(0,1)
        double randNormal = 50 + 10 * randStdNormal; //random normal(mean,stdDev^2)

        antigen = 1000 + randNormal;

        for (int i = 49; i < 50; i++)
        {
            string name = "cell" + i.ToString();
            if (gameObject.name == name)
            {
                antigen = 0;
            }
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        iteration++;
        currentState.Update();
    }

    private void OnTriggerStay(Collider other)
    {
        currentState.OnTriggerStay(other);
    }
}
