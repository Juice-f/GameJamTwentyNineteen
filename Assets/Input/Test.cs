using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Input;

public class Test : MonoBehaviour
{
   public InputMaster controls;


   void Awake()
   {
       controls.Player.Shoot.performed += ctx => Shoot();
       controls.Player.Movement.performed += ctx => Move(ctx.ReadValue<Vector2>());
       controls.Player.Movement.performed += ctx => Move(ctx.ReadValue<float>(), ctx.ReadValue<float>());
   }


   void Shoot(){
       Debug.Log("We shot!");
   }

   void Move(Vector2 direction){
       Debug.Log("Player wants to move!" + direction);
   }

   void Move(float xAxis, float yAxis){
       Debug.Log("Player wants to move!" + xAxis + " - " + yAxis);
   }

   void OnEnable(){
       controls.Enable();
   }

   void OnDisable(){
       controls.Disable();
   }
}
