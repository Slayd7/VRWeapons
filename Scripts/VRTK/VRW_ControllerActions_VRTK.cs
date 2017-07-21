﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;

namespace VRWeapons.InteractionSystems.VRTK
{

    public class VRW_ControllerActions_VRTK : MonoBehaviour
    {
        [HideInInspector]
        public Weapon CurrentHeldWeapon;

        private void Start()
        {
            GetComponent<VRTK_ControllerEvents>().TouchpadPressed += new ControllerInteractionEventHandler(DropMagazine);
            GetComponent<VRTK_ControllerEvents>().TriggerAxisChanged += new ControllerInteractionEventHandler(TriggerAxisChanged);
        }

        private void DropMagazine(object sender, ControllerInteractionEventArgs e)
        {
            Debug.Log(CurrentHeldWeapon);
            if (CurrentHeldWeapon != null && e.controllerReference.scriptAlias == CurrentHeldWeapon.holdingDevice)
            {
                CurrentHeldWeapon.DropMagazine();
            }
        }

        private void TriggerAxisChanged(object sender, ControllerInteractionEventArgs e)
        {
            if (CurrentHeldWeapon != null && e.controllerReference.scriptAlias == CurrentHeldWeapon.holdingDevice)
            {
                CurrentHeldWeapon.SetTriggerAngle(e.buttonPressure);
                if (Debug.isDebugBuild)
                {
                    Debug.Log("Holding device: " + CurrentHeldWeapon.holdingDevice);
                }
            }
        }
    }
}