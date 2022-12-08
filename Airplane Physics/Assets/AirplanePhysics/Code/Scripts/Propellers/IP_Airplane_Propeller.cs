using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Inputs
{
    public class IP_Airplane_Propeller : MonoBehaviour
    {
        #region Variables Methods

        #endregion

        #region BuildIn Methods
        void Start()
        {

        }

        void Update()
        {

        }
        #endregion

        #region Custom Methods
        public void HandlePropeller(float currentRPM)
        {
            // Get our degrees per second
            float dps = ((currentRPM * 360f) / 60f) * Time.deltaTime;
            transform.Rotate(Vector3.forward, dps);
        }
        #endregion
    }
}