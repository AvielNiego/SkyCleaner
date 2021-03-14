using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelfDestruct : MonoBehaviour
{
    [SerializeField] private float timeout = 1f;

    private void Start()
    {
        Destroy(gameObject, timeout);
    }
}
