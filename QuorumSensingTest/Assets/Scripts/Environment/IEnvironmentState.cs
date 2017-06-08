using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IEnvironmentState
{
    void Update();

    void OnTriggerStay(Collider other);

    void ToDiffusionState();

    void ToCellEffectedEnvState();

}
