﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataHandler : InputManager
{
    private static DataHandler instance;
    public static DataHandler Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<DataHandler>();
            }
            return instance;
        }
    }
}
