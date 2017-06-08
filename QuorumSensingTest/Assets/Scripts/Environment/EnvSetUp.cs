using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnvSetUp : MonoBehaviour {

    public GameObject envAgent;
    private GameObject[,,] env;      //environmental agents in 3 dimentions (3D array)
    public int spaceSize;                //size of array

    void Awake()
    {
        GlobalVariables.space = spaceSize;
        GlobalVariables.numOfEnvAgent = (int)System.Math.Pow(spaceSize, 2);

        env = new GameObject[spaceSize, spaceSize, spaceSize];
        for (int z = 0; z < spaceSize; z++)
        {
            for (int y = 0; y < spaceSize; y++)
            {
                for (int x = 0; x < spaceSize; x++)
                {
                    env[x, y, z] = Instantiate(envAgent, new Vector3(x, y, z), Quaternion.identity);
                    env[x, y, z].name = "env" + x.ToString() + y.ToString() + z.ToString();
                }
            }
        }
    }
}
