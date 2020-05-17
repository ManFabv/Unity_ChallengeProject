using System.Collections.Generic;
using UnityEditor;
using UnityEditor.Build.Reporting;
using UnityEngine;

public class BuildAndroid : MonoBehaviour 
{
    [MenuItem("Build/Build Android")]
    public static void ExportAndroidStudioProject()
    {
        EditorUserBuildSettings.SwitchActiveBuildTarget(BuildTargetGroup.Android, BuildTarget.Android);

        EditorUserBuildSettings.symlinkLibraries = true;
        EditorUserBuildSettings.development = true;
        EditorUserBuildSettings.allowDebugging = true;
        EditorUserBuildSettings.exportAsGoogleAndroidProject = true;

        List<string> scenes = new List<string>();
        for (int i = 0; i < EditorBuildSettings.scenes.Length; i++)
        {
            if (EditorBuildSettings.scenes[i].enabled)
            {
                scenes.Add(EditorBuildSettings.scenes[i].path);
            }
        }

        BuildReport report = BuildPipeline.BuildPlayer(scenes.ToArray(), "Build\\Android\\PPOP_ChallengeProject", BuildTarget.Android, BuildOptions.None);

        BuildSummary summary = report.summary;

        if (summary.result == BuildResult.Succeeded)
            Debug.Log("Build succeeded: " + summary.totalSize + " bytes");
        else if (summary.result == BuildResult.Failed)
            Debug.Log("Build Failed!");

        Debug.Log("Errors: " + summary.totalErrors);
        Debug.Log("Warnings: " + summary.totalWarnings);
        Debug.Log("Total elapsed time: " + summary.totalTime);
    }
}
