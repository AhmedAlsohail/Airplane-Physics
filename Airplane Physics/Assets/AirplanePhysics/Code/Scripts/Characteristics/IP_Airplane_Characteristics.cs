using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Inputs
{
    public class IP_Airplane_Characteristics : MonoBehaviour
    {
        #region Variables Methods
        [Header("Characteristics Properties")]
        [Tooltip("Max Speed in Meter/Second")]
        public float maxMPS = 49.174f;

        public float rbLerpSpeed = 0.01f;

        [Header("Lift Properties")]
        public float maxLiftPower = 800f;
        public AnimationCurve liftCurve = AnimationCurve.EaseInOut(0f, 0f, 1f, 1f);

        [Header("Drag Properties")]
        public float dragFactor = 0.01f;
        public float flapDragFactor = 0.005f;

        [Header("Control Properties")]
        [Tooltip("Unit: Newton.meter (N.m)")]
        public float pitchTorque = 1000f;
        [Tooltip("Unit: Newton.meter (N.m)")]
        public float rollTorque = 1000f;
        [Tooltip("Unit: Newton.meter (N.m)")]
        public float yawTorque = 1000f;

        [Tooltip("Current Speed in meter/second")]
        private float forwardSpeed; // In meter per second
        public float ForwardSpeed
        {
            get { return forwardSpeed; }
        }

        private IP_BaseAirplane_Input input;
        private Rigidbody rb;
        private float startDrag;
        private float startAngularDrag;
        
        private float normalizeMPS;

        private float angleOfAttack;
        private float pitchAngle;
        private float rollAngle;




        #endregion

        #region BuildIn Methods
        #endregion

        #region Custom Methods
        public void InitCharacteristics(Rigidbody curRB, IP_BaseAirplane_Input curInput)
        {
            // Basic Initialization
            input = curInput;
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

                // Process Controls
                handlePitch();
                handleRoll();
                handleYaw();
                handleBanking();

                // Handle Rigidbody
                HandleRigidbodyTransform();
            }              

        }

        void CalcualteForwardSpeed()
        {
            // Calculate forward velocity (in MPS)
            Vector3 localVelocity = transform.InverseTransformDirection(rb.velocity); //Transform the Rigidbody velocity vector from world space to local space
            forwardSpeed = Mathf.Max(0f, localVelocity.z); // Take the z-axis, and forward speed shouldn't be > 0.
            forwardSpeed = Mathf.Clamp(forwardSpeed, 0f, maxMPS);
            Debug.Log(forwardSpeed);
            normalizeMPS = Mathf.InverseLerp(0f, maxMPS, forwardSpeed); // Normalize.
        }

        void CalculateLift() 
        {
            //Get Angle Of Attack
            angleOfAttack = Vector3.Dot(rb.velocity.normalized, transform.forward); // Calcualte Angle Of Attack.
            angleOfAttack *= angleOfAttack; // Make it squared (Power of 2).

            // Create Lift Direction
            Vector3 liftDir = transform.up; // Lift Direction
            float liftPower = liftCurve.Evaluate(normalizeMPS) * maxLiftPower; // Calculate Lift Power.

            // Apply the final Lift Force to the Rigidbody
            Vector3 finalLiftForce = liftDir * liftPower * angleOfAttack; // Lift Direction: to determine direction, Lift Power: The power the plane will be lifted, Angle Of Attack: The angle effect on the lift.
            rb.AddForce(finalLiftForce); // Apply force
        }

        void CalculateDrag()
        {
            // Calculate & apply drag.
            float speedDrag = forwardSpeed * dragFactor;
            // Flap Drag
            float flapDrag = input.Flaps * flapDragFactor;
            // Add it all together
            float finalDrag = startDrag + speedDrag + flapDrag;

            rb.drag = finalDrag;

            // Calculate & apply angular drag.
            float finalAngularDrag = startAngularDrag * forwardSpeed;
            rb.angularDrag = finalAngularDrag;
        }

        void HandleRigidbodyTransform()
        {
            if (rb.velocity.magnitude > 1f)
            {
                Vector3 updatedVelocity = Vector3.Lerp(rb.velocity, transform.forward * forwardSpeed, forwardSpeed * angleOfAttack * Time.deltaTime * rbLerpSpeed);
                rb.velocity = updatedVelocity;


                Quaternion updatedRotation = Quaternion.Slerp(rb.rotation, Quaternion.LookRotation(rb.velocity, transform.up), Time.deltaTime * rbLerpSpeed);
                rb.MoveRotation(updatedRotation);
            }
        }

        void handlePitch()
        {
            // Get flat forward (World forward)
            Vector3 flatForward = transform.forward; // Store transform.forward
            flatForward.y = 0f; // Make y-axis = 0
            flatForward = flatForward.normalized;

            // now we can find the angle between the plane forward, and flat forward.
            pitchAngle = Vector3.Angle(transform.forward, flatForward);

            // Rotate plane around its x-axis
            Vector3 AppliedPitchTorque = input.Pitch * pitchTorque * transform.right;
            rb.AddTorque(AppliedPitchTorque);
        }

        void handleRoll()
        {
            // Get flat forward (World right)
            Vector3 flatRight = transform.right;
            flatRight.y = 0f;
            flatRight = flatRight.normalized;
            // now we can find the angle between the plane right, and flat forward.
            rollAngle = Vector3.SignedAngle(transform.right, flatRight, transform.forward);

            Vector3 AppliedRollTroque = -input.Roll * rollTorque * transform.forward;
            rb.AddTorque(AppliedRollTroque);
        }

        void handleYaw()
        {
            Vector3 AppliedYawTorque = input.Yaw * yawTorque * transform.up;
            rb.AddTorque(AppliedYawTorque);
        }

        void handleBanking()
        {
            float bankSide = Mathf.InverseLerp(-90f, 90f, rollAngle);
            float bankAmount = Mathf.Lerp(-1f, 1f, bankSide);
            Vector3 bankTorque = bankAmount * rollTorque * transform.up;
            rb.AddTorque(bankTorque);
        }
        #endregion
    }
}