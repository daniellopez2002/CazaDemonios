using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class followTrigger : MonoBehaviour
{
    [SerializeField] patrol logic;

    
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            logic.lookPlayer = true;
            logic.agent.Stop();
            logic.agent.nextPosition = other.transform.position;
            logic.agent.Resume();
            Debug.Log("si");
        }
    }

}
