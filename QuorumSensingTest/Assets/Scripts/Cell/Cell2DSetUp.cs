using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cell2DSetUp : MonoBehaviour
{

    public GameObject cellAgent;
    private GameObject[] cell;

    // Use this for initialization
    void Awake()
    {
        cell = new GameObject[1000];
        int space = 23;
        for (int i = 0; i < 10; i++)
        {
            cell[i] = Instantiate(cellAgent, new Vector3(Random.Range(16, space), Random.Range(16, space), 1), Quaternion.identity);
            cell[i].name = "cell" + i.ToString();
        }
        cell[49] = Instantiate(cellAgent, new Vector3(20, 20, 1), Quaternion.identity);
        cell[49].name = "cell" + 49.ToString();
        cell[50] = Instantiate(cellAgent, new Vector3(20, 21, 1), Quaternion.identity);
        cell[50].name = "cell" + 50.ToString();

        //for (int i = 0; i < 50; i++)
        //{
        //    for (int j = 0; j < 20; j++)
        //    {
        //        cell[i,j] = Instantiate(cellAgent, new Vector3(space / 2 - i + 25, space / 2 - j + 10), Quaternion.identity);
        //        cell[i,j].name = "cell" + i.ToString() + j.ToString();
        //    }
        //}

        //for (int i = 30; i < 70; i = i + 2)
        //{
        //    for (int j = 30; j < 70; j = j + 2)
        //    {
        //        cell[i, j] = Instantiate(cellAgent, new Vector3(i, j), Quaternion.identity);
        //        cell[i, j].name = "cell" + i.ToString() + j.ToString();

        //        cell[i, j+1] = Instantiate(cellAgent, new Vector3(i, j + 1), Quaternion.identity);
        //        cell[i, j+1].name = "cell" + i.ToString() + (j+1).ToString();
        //    }
        //}

        //cell[0] = Instantiate(cellAgent, new Vector3(50, 50, 1), Quaternion.identity);
        //cell[0].name = "cell0";


    }
}