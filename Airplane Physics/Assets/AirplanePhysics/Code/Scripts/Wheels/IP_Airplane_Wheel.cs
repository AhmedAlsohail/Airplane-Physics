using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Inputs
{
    [RequireComponent(typeof(WheelCollider))]
    public class IP_Airplane_Wheel : MonoBehaviour
    {
        #region Variables
        private WheelCollider WheelCol;
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
        #endregion
    }
}
