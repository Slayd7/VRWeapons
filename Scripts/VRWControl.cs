using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Valve.VR.InteractionSystem;


namespace VRWeapons
{
    public class VRWControl : MonoBehaviour
    {
        [HideInInspector]
        public List<Collider> weaponMainColliders;

        [Header("Gunshot layer mask")]
        [Header("For now, all it is capable of is providing the shot layer mask.")]
        [Header("VRWControl will be completed soon. Will handle events.")]
        public LayerMask shotMask;

        public delegate void TriggerHaptics();

        private void Start()
        {
            weaponMainColliders = new List<Collider>(CountWeapons());
            GetWeaponMainColliders();
        }

        int CountWeapons()
        {
            int i = 0;
            foreach(Weapon tmp in FindObjectsOfType<Weapon>())
            {
                i++;
            }
            return i;
        }

        public void GetWeaponMainColliders()
        {
            int i = 0;
            foreach (Weapon tmp in FindObjectsOfType<Weapon>())
            {
                if (tmp.weaponBodyCollider != null)
                {
                    weaponMainColliders.Insert(i, tmp.weaponBodyCollider);
                }
                i++;
            }
        }

        public static float V3InverseLerp(Vector3 a, Vector3 b, Vector3 value)
        {
            Vector3 AB = b - a;
            Vector3 AV = value - a;
            return Mathf.Clamp(Vector3.Dot(AV, AB) / Vector3.Dot(AB, AB), 0, 1);
        }

        public static Vector3 V3Clamp(Vector3 value, Vector3 min, Vector3 max)
        {
            Vector3 tmp = value;
            tmp = new Vector3(Mathf.Clamp(tmp.x, min.x, max.x), Mathf.Clamp(tmp.y, min.y, max.y), Mathf.Clamp(tmp.z, min.z, max.z));
            return tmp;
        }
    }
}