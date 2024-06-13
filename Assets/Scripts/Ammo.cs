using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ammo : MonoBehaviour
{  
    [SerializeField] AmmoSlot[] ammoSlots;

    [System.Serializable]
    public class AmmoSlot
    {
        public AmmoType ammoType;
        public int ammoAmount;
    }

    public int GetCurrentAmmo(AmmoType ammoType) // AmmoType ammoType
    {
        return GetAmmoSlot(ammoType).ammoAmount;
    }

    public void ReduceCurrentAmmo(AmmoType ammoType) // AmmoType ammoType
    {
        GetAmmoSlot(ammoType).ammoAmount--;
    }

    public void IncreaseCurrentAmmo(AmmoType ammoType, int ammoAmount) // AmmoType ammoType, 
    {
        GetAmmoSlot(ammoType).ammoAmount += ammoAmount;
    }

    private AmmoSlot GetAmmoSlot(AmmoType ammoType)
    {
        foreach (AmmoSlot slot in ammoSlots)
        {
            if (slot.ammoType == ammoType)
            {
                return slot;
            }
        }
        return null;
    }
}
