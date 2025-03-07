﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZF : MonoBehaviour
{
    private static ZF instance = null;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if(instance != this)
        {
            Destroy(this.gameObject);
            return;
        }
    }
}
