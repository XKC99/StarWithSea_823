using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;
using UnityEngine.Events;

public class VideoPlayerController : MonoBehaviour
{
    public RawImage rawImage;      // 显示视频的RawImage
    public VideoPlayer videoPlayer; // 视频播放器组件
    public Slider progressBar;      // 进度条
    public Slider volumeBar;        // 音量控制条
    public Button playPauseButton;  // 播放/暂停按钮
    public UnityEvent onVideoEnd;   // 视频结束事件

    private bool isPaused = false;

    void Start()
    {
        // 绑定事件
        videoPlayer.prepareCompleted += PrepareVideo;
        videoPlayer.loopPointReached += OnVideoEnd;
        progressBar.onValueChanged.AddListener(OnProgressChanged);
        volumeBar.onValueChanged.AddListener(OnVolumeChanged);
        playPauseButton.onClick.AddListener(TogglePlayPause);

        // 初始化视频播放器
        videoPlayer.Prepare();
    }

    void Update()
    {
        if (videoPlayer.isPlaying)
        {
            // 更新进度条
            progressBar.value = (float)(videoPlayer.time / videoPlayer.length);
        }
    }

    void PrepareVideo(VideoPlayer vp)
    {
        // 准备好后显示视频
        rawImage.texture = videoPlayer.texture;
    }

    void OnProgressChanged(float value)
    {
        if (!videoPlayer.isPlaying)
            return;

        // 设置视频播放位置
        videoPlayer.time = value * videoPlayer.length;
    }

    void OnVolumeChanged(float value)
    {
        // 设置音量
        videoPlayer.SetDirectAudioVolume(0, value);
    }

    void TogglePlayPause()
    {
        if (isPaused)
        {
            videoPlayer.Play();
            isPaused = false;
        }
        else
        {
            videoPlayer.Pause();
            isPaused = true;
        }
    }

    void OnVideoEnd(VideoPlayer vp)
    {
        // 响应视频播放结束事件
        onVideoEnd.Invoke();
    }
}
