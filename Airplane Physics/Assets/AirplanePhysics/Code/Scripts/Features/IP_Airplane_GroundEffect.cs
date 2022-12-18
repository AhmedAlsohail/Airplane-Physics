using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Inputs
{
    public class IP_Airplane_GroundEffect : MonoBehaviour
    {
        #region Variables
        public float maxGroundDistance = 3f; // 3 meters
        public float liftForce = 100f;
        public float maxSpeed = 15f;
        private Rigidbody rb;
        #endregion

        #region BuiltIn Methods
        void Start()
        {
            rb = GetComponent<Rigidbody>();
        }

        void FixedUpdate()
        {
            if (rb)
            {
                HandleGroudnEffect();
            }
        }
        #endregion

        #region Custom Methods
        protected virtual void HandleGroudnEffect()
        {
            RaycastHit hit;
            Vector3 curPos = new Vector3(transform.position.x, transform.position.y - 1, transform.position.z);
            if (Physics.Raycast(curPos, Vector3.down, out hit))
            {   
                if(hit.transform.tag == "ground" && hit.distance + 1 < maxGroundDistance) // We need to add 1 to the hit.distance because the curPos.y is lowered by 1, to prevent colliding with the plane itself.
                {
                    Debug.Log(rb.velocity);
                    float currentSpeed = rb.velocity.magnitude;
                    float normalizedSpeed = currentSpeed / maxSpeed;

                    float distance = maxGroundDistance - hit.distance + 1;
                    float finalForce = liftForce * distance * normalizedSpeed;
                    rb.AddForce(Vector3.up * finalForce);
                }
            }
        }
        #endregion
    }
}