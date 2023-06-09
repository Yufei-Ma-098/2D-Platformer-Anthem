using Cinemachine;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CameraController : MonoBehaviour
{
    public CinemachineVirtualCamera vcam1;

    void Awake()
    {
        DontDestroyOnLoad(gameObject);  
    }

    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "Scene2")
        {
            var vcam2 = FindObjectOfType<CinemachineVirtualCamera>();
            if (vcam2 != null)
            {
                SwitchCamera(vcam2);
            }
            else
            {
                Debug.LogError("No virtual camera found in scene");
            }
        }
    }

    void SwitchCamera(CinemachineVirtualCamera vcam)
    {
        vcam1.Priority = 10;
        vcam.Priority = 20;
    }
}
