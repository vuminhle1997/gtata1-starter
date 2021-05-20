using System;
using Actor;
using StateMachines;
using UnityEngine;

namespace Player
{
    public class PlayerPlayableActorScript: PlayableActor
    {
        private Rigidbody2D rb;
        private float jumpForce = 200f;
        private VaccineController vaccineController;
        

        private void Awake()
        {
            rb = GetComponent<Rigidbody2D>();
            vaccineController = gameObject.GetComponent<VaccineController>();
        }

        public override void Jump()
        {
            rb.velocity = Vector2.up * jumpForce;
        }

        public override void FireBullet()
        {
            vaccineController.FireVaccine();
        }
    }
}