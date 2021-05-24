using Actor;
using StateMachines;
using UnityEngine;

namespace Player
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private PlayerStateMachine playerStateMachine;
        [SerializeField] private GameStateMachine gameStateMachine;
        [SerializeField] private PlayableActor actor;

        private GameState currentGameState;
        private float MoveSpeed
        {
            get;
            set;
        }

        private float RunningSpeed
        {
            get;
            set;
        }

        public bool IsRunning
        {
            get;
            set;
        }
        private float dirX, dirXAxis;

        private Rigidbody2D rb;
        private IActorCommand jump, fire, run;

        public PlayableActor Actor
        {
            get => actor;
            set => BindActor(value);
        }

        public LayerMask obstacleLayer;

        void OnEnable()
        {
            BindActor(actor);
        }

        /// <summary>
        /// Initialize player stats!
        /// </summary>
        void Awake()
        {
            IsRunning = false;
            MoveSpeed = 100f;
            RunningSpeed = 200f;
            rb = GetComponent<Rigidbody2D>();
            currentGameState = gameStateMachine.GetCurrentGameState();
        }
        
        void Update()
        {
            CheckAliveCondition();
            
            dirXAxis = Input.GetAxisRaw("Horizontal");
            var isNearObstacle = GetsCollisionWithObstacle(dirXAxis, transform.position);

            dirX = !isNearObstacle ? !IsRunning ? (dirXAxis * MoveSpeed * Time.deltaTime) : (dirXAxis * RunningSpeed * Time.deltaTime) : 0;
        
            // triggers walking or idle transition
            playerStateMachine.Trigger(dirX == 0 ? PlayerTransition.IsIdle : PlayerTransition.IsWalking, null);

            var command = GetCommand();
            command?.Execute();
        }

        void FixedUpdate()
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

            var isRunning = Input.GetKey(KeyCode.LeftShift);
            if (!isRunning)
            {
                IsRunning = false;
            }
            else
            {
                return run;
            }

            return null;
        }

        private void BindActor(IActor actor)
        {
            this.actor = actor as PlayableActor;
            jump = new JumpCommand(actor);
            fire = new FireCommand(actor);
            run = new RunCommand(actor);
        }

        private void CheckAliveCondition()
        {
            if (actor.Alive) return;
            gameStateMachine.Trigger(GameTransition.ShowGameOver);
            Destroy(gameObject);
        }
        
        public float GetDirX()
        {
            return dirX;
        }

        public GameState GetCurrentGameStateFromPlayerParent()
        {
            return currentGameState;
        }
        
        /// <summary>
        /// Returns an Collider2D, if there is an obstacle by the estimated faced direction.
        /// Otherwise, return null.
        /// </summary>
        /// <param name="dir">The facing direction (left or right)</param>
        /// <param name="vecPos">The current player's vector position</param>
        /// <returns></returns>
        private Collider2D GetsCollisionWithObstacle(float dir, Vector2 vecPos)
        {
            Vector2 lookPos = dir < 0 ? new Vector2(vecPos.x - 15, vecPos.y) : new Vector2(vecPos.x + 15, vecPos.y);

            return Physics2D.OverlapCircle(lookPos, 7.5f, obstacleLayer);
        }
    }
}
