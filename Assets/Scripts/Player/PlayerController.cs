using System;
using Actor;
using Enemy;
using StateMachines;
using UnityEngine;

namespace Player
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private PlayerStateMachine stateMachine;
        [SerializeField] private GameStateMachine _gameStateMachine;
        [SerializeField] private PlayableActor actor;

        private GameState _currentGameState;
        // Start is called before the first frame update
        private float _moveSpeed;
        private float _dirX;

        private Rigidbody2D _rb;
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
            _moveSpeed = 100f;
            _rb = GetComponent<Rigidbody2D>();
            _currentGameState = _gameStateMachine.GetCurrentGameState();
        }

        // Update is called once per frame
        void Update()
        {
            _dirX = Input.GetAxisRaw("Horizontal") * _moveSpeed * Time.deltaTime;
        
            // triggers walking or idle transition
            stateMachine.Trigger(_dirX == 0 ? PlayerTransition.IsIdle : PlayerTransition.IsWalking, null);

            var command = GetCommand();
            command?.Execute();
        }

        private void FixedUpdate()
        {
            _currentGameState = _gameStateMachine.GetCurrentGameState();
            // isGrounded = Physics2D.OverlapCircle(groundCheck.position, 0.5f, groundLayer);
            switch (_currentGameState)
            {
                case GameState.Play:
                    _rb.WakeUp();
                    break;
                case GameState.Menu:
                    _rb.Sleep();
                    break;
            }
            
            CheckAliveCondition();
        }
        
        public float GetDirX()
        {
            return _dirX;
        }

        public void TweakMovementSpeed(float val)
        {
            _moveSpeed = val;
        }

        public GameState GetCurrentGameStateFromPlayerParent()
        {
            return _currentGameState;
        }

        private IActorCommand GetCommand()
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
        
        private void BindActor(IActor actor)
        {
            this.actor = actor as PlayableActor;
            jump = new JumpCommand(actor);
            fire = new FireCommand(actor);
        }

        private void CheckAliveCondition()
        {
            if (actor.Alive) return;
            _gameStateMachine.Trigger(GameTransition.ShowGameOver);
            Destroy(gameObject);
        }
    }
}
