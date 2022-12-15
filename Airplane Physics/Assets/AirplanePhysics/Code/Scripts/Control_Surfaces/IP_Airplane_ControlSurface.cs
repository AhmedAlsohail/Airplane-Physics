using Inputs;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace input {
    public enum ControlSurfaceType
    {
        Rudder,
        Elevator,
        Flap,
        Aleron
    }
    public class IP_Airplane_ControlSurface : MonoBehaviour
    {
        #region Variables  
        [Header("Control Surfaces Properties")]
        public ControlSurfaceType type = ControlSurfaceType.Rudder;
        public float maxAngle = 30f;
        public Vector3 axis = Vector3.right;
        public Transform controlSurfaceGraphic;
        public float smoothSpeed = 2f;
        private float wantedAngle;
        
        #endregion

        #region BuiltIn Methods
        void Start()
        {
        }

        void FixedUpdate()
        {
            if (controlSurfaceGraphic)
            {
                Vector3 finalAngleAxis = axis * wantedAngle;
                controlSurfaceGraphic.localRotation = Quaternion.Slerp(controlSurfaceGraphic.localRotation, Quaternion.Euler(finalAngleAxis), Time.deltaTime * smoothSpeed);
            }
        }

        #endregion

        #region Custom Methods
        public void HandleControlSurface(IP_BaseAirplane_Input input)
        {
            float inputValue = 0f;
            switch (type)
            {
                case ControlSurfaceType.Rudder:
                    inputValue = input.Yaw; 
                    break;

                    case ControlSurfaceType.Elevator:
                    inputValue = input.Pitch;
                    break;

                    case ControlSurfaceType.Flap:
                    inputValue = input.Flaps;
                    break;

                    case ControlSurfaceType.Aleron:
                    inputValue = input.Roll;
                    break;

                    default: break;
            }

            wantedAngle = inputValue * maxAngle;
        }
        #endregion

    }
}