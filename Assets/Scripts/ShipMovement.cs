using NeonRun.Input;
using UnityEngine;
using UnityEngine.InputSystem;

namespace NeonRun
{

    public class ShipMovement : MonoBehaviour, ShipControls.IShipActions
    {
        private ShipControls controls;
        private ShipController controller;
        private CommonGameState state;
        private BombController bombController;

        public void OnEnable()
        {
            if (controls is null)
            {
                controls = new();
                controls.Ship.SetCallbacks(this);
            }
            controls.Ship.Enable();
        }

        public void OnDisable()
        {
            controls.Ship.Disable();
        }

        // Start is called before the first frame update
        void Start()
        {
            controller = GetComponent<ShipController>();
            state = GetComponentInParent<CommonGameState>();
            bombController = GetComponentInParent<BombController>();
        }

        // Update is called once per frame
        void Update()
        {

        }

        public void OnMove(InputAction.CallbackContext context)
        {
            if (!context.performed) return;
            var value = context.ReadValue<float>();
            controller.ChangeLane((int)value);
        }

        public void OnBomb(InputAction.CallbackContext context)
        {
            if (!context.performed) return;
            bombController.TriggerBomb();
        }

        public void OnFocus(InputAction.CallbackContext context)
        {
            if (context.started) state.Focused = true;
            if (context.canceled) state.Focused = false;
        }
    }
}
