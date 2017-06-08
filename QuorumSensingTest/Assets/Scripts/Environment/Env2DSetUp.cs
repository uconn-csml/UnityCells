using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Env2DSetUp : MonoBehaviour
{

    public GameObject envAgent;
    private GameObject[,] env;      //environmental agents in 3 dimentions (3D array)
    public int spaceSize;                //size of array

    void Awake()
    {

        env = new GameObject[spaceSize, spaceSize];
        for (int y = 0; y < spaceSize; y++)
        {
            for (int x = 0; x < spaceSize; x++)
            {
                env[x, y] = Instantiate(envAgent, new Vector3(x, y, 1), Quaternion.identity);
                env[x, y].name = "env" + x.ToString() + y.ToString();
            }
        }
    }

    private void Start()
    {
        GlobalVariables.space = spaceSize;
        GlobalVariables.numOfEnvAgent = (int)System.Math.Pow(spaceSize, 2);
    }
}