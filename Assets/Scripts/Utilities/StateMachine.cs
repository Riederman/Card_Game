using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class StateMachine
{
    private Queue<IBaseState> states = new Queue<IBaseState>();

    public IBaseState CurrentState { get; private set; }

    public StateMachine(IBaseState state)
    {
        AddState(state);
    }

    public IEnumerator Run()
    {
        while (states.Count > 0)
        {
            // Enter current state
            CurrentState = states.Dequeue();
            Debug.Log("Enter " + CurrentState.ToString());
            CurrentState.Enter();
            // Execute current state
            Debug.Log("Execute " + CurrentState.ToString());
            yield return CurrentState.Execute();
            // Exit current state
            Debug.Log("Exit " + CurrentState.ToString());
            CurrentState.Exit();
        }
        yield return null;
    }

    public void AddState(IBaseState state)
    {
        // Add new state to queue
        Assert.IsTrue(state != null);
        states.Enqueue(state);
    }
}