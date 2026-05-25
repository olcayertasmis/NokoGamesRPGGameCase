using UnityEngine;

namespace Noko.Enemy
{
    public enum EnemyState { Roaming, Triggered }

    public class EnemyStateMachine : MonoBehaviour
    {
        public EnemyState CurrentState { get; private set; } = EnemyState.Roaming;
        public event System.Action<EnemyState, EnemyState> OnStateChanged;

        public void SetTriggered(bool triggered)
        {
            var newState = triggered ? EnemyState.Triggered : EnemyState.Roaming;
            if (CurrentState != newState)
            {
                var oldState = CurrentState;
                CurrentState = newState;
                OnStateChanged?.Invoke(oldState, newState);
            }
        }
    }
}