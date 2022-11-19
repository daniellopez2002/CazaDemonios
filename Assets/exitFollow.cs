using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class exitFollow : MonoBehaviour
{
    [SerializeField] patrol logic;


    private void OnTriggerExit(Collider other)
    {
        if(other.tag == "Player")
        {
            logic.lookPlayer = false;
        }
    }
}
