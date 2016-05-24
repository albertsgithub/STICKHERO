//------------------------------------------------------------------------------
// Copyright 2016 Baofeng Mojing Inc. All rights reserved.
//------------------------------------------------------------------------------

using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using MojingSample.CrossPlatformInput;

public class InputManagerPayMenu : MonoBehaviour
{

    public PayMenuController paymenu_controller;
    void Start()
    {
#if UNITY_EDITOR || UNITY_STANDALONE_WIN || UNITY_IOS
        Destroy(gameObject);
#endif
    }

    void Update()
    {
        if (CrossPlatformInputManager.GetButtonUp("OK") || CrossPlatformInputManager.GetButtonUp("Submit"))
        {
            if(paymenu_controller != null)
                paymenu_controller.PressCurrent();
        }

        if (CrossPlatformInputManager.GetButtonUp("UP"))
        {
            if(paymenu_controller != null)
                paymenu_controller.HoverPrev();
        }

        if (CrossPlatformInputManager.GetButtonUp("DOWN"))
        {
            if (paymenu_controller != null)
                paymenu_controller.HoverNext();
        }
    }
}