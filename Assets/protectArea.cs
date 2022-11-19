using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class protectArea : MonoBehaviour
{

    playerController playerController;

    private void Awake()
    {
        playerController = GetComponentInParent<playerController>();
    }
    // Start is called before the first frame update
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Demon" && playerController != null)
        {
            if (playerController.protect)
            {
                Debug.Log("enemigo hit");
                //afecta enemigo
            }
        }
    }
}
