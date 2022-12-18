using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace Inputs
{
    public static class Airplane_SetupTools
    {
        public static void BuildDefaultAirplane(string name)
        {
            // Create the root Game Objcet
            GameObject rootGO = new GameObject(name, typeof(IP_Airplane_Controller), typeof(IP_BaseAirplane_Input));
            // Create Center Of Gravity
            GameObject cogGO = new GameObject("COG");
            cogGO.transform.SetParent(rootGO.transform, false);

            // Create the Base Components or Find them
            IP_BaseAirplane_Input input = rootGO.GetComponent<IP_BaseAirplane_Input>();
            IP_Airplane_Controller controller = rootGO.GetComponent<IP_Airplane_Controller>();
            IP_Airplane_Characteristics characteristics = rootGO.GetComponent<IP_Airplane_Characteristics>();
            // Seth the Base Components
            if (controller)
            {
                controller.input = input;
                controller.characteristics = characteristics;
                controller.centerOfGravity = cogGO.transform;

                GameObject graphicGroup = new GameObject("Graphic_GRP");
                GameObject collisionGroup = new GameObject("Collision_GRP");
                GameObject controlSurfaceGroup = new GameObject("ControlSurfaces_GRP");

                graphicGroup.transform.SetParent(rootGO.transform, false);
                collisionGroup.transform.SetParent(rootGO.transform, false);
                controlSurfaceGroup.transform.SetParent(rootGO.transform, false);

                // Create First Engine
                GameObject engineGO = new GameObject("Engine", typeof(IP_Airplane_Engine));
                IP_Airplane_Engine engine = engineGO.GetComponent<IP_Airplane_Engine>();
                controller.engines.Add(engine);
                engineGO.transform.SetParent(rootGO.transform, false);

                // Create the base Airplane
                GameObject defaultAirplaneModel = (GameObject)AssetDatabase.LoadAssetAtPath("Assets/Premade-Assets/IndiePixel_AirplanesOld/Indie-Pixel_Airplane/IndiePixel_Airplane.fbx", typeof(GameObject));
                if (defaultAirplaneModel)
                {
                    GameObject.Instantiate(defaultAirplaneModel, graphicGroup.transform);
                }
            }

            // Select the new Airplane setup.
            Selection.activeGameObject = rootGO;

        }
    }
}