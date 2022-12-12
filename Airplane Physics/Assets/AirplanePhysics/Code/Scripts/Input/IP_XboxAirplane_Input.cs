using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Inputs
{   
    public class IP_XboxAirplane_Input : IP_BaseAirplane_Input
    {
        #region Variables     
        #endregion

        #region Custom Methods
        protected override void HandleInput()
        {
            base.HandleInput();
            // Process Main Control Inputs
            pitch += Input.GetAxis("Vertical");
            roll += Input.GetAxis("Horizontal");
            yaw += Input.GetAxis("Xbox_RH_Stick");
            throttle += Input.GetAxis("Xbox_RV_Stick");
            StickyThrottleControl();
            // Process Brake Inputs
            brake += Input.GetAxis("Fire1");

            // Process Flaps Inputs
            if (Input.GetButtonDown("Xbox_R_Bumper"))
            {
                flaps += 1;
            }

            if (Input.GetButtonDown("Xbox_L_Bumper"))
            {
                flaps -= 1;
            }

            flaps = Mathf.Clamp(flaps, 0, maxFlapIncrements);
        }
    }
    #endregion
}