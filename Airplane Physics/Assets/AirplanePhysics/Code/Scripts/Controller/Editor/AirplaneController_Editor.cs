using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditor.TerrainTools;
using System.Linq;
using input;

namespace Inputs
{
    [CustomEditor(typeof(IP_Airplane_Controller))]
    public class AirplaneController_Editor : Editor
    {
        #region Variabless
        private IP_Airplane_Controller targetController;
        #endregion

        #region BuiltIn Methods
        void OnEnable()
        {
            targetController = (IP_Airplane_Controller)target;
        }

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            GUILayout.Space(15);
            if (GUILayout.Button("Get Airplane Components", GUILayout.Height(35)))
            {
                
                // Find All Engines
                targetController.engines.Clear();
                targetController.engines = FindAllEngines().ToList<IP_Airplane_Engine>();
                // Find All Wheels
                targetController.wheels.Clear();
                targetController.wheels = FindAllWheels().ToList<IP_Airplane_Wheel>();
                // Find All Control Surfaces
                targetController.controlSurfaces.Clear();
                targetController.controlSurfaces = FindAlControlSurfaces().ToList<IP_Airplane_ControlSurface>();
            }
            GUILayout.Space(15);
            if (GUILayout.Button("Create Airplane Preset", GUILayout.Height(35)))
            {
                string filePath = EditorUtility.SaveFilePanel("Save Airplane Preset", "Assets", "New_Airplane_Preset", "asset");
                saveAirplanePreset(filePath);
            }
        }
        #endregion

        #region Custom Methods
        IP_Airplane_Engine[] FindAllEngines()
        {
            IP_Airplane_Engine[] engines = new IP_Airplane_Engine[0];
            if (targetController)
            {
                engines = targetController.transform.GetComponentsInChildren<IP_Airplane_Engine>(true);
            }

            return engines;
        }

        IP_Airplane_Wheel[] FindAllWheels()
        {
            IP_Airplane_Wheel[] wheels = new IP_Airplane_Wheel[0];
            if (targetController)
            {
                wheels = targetController.transform.GetComponentsInChildren<IP_Airplane_Wheel>(true);
            }

            return wheels;
        }

        IP_Airplane_ControlSurface[] FindAlControlSurfaces()
        {
            IP_Airplane_ControlSurface[] controlSurfaces = new IP_Airplane_ControlSurface[0];
            if (targetController)
            {
                controlSurfaces = targetController.transform.GetComponentsInChildren<IP_Airplane_ControlSurface>(true);
            }

            return controlSurfaces;
        }

        void saveAirplanePreset(string path)
        {
            if(targetController && !string.IsNullOrEmpty(path))
            {
                string appPath = Application.dataPath; // Make the path start In the assets folder.
                string finalPath = "Assets" + path.Substring(appPath.Length); // Add the assets folder

                // Create new Preset
                Airplane_Preset newPreset = ScriptableObject.CreateInstance<Airplane_Preset>();
                newPreset.airplaneWeight = targetController.airplaneWeight;
                if (targetController.centerOfGravity)
                {
                    newPreset.cogPosition = targetController.centerOfGravity.localPosition;
                }
                Debug.Log("test1");

                if (targetController.characteristics)
                {
                    Debug.Log("test2");
                    newPreset.dragFactor = targetController.characteristics.dragFactor;
                    newPreset.flapDragFactor = targetController.characteristics.flapDragFactor;
                    newPreset.maxMPS = targetController.characteristics.maxMPS;
                    newPreset.rbLerpSpeed = targetController.characteristics.rbLerpSpeed;
                    newPreset.maxLiftPower = targetController.characteristics.maxLiftPower;
                    newPreset.liftCurve = targetController.characteristics.liftCurve;
                    newPreset.pitchTorque = targetController.characteristics.pitchTorque;
                    newPreset.rollTorque = targetController.characteristics.rollTorque;
                    newPreset.yawTorque = targetController.characteristics.yawTorque;
                }

                // Create Final Preset
                AssetDatabase.CreateAsset(newPreset, finalPath);
            }
        }
        #endregion
    }
}
