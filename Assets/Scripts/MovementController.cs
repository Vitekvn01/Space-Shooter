
using SpaceShooter;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceShooter
{
    public class MovementController : MonoBehaviour
    {
        public enum ControlMode
        {
            Keyboard,
            Mobile
        }



        [SerializeField] private ControlMode _controlMode;

        /*  public void Construct(VirtualGamePad virtualGamePad)
          {
              _mobilelJoystick = virtualGamePad;
          }*/


        [SerializeField] private SpaceShip _targetShip;
        [SerializeField] private VirtualJoystick _mobilelJoystick;

        [SerializeField] private PointerClickHold _mobileFirePrimary;
        [SerializeField] private PointerClickHold _mobileFireSecondary;


        private void Start()
        {
            if (Application.isMobilePlatform)
            {
                _controlMode = ControlMode.Mobile;
                _mobilelJoystick.gameObject.SetActive(true);
            }
            else
            {
                if (_controlMode == ControlMode.Keyboard)
                {
                    _mobilelJoystick.gameObject.SetActive(false);
                    _mobileFirePrimary.gameObject.SetActive(false);
                    _mobileFireSecondary.gameObject.SetActive(false);
                }
                else
                {
                    _mobilelJoystick.gameObject.SetActive(true);
                }
            }
        }

        private void Update()
        {
            if (_targetShip == null) return;

            if (_controlMode == ControlMode.Keyboard)
                ControlKeyboard();

            if (_controlMode == ControlMode.Mobile)
                ControlMobile();
        }

        private void ControlMobile()
        {
            Vector3 dir = _mobilelJoystick.Value;

            _targetShip.ThrustControl = dir.y;
            _targetShip.TorqueControl = -dir.x;


            if (_mobileFirePrimary.IsHold == true)
            {
                _targetShip.Fire(TurretMode.Primary);
            }
            if (_mobileFireSecondary.IsHold == true)
            {
                _targetShip.Fire(TurretMode.Secondary);
            }


        }

        private void ControlKeyboard()
        {
            float thrust = Input.GetAxisRaw("Vertical");
            float torque = -Input.GetAxisRaw("Horizontal");

            _targetShip.ThrustControl = thrust;
            _targetShip.TorqueControl = torque;

            if (Input.GetKey(KeyCode.Space))
            {
                _targetShip.Fire(TurretMode.Primary);
            }

            if (Input.GetKey(KeyCode.LeftShift))
            {
                _targetShip.Fire(TurretMode.Secondary);
            }

        }

        public void SetTargetShip(SpaceShip ship)
        {
            _targetShip = ship;
        }
    }
}

