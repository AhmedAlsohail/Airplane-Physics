using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Inputs
{
    [CreateAssetMenu(menuName ="IP/Create Airplane Preset")]
    public class Airplane_Preset : ScriptableObject
    {
        #region Controller Properties
        [Header("Controller Properties")]
        public Vector3 cogPosition;
        public float airplaneWeight;
        #endregion

        #region Characteristics Properties
        [Header("Characteristics Properties")]
        public float maxMPS;

        public float rbLerpSpeed;

        public float maxLiftPower;
        public AnimationCurve liftCurve;

        public float dragFactor;
        public float flapDragFactor;

        public float pitchTorque;

        public float rollTorque;
        public float yawTorque;
        #endregion
    }
}