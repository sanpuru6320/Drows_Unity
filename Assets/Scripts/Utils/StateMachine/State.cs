using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Utilitys.StateMachine
{
    public class State<T> : MonoBehaviour
    {
        public virtual void Enter(T owner) { } //�X�e�[�g�J�n��

        public virtual void Excute() { } //�X�e�[�g�i�s��
        public virtual void Exit() { } //�@�X�e�[�g�I����
    }
}

