//------------------------------------------------------------------------------
// Copyright 2016 Baofeng Mojing Inc. All rights reserved.
//------------------------------------------------------------------------------

using UnityEngine;
using System.Collections;
using MojingSample.CrossPlatformInput;

public class InputManagerBack : MonoBehaviour {
	
	// Update is called once per frame
	void Update () {
        if (CrossPlatformInputManager.GetButtonDown("C"))
        {
            Debug.Log("C-----GetButtonDown");
        }
        if (CrossPlatformInputManager.GetButton("C"))
        {
            Debug.Log("C-----GetButton");
        }
        else if (CrossPlatformInputManager.GetButtonUp("C") || CrossPlatformInputManager.GetButtonUp("Cancel"))
        {
            Application.LoadLevel("Menu");
        }
	}
}
