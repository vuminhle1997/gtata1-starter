using Actor;
using StateMachines;
using UnityEngine;

namespace Player
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private PlayerStateMachine playerStateMachine;
        [SerializeField] private GameStateMachine gameStateMachine;
        // The player's unique PlayableActor script
        [SerializeField] private PlayableActor actor;
        public LayerMask obstacleLayer;
        
        private GameState currentGameState;

        #region Stats

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

        #endregion

        /// <summary>
        /// Same as Awake (if script/gameobject starts and is enabled)
        /// </summary>
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
            // Nested ternary operator
            // if the player is near an obstacle, then don't move 
            // if the other case is true, then observe if the player is holding the shift key down (if the player is running)
            // when the player runs, use the running speed parameter
            // otherwise, use the normal movement speed instead
            if (rb.IsAwake())
            {
                dirX = !isNearObstacle ? !IsRunning ? (dirXAxis * MoveSpeed * Time.deltaTime) : (dirXAxis * RunningSpeed * Time.deltaTime) : 0;
                // triggers walking or idle transition
                playerStateMachine.Trigger(dirX == 0 ? PlayerTransition.IsIdle : PlayerTransition.IsWalking, null);

                // get input and execute commands (jump, fire or run)
                var command = GetCommand();
                command?.Execute();
            }
        }

        /// <summary>
        /// Checks game's current state.
        /// Freezes the RigidBody of player, if game is paused.
        /// Otherwise, resume the RigidBody's physics.
        /// </summary>
        void FixedUpdate()
        {
            currentGameState = gameStateMachine.GetCurrentGameState();
            Debug.Log(currentGameState);
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

        #region I_PlayableActor

        /// <summary>
        /// Returns a specific command by the user's input.
        /// </summary>
        /// <returns>
        /// Either Jump, Fire or Run
        /// </returns>
        private IActorCommand GetCommand()
        {
            var playerState = playerStateMachine.GetCurrentState();
            if (Input.GetKeyDown(KeyCode.Space) && playerState != PlayerState.Jump)
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

        /// <summary>
        /// Binds the serialized actor script (an unique script for the player) to this object.
        /// Initialize the three commands to this object. 
        /// </summary>
        /// <param name="actor"></param>
        private void BindActor(IActor actor)
        {
            this.actor = actor as PlayableActor;
            jump = new JumpCommand(actor);
            fire = new FireCommand(actor);
            run = new RunCommand(actor);
        }

        #endregion

        #region Getters

        /// <summary>
        /// Returns the player's current x-Position.
        /// </summary>
        /// <returns></returns>
        public float GetDirX()
        {
            return dirX;
        }

        /// <summary>
        /// Returns the game's current state.
        /// </summary>
        /// <returns></returns>
        public GameState GetCurrentGameState()
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

        #endregion

        #region Misc

        /// <summary>
        /// Checks the player's alive condition.
        /// If the player is dead, destroy this object and switch to game over screen.
        /// </summary>
        private void CheckAliveCondition()
        {
            if (actor.Alive) return;
            gameStateMachine.Trigger(GameTransition.ShowGameOver);
            Destroy(gameObject);
        }

        #endregion
    }
}
