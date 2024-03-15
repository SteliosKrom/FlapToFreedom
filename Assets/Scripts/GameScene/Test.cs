using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    private void OnDestroy()
    {
        Debug.Log(name + "Was destroyed");
    }
}
