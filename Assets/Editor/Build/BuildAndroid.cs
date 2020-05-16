using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace Assets.Editor.Build
{
    public class BuildAndroid : MonoBehaviour 
    {
        [MenuItem("Build/Build Android")]
        public static void ExportAndroidStudioProject()
        {
            EditorUserBuildSettings.SwitchActiveBuildTarget(BuildTargetGroup.Android, BuildTarget.Android);

            EditorUserBuildSettings.symlinkLibraries = true;
            EditorUserBuildSettings.development = false;
            EditorUserBuildSettings.allowDebugging = false;
            EditorUserBuildSettings.exportAsGoogleAndroidProject = true;

            List<string> scenes = new List<string>();
            for (int i = 0; i < EditorBuildSettings.scenes.Length; i++)
            {
                if (EditorBuildSettings.scenes[i].enabled)
                {
                    scenes.Add(EditorBuildSettings.scenes[i].path);
                }
            }

            BuildPipeline.BuildPlayer(scenes.ToArray(), "Build\\Android\\PPOP_ChallengeProject", BuildTarget.Android, BuildOptions.None);
        }
    }
}