using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace Inputs
{
    public static class IP_Airplane_Menus
    {
        [MenuItem("Airplane Tools/Create New Airplane")]
        public static void CreateNewAirplane()
        {
            GameObject curSelected = Selection.activeGameObject; //  cur = current
            if (curSelected)
            {
                // Add the script
                IP_Airplane_Controller curController = curSelected.AddComponent<IP_Airplane_Controller>();
                Debug.Log("A new airplane has been created!");

                // Create Center of Gravity
                GameObject curCOG = new GameObject("Center Of Gravity");
                // Make it a child to the plane
                curCOG.transform.SetParent(curSelected.transform);
                // And assign it to the script
                curController.centerOfGravity = curCOG.transform;
            }
            else
            {
                Debug.Log("Failed, please select a GameObject.");
            }
            
        }
    }
}
