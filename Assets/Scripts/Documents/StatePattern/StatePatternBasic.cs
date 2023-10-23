using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatePatternBasic : MonoBehaviour
{
#if UNITY_EDITOR
    //  State pattern solves two problems
    // 1. An object should change its behavior when its internal state changes
    // 2. State-specific behavior is defined independently. Adding new states does not impact the behavior of existing states.
    // You want to minimize the impact on existing states when you add new ones. Instead, you can encapsulate a state as an object.
    
    
    // You enter the state and loop each frame until a condition causes control flow to exit. To implement this pattern, create an interface, IState:
    // Each concrete state in your game will implement the IState interface:
    public interface IState
    {
        public void Enter()
        {
            // code that runs when we first enter the state
        }
        
        // You can further segment the Update method as MonoBehaviour does, using a FixedUpdate for physics, LateUpdate, and so on.
        public void Update()
        {
            // per-frame logic, include condition to transition to a new state
        }
        public void Exit()
        {
            // code that runs when we exit the state
        }
    }
    
    // You’ll need to create a class for each state that implements IState. In the sample project, a separate class has been set up for WalkState, IdleState, and JumpState.
    // Another class, the StateMachine, will then manage how control flow enters and exits the states. With the three example states

    [Serializable]
    public class StateMachine
    {
        public IState CurrentState { get; private set; }
        
        // To follow the pattern, the StateMachine references a public object for each state under its management (in this case, walkState, jumpState, and idleState).
        public WalkState walkState;
        public JumpState jumpState;
        public IdleState idleState;
        
        // Because StateMachine doesn’t inherit from MonoBehaviour, use a constructor to set up each instance
        public void Initialize(IState startingState)
        {
            CurrentState = startingState;
            startingState.Enter();
        }
        public void TransitionTo(IState nextState)
        {
            CurrentState.Exit();
            CurrentState = nextState;
            nextState.Enter();
        }
        public void Update()
        {
            if (CurrentState != null)
            {
                CurrentState.Update();
            }
        }
    }
    
    /* Hoan - Edit . (this version not implemented now but i will update later)  
    public void FuncStateMachine(PlayerController player)
    {
        WalkState walkState = new WalkState(player);
        JumpState jumpState = new JumpState(player);
        IdleState idleState = new IdleState(player);
    }
    */
    
    /* Original
    public StateMachine(PlayerController player)
    {
        this.walkState = new WalkState(player);
        this.jumpState = new JumpState(player);
        this.idleState = new IdleState(player);
    }
    */
    
    // You can pass in any parameters needed to the constructor. In the sample project, a PlayerController is referenced in each state. You then use that to update each state per frame (see the IdleState example below).
    public class IdleState : IState
    {
        private PlayerController player;
        public IdleState(PlayerController player)
        {
            this.player = player;
        }
        public void Enter()
        {
            // code that runs when we first enter the state
        }
        public void Update()
        {
            // Here we add logic to detect if the conditions exist to
            // transition to another state
            // …
        }
        public void Exit()
        {
            // code that runs when we exit the state
        }
    }
    
    // Add two class to complete implement pattern 
    public class WalkState : IState
    {
        private PlayerController player;
        public WalkState(PlayerController player)
        {
            this.player = player;
        }
        public void Enter()
        {
            // code that runs when we first enter the state
        }
        public void Update()
        {
            // Here we add logic to detect if the conditions exist to
            // transition to another state
            // …
        }
        public void Exit()
        {
            // code that runs when we exit the state
        }
    }
    
    public class JumpState : IState
    {
        private PlayerController player;
        public JumpState(PlayerController player)
        {
            this.player = player;
        }
        public void Enter()
        {
            // code that runs when we first enter the state
        }
        public void Update()
        {
            // Here we add logic to detect if the conditions exist to
            // transition to another state
            // …
        }
        public void Exit()
        {
            // code that runs when we exit the state
        }
    }
    
    // Note the following about the StateMachine:
    // 1. The Serializable attribute allows us to display the StateMachine (and its public fields) in the Inspector. Another MonoBehaviour (e.g., a PlayerController or EnemyController) can then use the StateMachine as a field.
    // 2. The CurrentState property is read-only. The StateMachine itself does not explicitly set this field. An external object like the PlayerController can then invoke the Initialize method to set the default State.
    // 3. Each State object determines its own conditions for calling the TransitionTo method to change the currently active state. You can pass in any necessary dependencies (including the State Machine itself) to each state when setting up the StateMachine instance.
    
    // In the example project, the PlayerController already includes a reference to the StateMachine, so you only pass in one player parameter.
    // Each state object will manage its own internal logic, and you can make as many
    // states as needed to describe your GameObject or component. Each one gets its own class that implements IState. In keeping with the SOLID principles, adding more states has minimal impact on any previously created states.
#endif
}
