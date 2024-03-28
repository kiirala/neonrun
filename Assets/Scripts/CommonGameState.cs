using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommonGameState : MonoBehaviour
{
    public bool Crashed { get; private set; }

    public void Crash()
    {
        Crashed = true;
    }
}
