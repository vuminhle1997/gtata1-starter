using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ButtonsController : MonoBehaviour
{
    public void ButtonPressed(int type)
    {
        switch (type)
        {
            case 1:
                Debug.Log("Pause/Resume pressed");
                break;
            case 2:
                Debug.Log("Menu pressed");
                break;
            default:
                throw new NotImplementedException("No button logic implemented");
        }
    }}
