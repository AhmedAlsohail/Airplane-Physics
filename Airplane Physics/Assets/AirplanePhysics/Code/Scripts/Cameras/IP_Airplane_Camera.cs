using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace input
{
    public class IP_Airplane_Camera : IP_Basic_Follow_Camera
    {
        #region Variables     
        [Header("Airplane Camera Properties")]
        public float minHeightFromGround = 2f;
        #endregion

        #region Custom Methods
        protected override void handleCamera()
        {
            // Airplane camera code
            RaycastHit hit;
            if (Physics.Raycast(transform.position, Vector3.down, out hit))
            {
                if(hit.distance < minHeightFromGround && hit.transform.tag == "ground")
                {
                    float wantedHeight = origHeight + (minHeightFromGround - hit.distance);
                    height = wantedHeight;
                }
            }
            base.handleCamera ();
        }
        #endregion

    }
}