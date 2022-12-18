using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace Inputs
{
    public class Airplane_Setup_Window: EditorWindow
    {
        #region Variables
        private string wantedName;
        #endregion

        #region BuiltIn Methods
        public static void LaunchSetupWindow()
        {
            Airplane_Setup_Window.GetWindow(typeof(Airplane_Setup_Window), true, "Airplane Setup").Show();
        }

        private void OnGUI()
        {
            wantedName = EditorGUILayout.TextField("Airplane Name:", wantedName);
            if(GUILayout.Button("Create new Airplane"))
            {
                Airplane_SetupTools.BuildDefaultAirplane(wantedName);
                Airplane_Setup_Window.GetWindow<Airplane_Setup_Window>().Close();
            }
        }
        #endregion
    }
}
