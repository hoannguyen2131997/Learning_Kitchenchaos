using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommadPatternBasic : MonoBehaviour
{
#if UNITY_EDITOR_SKIP
    
    // Uses an interface
    // Every gameplay action will apply the ICommand interface (you could also implement this with an abstract class)
    // Each command object will be responsible for its own Execute and Undo methods
    public interface ICommand
    {
        void Execute();
        void Undo();
    }

    // Need another class to execute and undo commands. For example:
    public class CommandInvoker
    {
        private static Stack<ICommand> undoStack = new Stack<ICommand>();
        
        // ExecuteCommand and UndoCommand methods, it has an undo stack to hold the sequence of command objects.
        public static void ExecuteCommand(ICommand command)
        {
            command.Execute();
            undoStack.Push(command);
        }
        public static void UndoCommand()
        {
            if (undoStack.Count > 0)
            {
                ICommand activeCommand = undoStack.Pop();
                activeCommand.Undo();
            }
        }
    }

    // ============================
    // Example: Undoable movement
    // To move your player around a maze (me cung) in your application, create a PlayerMover responsible for shifting the player’s position
    public class PlayerMover : MonoBehaviour
    {
        [SerializeField] private LayerMask obstacleLayer;
        private const float boardSpacing = 1f;
        
        // In this case, the player along the four compass directions
        public void Move(Vector3 movement)
        {
            transform.position = transform.position + movement;
        }
        
        // Use a raycast to detect the walls in the appropriate LayerMask
        public bool IsValidMove(Vector3 movement)
        {
            return !Physics.Raycast(transform.position, movement, boardSpacing, obstacleLayer);
        }
    }

    // Capture the PlayerMover’s Move method as an object.
    // Instead of calling Move directly, create a new class, MoveCommand, that implements the ICommand interface:
    public class MoveCommand : ICommand
    {
        PlayerMover playerMover;
        Vector3 movement;
        
        // The MoveCommand stores any parameters that it needs to execute. Set these up with a constructor.
        // In this case, you save the appropriate PlayerMover component and the movement vector.
        public MoveCommand(PlayerMover player, Vector3 moveVector)
        {
            this.playerMover = player;
            this.movement = moveVector;
        }
        
        // ICommand requires an Execute method to store what you’re trying to accomplish
        public void Execute()
        {
            playerMover.Move(movement);
        }
        
        // ICommand also needs an Undo method to restore the scene back to its previous state
        // The Undo logic subtracts the movement vector, essentially pushing the player in the opposite direction.
        public void Undo()
        {
            playerMover.Move(-movement);
        }
    }
    
    // Once you create the command object and save its needed parameters, use the CommandInvoker’s static ExecuteCommand and UndoCommand methods to pass in your MoveCommand.
    // This runs the MoveCommand’s Execute or Undo and tracks the command object in the undo stack.
    // The InputManager doesn’t call the PlayerMover’s Move method directly.
    // Instead, add an extra method, RunMoveCommand, to create a new MoveCommand and send it to the CommandInvoker.
    
    private void RunPlayerCommand(PlayerMover playerMover, Vector3 movement)
    {
        if (playerMover == null)
        {
            return;
        }
        if (playerMover.IsValidMove(movement))
        {
            ICommand command = new MoveCommand(playerMover, movement);
            CommandInvoker.ExecuteCommand(command);
        }
    }
    
    // Then, set up the various onClick events of the UI Buttons to call RunPlayerCommand with the four movement vectors.
    // Check out the sample project for implementation details for the InputManager or set up your own input using the keyboard or gamepad.
    // Your player can now navigate the maze.
    // Click the Undo button so you can backtrack to the beginning square.
    
#endif 
    
}
