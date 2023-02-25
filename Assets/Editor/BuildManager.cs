using System.Linq;
using UnityEditor;
using UnityEditor.Build.Reporting;
using UnityEngine;

public class BuildManager : MonoBehaviour
{
  [MenuItem("Build/Build WebGL")]
  public static void Build()
  {
    var buildPlayerOptions = new BuildPlayerOptions
    {
      scenes = EditorBuildSettings.scenes.Select(scene => scene.path).ToArray(),
      target = BuildTarget.WebGL,
      options = BuildOptions.None,
      locationPathName = "Builds/WebGL/"
    };

    PlayerSettings.WebGL.emscriptenArgs = "";
    // PlayerSettings.WebGL.emscriptenArgs = "-s EXTRA_EXPORTED_RUNTIME_METHODS=['ccall','cwrap']";

    var report = BuildPipeline.BuildPlayer(buildPlayerOptions);
    
    #if UNITY_EDITOR
    var buildSum = report.summary;
    
    switch (buildSum)
    {
      case { result: BuildResult.Succeeded }:
        Debug.Log("Build Succeeded: " + buildSum.totalSize + " bytes");
        break;
      case { result: BuildResult.Failed }:
        Debug.LogError("Build failed");
        break;
    }
    #endif
  }
}