using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Utilitys.StateMachine //スタックベース状態遷移
{
    public class StateMachine<T>
    {
        public State<T> CurrentState { get; private set; }
        public Stack<State<T>> StateStack { get; private set; }

        T owner;
        public StateMachine(T owner)
        {
            this.owner = owner;
            StateStack = new Stack<State<T>>();
        }

        public void Excute()
        {
            CurrentState?.Excute();
        }

        public void Push(State<T> newState)
        {
            StateStack.Push(newState);
            CurrentState = newState;
            CurrentState.Enter(owner);
        }

        public void Pop()
        {
            StateStack.Pop();
            CurrentState.Exit();
            CurrentState = StateStack.Peek();
        }

        public void ChangeState(State<T> newState)
        {
            if(CurrentState != null)
            {
                StateStack.Pop();
                CurrentState.Exit();
            }

            StateStack.Push(newState);
            CurrentState = newState;
            CurrentState.Enter(owner);

        }

        public IEnumerator PushAndWait(State<T> newState)
        {
            var oldState = CurrentState;
            Push(newState);
            yield return new WaitUntil(() => CurrentState == oldState);
        }

        public State<T> GetPrevState()
        {
            return StateStack.ElementAt(1);
        }
    }

}
