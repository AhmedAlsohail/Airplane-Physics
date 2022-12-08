using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Inputs
{
    public class IP_Airplane_Engine : MonoBehaviour
    {
        #region Variables
        public float maxForce = 200f;
        public float maxRPM = 2550f;

        public AnimationCurve powerCurve = AnimationCurve.Linear(0f, 0f, 1f, 1f);
        #endregion

        #region BuildIn Methods

        #endregion

        #region Cusotm Methods
        public Vector3 CalculateForce(float throttle)
        {            
            float finalThrottle = Mathf.Clamp01(throttle); // Set Throttle between 0 & 1.
            finalThrottle = powerCurve.Evaluate(finalThrottle); // Convert to the power curve.
            float finalPower = finalThrottle * maxForce; // Max Power is when Throttle = 1.
            Vector3 finalForce = transform.forward * finalPower; // Final Vector3 force.
   
            return finalForce;
        }
        #endregion
    }
}