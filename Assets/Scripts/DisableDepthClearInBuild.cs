#if UNITY_EDITOR
using UnityEditor.Build;
using UnityEditor.Build.Reporting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DisableDepthClearInBuild : MonoBehaviour, IProcessSceneWithReport
{
    public int callbackOrder { get { return 1; } }

    public void OnProcessScene(Scene scene, BuildReport report)
    {
        foreach (GameObject rootGameObject in scene.GetRootGameObjects())
            foreach (Camera camera in rootGameObject.GetComponentsInChildren<Camera>())
                if (camera.clearFlags == CameraClearFlags.Nothing) 
                    camera.clearFlags = CameraClearFlags.Depth;
    }
}
#endif