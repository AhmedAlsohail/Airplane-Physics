using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace Inputs
{
    public static class Airplane_Menus
    {
        [MenuItem("Airplane Tools/Create New Airplane")]
        public static void CreateNewAirplane()
        {
            //Airplane_SetupTools.BuildDefaultAirplane("New Airplane");
            Airplane_Setup_Window.LaunchSetupWindow();
        }
    }
}
