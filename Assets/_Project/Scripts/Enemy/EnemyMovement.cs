using Noko.Core;
using UnityEngine;
using Noko.Data;

namespace Noko.Enemy
{
    public class EnemyMovement : BaseMovement
    {
        [SerializeField] private EnemyData enemyData;
        private float _speedModifier = 1f;

        public void SetSpeedModifier(float modifier) => _speedModifier = modifier;

        public override void Move(Vector3 direction, float speed)
        {
            if (direction.sqrMagnitude <= 0.01f) return;
            base.Move(direction, enemyData.moveSpeed * _speedModifier);
        }
    }
}