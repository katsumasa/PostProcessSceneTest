#if UNITY_EDITOR
using UnityEditor;
using UnityEditor.Build.Reporting;
using UnityEditor.Callbacks;
using UnityEditor.SceneManagement;
using UnityEngine;

public class MyScenePostprocessor
{
    [MenuItem("Test/Build")]
    public static void BuildWindows()
    {

        BuildPlayerOptions defaultBuildPlayerOptions = new BuildPlayerOptions();

        var buildPlayerOptions = BuildPlayerWindow.DefaultBuildMethods
            .GetBuildPlayerOptions(defaultBuildPlayerOptions);


        var scene = EditorSceneManager.GetSceneByPath(buildPlayerOptions.scenes[0]);
        if (!scene.isLoaded)
        {
            scene = EditorSceneManager.OpenScene(buildPlayerOptions.scenes[0]);
        }
        EditorSceneManager.MarkSceneDirty(scene);
        EditorSceneManager.SaveScene(scene);


        BuildReport report = BuildPipeline.BuildPlayer(buildPlayerOptions);
        BuildSummary summary = report.summary;

        if (summary.result == BuildResult.Succeeded)
        {
            Debug.Log("Build succeeded: " + summary.totalSize + " bytes");
        }

        if (summary.result == BuildResult.Failed)
        {
            Debug.Log("Build failed");
        }
    }


    [PostProcessSceneAttribute(2)]
    public static void OnPostprocessScene()
    {
        Debug.Log("Post-processing scene after build...");
    }
}
#endif