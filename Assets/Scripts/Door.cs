using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    //Animacao da porta abrindo e fechando
    public Animator doorAnim;

    //Cena que deve carregar quando uma porta abrir
    public GameObject areaToSpawn;

    //Booleano para saber se a porta necessita de credencial
    public bool requiresCredential;

    //Booleano para saber qual tier de credencial pertence a porta
    public bool requiresTier1, requiresTier2, requiresTier3;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (requiresCredential)
            {
                if (requiresTier1 && other.GetComponent<PlayerInventory>().hasTier1Credential)
                {
                    doorAnim.SetTrigger("OpenDoor");
                    areaToSpawn.SetActive(true);
                    areaToSpawn.SetActive(true);
                }
                if (requiresTier2 && other.GetComponent<PlayerInventory>().hasTier2Credential)
                {
                    doorAnim.SetTrigger("OpenDoor");
                }
                if (requiresTier3 && other.GetComponent<PlayerInventory>().hasTier3Credential)
                {
                    doorAnim.SetTrigger("OpenDoor");
                    areaToSpawn.SetActive(true);
                }
            }
            else
            {
                doorAnim.SetTrigger("OpenDoor");
                areaToSpawn.SetActive(true);
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (requiresCredential)
            {
                if (requiresTier1 && other.GetComponent<PlayerInventory>().hasTier1Credential)
                {
                    doorAnim.SetTrigger("CloseDoor");
                }
                if (requiresTier2 && other.GetComponent<PlayerInventory>().hasTier2Credential)
                {
                    doorAnim.SetTrigger("CloseDoor");
                }
                if (requiresTier3 && other.GetComponent<PlayerInventory>().hasTier3Credential)
                {
                    doorAnim.SetTrigger("CloseDoor");
                }
            }
            else
            {
                doorAnim.SetTrigger("CloseDoor");
            }
        }
    }
}
