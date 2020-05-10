using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class BuildiOS : MonoBehaviour
{
    [MenuItem("Build/Build iOS")]
    public static void ExportXcodeProject()
    {
        EditorUserBuildSettings.SwitchActiveBuildTarget(BuildTargetGroup.iOS, BuildTarget.iOS);

        EditorUserBuildSettings.symlinkLibraries = true;
        EditorUserBuildSettings.development = true;
        EditorUserBuildSettings.allowDebugging = true;

        List<string> scenes = new List<string>();
        for (int i = 0; i < EditorBuildSettings.scenes.Length; i++)
        {
            if (EditorBuildSettings.scenes[i].enabled)
            {
                scenes.Add(EditorBuildSettings.scenes[i].path);
            }
        }

        BuildPipeline.BuildPlayer(scenes.ToArray(), "Build\\iOS\\UnityGitlabTestiOSProj", BuildTarget.iOS, BuildOptions.None);
    }
}