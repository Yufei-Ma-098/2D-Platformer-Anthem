using UnityEngine;
using UnityEngine.Video;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class BubbleVideoController : MonoBehaviour
{
    public RawImage rawImage;
    public VideoPlayer videoPlayer;
    public string nextSceneName;
    private AsyncOperation asyncOperation;

    void Start()
    {
        asyncOperation = SceneManager.LoadSceneAsync(nextSceneName);
        asyncOperation.allowSceneActivation = false;

        videoPlayer.loopPointReached += OnVideoEnd;
        videoPlayer.targetTexture = new RenderTexture((int)videoPlayer.clip.width, (int)videoPlayer.clip.height, 0);
        rawImage.texture = videoPlayer.targetTexture;
    }

    public void OnVideoEnd(VideoPlayer vp)
    {
        asyncOperation.allowSceneActivation = true;
    }
}
