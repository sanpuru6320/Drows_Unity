using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Utilitys.StateMachine
{
    public class State<T> : MonoBehaviour
    {
        public virtual void Enter(T owner) { } //ステート開始時

        public virtual void Excute() { } //ステート進行中
        public virtual void Exit() { } //　ステート終了時
    }
}

