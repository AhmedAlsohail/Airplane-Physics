using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace Inputs
{
    public class IP_BaseAirplane_Input : MonoBehaviour
    {
        #region Variables
        protected float pitch = 0f;
        protected float roll = 0f;
        protected float yaw = 0f;
        protected float throttle = 0f;
        

        public KeyCode brakeKey = KeyCode.LeftShift;
        protected float brake = 0;

        protected int flaps = 0;
        public int maxFlapIncrements = 2;
        #endregion

        #region Properties
        public float Pitch
        {
            get { return pitch; }
        }
        public float Roll
        {
            get { return roll; }
        }
        public float Yaw
        {
            get { return yaw; }
        }
        public float Throttle
        {
            get { return throttle; }
        }
        public int Flaps
        {
            get { return flaps; }
        }
        public float Brake
        {
            get { return brake; }
        }
        #endregion

        #region Builtin Methods
        void Start()
        {
            
        }

        void Update()
        {
            HandleInput();
        }
        #endregion


        #region Custom Methods
        protected virtual void HandleInput()
        {
            // Process Main Control Inputs
            pitch = Input.GetAxis("Vertical");
            roll = Input.GetAxis("Horizontal");
            yaw = Input.GetAxis("Yaw");
            throttle = Input.GetAxis("Throttle");

            // Process Brake Inputs
            brake = Input.GetKey(brakeKey) ? 1f : 0f;

            // Process Flaps Inputs
            if (Input.GetKeyDown(KeyCode.F))
            {
                flaps += 1;
            }

            if (Input.GetKeyDown(KeyCode.G))
            {
                flaps -= 1;
            }

            flaps = Mathf.Clamp(flaps, 0, maxFlapIncrements);
        }
        #endregion
    }
}
