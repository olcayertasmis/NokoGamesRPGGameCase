using Noko.Core;
using UnityEngine;
using Noko.Interfaces;
using Noko.Systems;
using Noko.Data;

namespace Noko.Player
{
    [RequireComponent(typeof(Rigidbody))]
    public class PlayerMovement : BaseMovement
    {
        [Header("Data & References")]
        [SerializeField] private PlayerData playerData;
        [SerializeField] private MonoBehaviour inputHandlerRef;

        private UpgradeManager _upgradeManager;
        private IInputHandler _inputHandler;
        private Vector3 _movementInput;

        private void Awake()
        {
            _inputHandler = inputHandlerRef as IInputHandler;

            _upgradeManager = FindFirstObjectByType<UpgradeManager>();
        }

        private void Update()
        {
            ReadMovementInput();
        }

        private void FixedUpdate()
        {
            float speed = playerData.moveSpeed;
            if (_upgradeManager) speed += _upgradeManager.GetUpgradeValue(UpgradeType.MovementSpeed);

            Move(_movementInput, speed);
            Rotate(_movementInput, playerData.rotationSpeed);
            UpdateAnimation(_movementInput.magnitude);
        }

        private void ReadMovementInput()
        {
            if (_inputHandler != null) _movementInput = _inputHandler.GetMovementInput();
        }
    }
}