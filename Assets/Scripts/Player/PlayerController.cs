using System;
using Actor;
using Enemy;
using StateMachines;
using UnityEngine;
using UnityEngine.Serialization;
using Utils;

namespace Player
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private PlayerStateMachine playerStateMachine;
        [SerializeField] private GameStateMachine gameStateMachine;
        [SerializeField] private PlayableActor actor;

        private GameState currentGameState;
        // Start is called before the first frame update
        private float moveSpeed;
        private float dirX;

        private Rigidbody2D rb;
        private IActorCommand jump, fire;

        public PlayableActor Actor
        {
            get => actor;
            set => BindActor(value);
        }

        private void OnEnable()
        {
            BindActor(actor);
        }

        private void Awake()
        {
            moveSpeed = 100f;
            rb = GetComponent<Rigidbody2D>();
            currentGameState = gameStateMachine.GetCurrentGameState();
        }
        
        void Update()
        {
            dirX = Input.GetAxisRaw("Horizontal") * moveSpeed * Time.deltaTime;
        
            // triggers walking or idle transition
            playerStateMachine.Trigger(dirX == 0 ? PlayerTransition.IsIdle : PlayerTransition.IsWalking, null);

            var command = GetCommand();
            command?.Execute();
        }

        private void FixedUpdate()
        {
            currentGameState = gameStateMachine.GetCurrentGameState();
            switch (currentGameState)
            {
                case GameState.Play:
                    rb.WakeUp();
                    break;
                case GameState.Menu:
                    rb.Sleep();
                    break;
            }
            
            CheckAliveCondition();
        }

        private void OnCollisionEnter2D(Collision2D other)
        {
            foreach (var go in other.contacts)
            {
                if (go.collider.gameObject.layer == Layers.Obstacle)
                {
                    Debug.Log("Runs against Wall");
                    dirX = 0;
                    moveSpeed = 0;
                    return;
                }

                moveSpeed = 100f;
            }
        }

        public float GetDirX()
        {
            return dirX;
        }

        public void TweakMovementSpeed(float val)
        {
            moveSpeed = val;
        }

        public GameState GetCurrentGameStateFromPlayerParent()
        {
            return currentGameState;
        }

        private IActorCommand GetCommand()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                playerStateMachine.Trigger(PlayerTransition.IsJumping);
                return jump;
            }

            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                return fire;
            }

            return null;
        }
        
        private void BindActor(IActor actor)
        {
            this.actor = actor as PlayableActor;
            jump = new JumpCommand(actor);
            fire = new FireCommand(actor);
        }

        private void CheckAliveCondition()
        {
            if (actor.Alive) return;
            gameStateMachine.Trigger(GameTransition.ShowGameOver);
            Destroy(gameObject);
        }
    }
}
