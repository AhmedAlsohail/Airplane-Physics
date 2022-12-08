using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Inputs
{
    public class IP_Airplane_Controller : IP_BaseRigidbody_Controller
    {
        #region Variables
        [Header("Base Airplane Properties")]
        public IP_BaseAirplane_Input input;
        public Transform centerOfGravity;

        [Tooltip("Weight is in Kilograms")]
        public float airplaneWeight = 800f;

        [Header("Engines")]
        public List<IP_Airplane_Engine> engines = new List<IP_Airplane_Engine>();

        [Header("Wheels")]
        public List<IP_Airplane_Wheel> wheels = new List<IP_Airplane_Wheel>();
        #endregion

        #region Constants
        #endregion

        #region Built In Methods
        public override void Start()
        {
            base.Start();

            if (rb)
            {
                rb.mass = airplaneWeight;
                if (centerOfGravity)
                {
                    rb.centerOfMass = centerOfGravity.localPosition;
                }
            }

            if(wheels != null)
            {
                if(wheels.Count > 0)
                {
                    foreach (IP_Airplane_Wheel wheel in wheels)
                    {
                        wheel.InitWheel();
                    }
                }
            }
        }
        #endregion


        #region Custom Methods
        protected override void HandlePhysics()
        {
            if (input)
            {
                HandleEngine();
                HandleAerodynamic();
                HandleSteering();
                HandleBreaks();
                HandleAltitude();
            }            
        }

        void HandleEngine()
        {
            if(engines != null)
            {
                if(engines.Count > 0)
                {
                    foreach (IP_Airplane_Engine engine in engines)
                    {
                        rb.AddForce(engine.CalculateForce(input.Throttle));
                    }
                }
            }
        }

        void HandleAerodynamic()
        {

        }

        void HandleSteering()
        {

        }

        void HandleBreaks()
        {

        }

        void HandleAltitude()
        {

        }
        #endregion
    }
}