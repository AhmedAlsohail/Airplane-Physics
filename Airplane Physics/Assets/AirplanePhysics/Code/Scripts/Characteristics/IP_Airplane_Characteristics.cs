using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Inputs
{
    public class IP_Airplane_Characteristics : MonoBehaviour
    {
        #region Variables Methods
        [Header("Characteristics Properties")]
        [Tooltip("Speed is in mps (Meter Per Second)")]
        public float forwardSpeed; // In meter per second

        [Header("Lift Properties")]
        public float maxLiftPower = 800f;
        public AnimationCurve liftCurve = AnimationCurve.EaseInOut(0f, 0f, 1f, 1f);

        [Header("Drag Properties")]
        public float dragFactor = 0.01f;

        private Rigidbody rb;
        private float startDrag;
        private float startAngularDrag;

        [Tooltip("Max Meters Per Second")]
        public float maxMPS = 49.174f;
        private float normalizeMPS;

        private float angleOfAttack;
        #endregion

        #region BuildIn Methods
        #endregion

        #region Custom Methods
        public void InitCharacteristics(Rigidbody curRB)
        {
            // Basic Initialization
            rb = curRB;
            startDrag = rb.drag;
            startAngularDrag = rb.angularDrag;
        }

        public void UpdateCharacteristics()
        {
            if (rb)
            {
                // Process the Flight Model.
                CalcualteForwardSpeed();
                CalculateLift();
                CalculateDrag();
            }              

        }

        void CalcualteForwardSpeed()
        {
            Vector3 localVelocity = transform.InverseTransformDirection(rb.velocity);
            forwardSpeed = Mathf.Max(0f, localVelocity.z);
            forwardSpeed = Mathf.Clamp(forwardSpeed, 0f, maxMPS);

            normalizeMPS = Mathf.InverseLerp(0f, maxMPS, forwardSpeed);
        }

        void CalculateLift() 
        {
            //Get Angle Of Attack
            angleOfAttack = Vector3.Dot(rb.velocity.normalized, transform.forward); // Calcualte Angle Of Attack
            angleOfAttack *= angleOfAttack; // Square it (^2)
            Debug.Log(angleOfAttack);

            // Create Lift Direction
            Vector3 liftDir = transform.up; // Lift Direction
            float liftPower = liftCurve.Evaluate(normalizeMPS) * maxLiftPower; // Calculate Lift Power
            Debug.Log(liftPower);

            // Apply the final Lift Force to the Rigidbody
            Vector3 finalLiftForce = liftDir * liftPower * angleOfAttack;            
            rb.AddForce(finalLiftForce);
        }

        void CalculateDrag()
        {
            float speedDrag = forwardSpeed * dragFactor;
            float finalDrag = startDrag + speedDrag;

            rb.drag = finalDrag;
            rb.angularDrag = startAngularDrag * forwardSpeed;
        }
        #endregion
    }
}