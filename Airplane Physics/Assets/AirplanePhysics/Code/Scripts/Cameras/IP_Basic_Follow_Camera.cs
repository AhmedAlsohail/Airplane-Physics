using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace input
{
    public class IP_Basic_Follow_Camera : MonoBehaviour
    {
        #region Variables  
        [Header("Basic follow camera properties")]
        public Transform target;
        public float distance = 5f;
        public float height = 2f;
        [Tooltip("How smooth is the camera movement")]
        public float smoothSpeed = 0.5f;

        private Vector3 smoothVelocity; // Refrence variable
        protected float origHeight;
        #endregion

        #region BuiltIn Methods
        void Start()
        {
            origHeight = height;
        }

        void FixedUpdate()
        {
            if (target)
            {
                handleCamera();
            }            
        }

        #endregion

        #region Custom Methods
        protected virtual void handleCamera()
        {
            Vector3 wantedPosition = target.position + (-target.forward * distance) + (Vector3.up * height);
            Debug.DrawLine(target.position, wantedPosition, Color.blue );

            transform.position = Vector3.SmoothDamp(transform.position, wantedPosition, ref smoothVelocity, smoothSpeed);
            transform.LookAt(target);
        }
        #endregion

    }
}