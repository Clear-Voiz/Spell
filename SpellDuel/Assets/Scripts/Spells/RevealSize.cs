using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RevealSize : MonoBehaviour
{
    private MeshRenderer mr;

    private void Start()
    {
        mr = GetComponent<MeshRenderer>();
        Debug.Log(mr.bounds.extents*2f);
    }
}
