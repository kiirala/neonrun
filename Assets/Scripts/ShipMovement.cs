using System.Collections;
using System.Collections.Generic;
using NeonRun.Input;
using UnityEngine;
using UnityEngine.InputSystem;

namespace NeonRun
{

    public class ShipMovement : MonoBehaviour, ShipControls.IShipActions
    {
        private ShipControls controls;
        private ShipController controller;

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
    }
}
