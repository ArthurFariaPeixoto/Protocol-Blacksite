using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CredentialPickup : MonoBehaviour
{
    public bool isTier1, isTier2, isTier3;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (isTier1)
            {
                other.GetComponent<PlayerInventory>().hasTier1Credential = true;
            }
            if (isTier2)
            {
                other.GetComponent<PlayerInventory>().hasTier2Credential = true;
            }
            if (isTier3)
            {
                other.GetComponent<PlayerInventory>().hasTier3Credential = true;
            }
            Destroy(gameObject);

        }
    }
}
