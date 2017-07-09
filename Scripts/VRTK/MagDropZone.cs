﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;
using VRWeapons;

[RequireComponent(typeof(VRTK_SnapDropZone))]

public class MagDropZone : MonoBehaviour
{
    Weapon thisWeap;
    Weapon_VRTK_InteractableObject thisWeapInteractable;
    VRTK_SnapDropZone dropZone;

    private void Start()
    {
        dropZone = GetComponent<VRTK_SnapDropZone>();
        dropZone.ObjectSnappedToDropZone += new SnapDropZoneEventHandler(ObjectSnapped);
        dropZone.ObjectUnsnappedFromDropZone += new SnapDropZoneEventHandler(ObjectUnsnapped);

        thisWeap = GetComponentInParent<Weapon>();
        thisWeapInteractable = GetComponentInParent<Weapon_VRTK_InteractableObject>();
    }

    void ObjectSnapped(object sender, SnapDropZoneEventArgs e)
    {
        IMagazine mag = e.snappedObject.GetComponent<IMagazine>();
        mag.MagIn(thisWeap);
        Physics.IgnoreCollision(e.snappedObject.GetComponent<Collider>(), thisWeapInteractable.weaponBodyCollider, true);
        mag.MagDropped += Mag_MagDropped;
    }

    void ObjectUnsnapped(object sender, SnapDropZoneEventArgs e)
    {
        IMagazine mag = e.snappedObject.GetComponent<IMagazine>();
        //Stop listening for mag drop event so we won't redundantly unsnap
        mag.MagDropped -= Mag_MagDropped;
        mag.MagOut(thisWeap);

        Physics.IgnoreCollision(e.snappedObject.GetComponent<Collider>(), thisWeapInteractable.weaponBodyCollider, false);

        //This is necessary for the initial mag so it won't revert to child of weapon
        var interactable = e.snappedObject.GetComponent<VRTK_InteractableObject>();
        if(interactable != null)
        { 
            interactable.SaveCurrentState();
        }
        
    }

    private void Mag_MagDropped(object sender, System.EventArgs e)
    {
        if (sender is IMagazine)
        {
            (sender as IMagazine).MagDropped -= Mag_MagDropped;
        }
        dropZone.ForceUnsnap();
    }

}