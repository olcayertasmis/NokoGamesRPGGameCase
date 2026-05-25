using UnityEngine;
using Noko.Data;

namespace Noko.Enemy.EnemyTypes
{
    public class FleeingEnemy : EnemyAIBase
    {
        private FleeingEnemyData FleeData => (FleeingEnemyData)characterData;

        protected override void HandleTriggeredBehavior()
        {
            if (!attackerTarget) return;

            Vector3 fleeDirection = (characterRigidbody.position - attackerTarget.position).normalized;
            fleeDirection.y = 0f;

            float speed = FleeData.moveSpeed * FleeData.fleeSpeedMultiplier;
            
            Move(fleeDirection, speed);
            Rotate(fleeDirection);
            UpdateAnimation(1f);
        }
    }
}