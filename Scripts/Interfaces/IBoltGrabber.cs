﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VRWeapons
{
    public interface IBoltGrabber
    {
        Collider GetInteractableCollider();
    }
}