﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DummyIUserInput : IUserInput
{
    IEnumerator Start()
    {
        while (true)
        {
            rb = true;
            yield return new WaitForSeconds(0);
        }
    }

    void Update ()
    {
        UpdateDmagDvec(Dup, Dright);
    }
}
