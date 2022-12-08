using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Inputs
{
    // Initiate if not already existed
    [RequireComponent(typeof(Rigidbody))]
    [RequireComponent(typeof(AudioSource))]
    public class IP_BaseRigidbody_Controller : MonoBehaviour
    {
        #region Variables
        protected Rigidbody rb;
        protected AudioSource aSource;
        #endregion

        #region Built in Methods
        public virtual void Start()
        {
            rb = GetComponent<Rigidbody>();
            aSource = GetComponent<AudioSource>();

            if (aSource) // if there is aSource
            {
                aSource.playOnAwake = false; // Turn off playOnAwake Automaticlly
            }
        }

        void FixedUpdate()
        {
            if (rb)
            {
                HandlePhysics();
            }
        }
        #endregion

        #region Custom Methods
        protected virtual void HandlePhysics(){}
        #endregion
    }
}
