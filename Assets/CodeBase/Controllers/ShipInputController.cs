
using Common;
using UnityEngine;

namespace SpaceShooter
{
    public class ShipInputController : MonoBehaviour
    {
        public enum ControlMode
        {
            Keyboard,
            Mobile
        }



        [SerializeField] private ControlMode _controlMode;

        public void Construct(VirtualGamePad virtualGamePad)
        {
            _virtualGamePad = virtualGamePad;
        }


        private VirtualGamePad _virtualGamePad;

        private VirtualJoystick _mobilelJoystick;
        private PointerClickHold _mobileFirePrimary;
        private PointerClickHold _mobileFireSecondary;

        private SpaceShip _targetShip;

        private void Start()
        {
            if (Application.isMobilePlatform)
            {
                _controlMode = ControlMode.Mobile;
                _virtualGamePad.VirtualJoystick.gameObject.SetActive(true);
            }
            else
            {
                if (_controlMode == ControlMode.Keyboard)
                {
                    _virtualGamePad.VirtualJoystick.gameObject.SetActive(false);
                    _virtualGamePad.MobileFirePrimary.gameObject.SetActive(false);
                    _virtualGamePad.MobileFireSecondary.gameObject.SetActive(false);
                }
                else
                {
                    _virtualGamePad.VirtualJoystick.gameObject.SetActive(true);
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

