using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Inputs
{
    [RequireComponent(typeof(WheelCollider))]
    public class IP_Airplane_Wheel : MonoBehaviour
    {
        #region Variables
        [Header("Wheel Properties")]
        public Transform wheelGraphic;
        public bool isBraking = false;
        public float brakePower = 5f;
        public bool isSteering = false;
        public float steerAngle = 20f;
        public float steerSmoothSpeed = 2f;

        private WheelCollider WheelCol;
        private Vector3 worldPos;
        private Quaternion worldRot;
        private float finalBrakeForce;
         private float finalSteeringAngle;
        #endregion

        #region BuiltInMethods
        void Start()
        {
            WheelCol = GetComponent<WheelCollider>();
        }
        #endregion

        #region Custom Methods
        public void InitWheel()
        {
            if (WheelCol)
            {
                WheelCol.motorTorque = 0.000000000001f; // Unlock Wheels
            }
        }

        public void UpdateWheel(IP_BaseAirplane_Input input)
        {
            if (WheelCol)
            {
                WheelCol.GetWorldPose(out worldPos, out worldRot);
                if (wheelGraphic)
                {
                    wheelGraphic.rotation = worldRot;
                    wheelGraphic.position = worldPos;
                }

                if(isBraking)
                {
                    if (input.Brake > 0.1f)
                    {
                        finalBrakeForce = Mathf.Lerp(finalBrakeForce, input.Brake * brakePower, Time.deltaTime);
                        WheelCol.brakeTorque = finalBrakeForce;
                    }
                    else
                    {
                        finalBrakeForce = 0;
                        WheelCol.brakeTorque = 0f;
                        WheelCol.motorTorque = 0.000000000001f; // Unlock Wheels
                    }
                }

                if (isSteering)
                {
                    finalSteeringAngle = Mathf.Lerp(finalSteeringAngle, -input.Yaw * steerAngle, Time.deltaTime * steerSmoothSpeed);
                    WheelCol.steerAngle = finalSteeringAngle;
                }

            }
        }
        #endregion
    }
}
