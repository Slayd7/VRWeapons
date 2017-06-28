﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;
using VRWeapons;

public class Weapon_VRTK_InteractableObject : VRTK_InteractableObject
{
    Weapon thisWeap;
    Collider col;

    private void Start()
    {
        thisWeap = GetComponent<Weapon>();
        col = thisWeap.GetComponentInChildren<Collider>();
        if (col == null)
        {
            Debug.Log("No collider found");
        }
    }

    public override void OnInteractableObjectGrabbed(InteractableObjectEventArgs e)
    {
        VRW_ControllerActions_VRTK f;
        f = e.interactingObject.GetComponent<VRW_ControllerActions_VRTK>();
        if (f != null)
        {
            f.CurrentHeldWeapon = thisWeap;             // Setting up for touchpad input
        }

        base.OnInteractableObjectGrabbed(e);
        col.enabled = false;
    }

    public override void OnInteractableObjectUngrabbed(InteractableObjectEventArgs e)
    {
        VRW_ControllerActions_VRTK f;
        f = e.interactingObject.GetComponent<VRW_ControllerActions_VRTK>();
        if (f != null)
        {
            f.CurrentHeldWeapon = null;
        }

        base.OnInteractableObjectUngrabbed(e);        
        col.enabled = true;
    }

    public override void StartUsing(GameObject usingObject)
    {
        base.StartUsing(usingObject);
        thisWeap.StartFiring(usingObject);
    }

    public override void StopUsing(GameObject previousUsingObject)
    {
        base.StopUsing(previousUsingObject);
        thisWeap.StopFiring(previousUsingObject);
    }
}
