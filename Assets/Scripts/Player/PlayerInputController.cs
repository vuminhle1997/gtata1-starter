using System;
using Actor;
using UnityEngine;
using UnityEngine.UIElements;

namespace Player
{
    public class PlayerInputController: MonoBehaviour
    {
        [SerializeField] private PlayableActor actor;

        public IActor Actor
        {
            get => actor;
            set => BindActor(value);
        }

        private IActorCommand jump, fire;

        private void BindActor(IActor actor)
        {
            this.actor = actor as PlayableActor;
            jump = new JumpCommand(actor);
            fire = new FireCommand(actor);
        }

        private void OnEnable()
        {
            BindActor(actor);
        }

        private void Update()
        {
            var input = HandleInput();
            input?.Execute();
        }


        private IActorCommand HandleInput()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                return jump;
            }

            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                return fire;
            }

            return null;
        }
    }
}