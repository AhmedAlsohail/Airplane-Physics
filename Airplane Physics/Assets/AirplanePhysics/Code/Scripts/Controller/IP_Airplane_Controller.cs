using input;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Inputs
{
    [RequireComponent(typeof(IP_Airplane_Characteristics))]
    public class IP_Airplane_Controller : IP_BaseRigidbody_Controller
    {
        #region Variables
        [Header("Base Airplane Properties")]
        public Airplane_Preset airplanePreset;
        public IP_BaseAirplane_Input input;
        public IP_Airplane_Characteristics characteristics;
        public Transform centerOfGravity;

        [Tooltip("Weight is in Kilograms")]
        public float airplaneWeight = 800f;

        [Header("Engines")]
        public List<IP_Airplane_Engine> engines = new List<IP_Airplane_Engine>();

        [Header("Wheels")]
        public List<IP_Airplane_Wheel> wheels = new List<IP_Airplane_Wheel>();

        [Header("Control Surfaces")]
        public List<IP_Airplane_ControlSurface> controlSurfaces = new List<IP_Airplane_ControlSurface>();
        #endregion

        #region Constants
        #endregion

        #region Built In Methods
        public override void Start()
        {
            GetPresetInfo();
            base.Start();

            if (rb)
            {
                rb.mass = airplaneWeight; // Set plane weight
                if (centerOfGravity)
                {
                    rb.centerOfMass = centerOfGravity.localPosition;
                }

                characteristics = GetComponent<IP_Airplane_Characteristics>();
                if (characteristics)
                {
                    characteristics.InitCharacteristics(rb, input);
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
                HandleCharacteristics();
                HandleControlSurfaces();
                HandleWheel();
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
                        rb.AddForce(engine.CalculateForce(input.StickyThrottle));
                    }
                }
            }
        }

        void HandleCharacteristics()
        {
            if (characteristics)
            {
                characteristics.UpdateCharacteristics();
            }
        }

        void HandleControlSurfaces()
        {
            if (controlSurfaces.Count > 0)
            {
                foreach(IP_Airplane_ControlSurface controlSurface in controlSurfaces)
                {
                    controlSurface.HandleControlSurface(input);
                }
            }
        }

        void HandleWheel()
        {
            if(wheels.Count > 0)
            {
                foreach(IP_Airplane_Wheel wheel in wheels)
                {
                    wheel.UpdateWheel(input);
                }
            }
        }

        void HandleAltitude()
        {

        }

        void GetPresetInfo()
        {
            if (airplanePreset)
            {
                airplaneWeight = airplanePreset.airplaneWeight;
                centerOfGravity.localPosition = airplanePreset.cogPosition;

                if (characteristics)
                {
                    characteristics.dragFactor = airplanePreset.dragFactor;
                    characteristics.flapDragFactor= airplanePreset.flapDragFactor;
                    characteristics.liftCurve= airplanePreset.liftCurve;
                    characteristics.maxLiftPower= airplanePreset.maxLiftPower;
                    characteristics.maxMPS= airplanePreset.maxMPS;
                    characteristics.rbLerpSpeed= airplanePreset.rbLerpSpeed;
                    characteristics.rollTorque= airplanePreset.rollTorque;
                    characteristics.pitchTorque= airplanePreset.pitchTorque;
                    characteristics.yawTorque= airplanePreset.yawTorque;
                }
            }
        }
        #endregion
    }
}