using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pasar : MonoBehaviour

{
     public Animator lapuerta;
     

     private void OnTriggerEnter(Collider other)
     {
       lapuerta.Play("abrir");
     }

      private void OnTriggerExit(Collider other)
     {
       lapuerta.Play("cerrar");


     }
}
