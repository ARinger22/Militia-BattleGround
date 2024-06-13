using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUp : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] AmmoType ammoType;
    [SerializeField] int ammoAmount = 5;
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            Debug.Log("Picked Up Ammo");
            FindObjectOfType<Ammo>().IncreaseCurrentAmmo(ammoType, ammoAmount);
            Destroy(gameObject);
        }
    }
}
