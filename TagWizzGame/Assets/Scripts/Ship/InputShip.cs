using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputShip : MonoBehaviour
{
    [HideInInspector] public float horizontal;
    [HideInInspector] public float vertical;
    public KeyCode shootButton = KeyCode.W;

   private void Update()
   {
        horizontal = Input.GetAxisRaw("Horizontal");
        vertical = Input.GetAxis("Vertical");
   }
}
