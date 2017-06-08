using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SetLineRenderer : MonoBehaviour
{
    public UnityEngine.UI.Extensions.UILineRenderer LineRenderer;
    
    private int iteration;
    
    // Use this for initialization
    private void Start()
    {        
    }

    public void FixedUpdate()
    {
        if (iteration == 200)
        {
            SetPoinyList();
        }
        iteration++;        
    }

    public void SetPoinyList()
    {
        for (int i = 0 ; i < iteration/2; i++)
        {
            var point = new Vector2() { x = i, y = (float)GlobalVariables.envIL2Sum[i] / GlobalVariables.numOfEnvAgent };
            //var point = new Vector2() { x = i, y = GlobalVariables.envIL2Sum[i]  };
            var pointList = new List<Vector2>(LineRenderer.Points);
            pointList.Add(point);
            LineRenderer.Points = pointList.ToArray();
        }
    }
}
