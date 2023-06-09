using UnityEngine;
using UnityEngine.Video;
using UnityEngine.UI;

public class TowerVideoController : MonoBehaviour
{
    public RawImage rawImage;
    public VideoPlayer videoPlayer;

    void Start()
    {
        videoPlayer.loopPointReached += OnVideoEnd;
        videoPlayer.targetTexture = new RenderTexture((int)videoPlayer.clip.width, (int)videoPlayer.clip.height, 0);
        rawImage.texture = videoPlayer.targetTexture;
    }

    public void PlayVideo()
    {
        videoPlayer.Play();
    }

    public void OnVideoEnd(VideoPlayer vp)
    {
    }
}
