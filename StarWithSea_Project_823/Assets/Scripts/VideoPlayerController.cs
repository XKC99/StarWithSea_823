using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;
using UnityEngine.Events;
using System.Collections;

public class VideoPlayerController : MonoBehaviour
{
    public RawImage rawImage;      // 显示视频的RawImage
    public VideoPlayer videoPlayer; // 视频播放器组件
    public Slider progressBar;      // 进度条
    public Slider volumeBar;        // 音量控制条
    public Button playPauseButton;  // 播放/暂停按钮
    public UnityEvent onVideoEnd;   // 视频结束事件

    private bool isPaused = false;
    private Coroutine hideUICoroutine;

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

        // 初始隐藏UI
        HideUI();
    }

    void Update()
    {
        if (videoPlayer.isPlaying)
        {
            // 更新进度条
            progressBar.value = (float)(videoPlayer.time / videoPlayer.length);
        }

        // 检测鼠标左键点击
        if (Input.GetMouseButtonDown(0))
        {
            ShowUI();

            // 如果有正在运行的隐藏UI协程，先停止它
            if (hideUICoroutine != null)
            {
                StopCoroutine(hideUICoroutine);
            }

            // 开始新的隐藏UI协程
            hideUICoroutine = StartCoroutine(HideUIAfterDelay(3f));
        }
    }

    void PrepareVideo(VideoPlayer vp)
    {
        // 准备好后显示视频
        //rawImage.texture = videoPlayer.texture;
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


    public void ChangeVideo(VideoClip newClip)
    {
        // 如果视频正在播放，先暂停
        if (videoPlayer.isPlaying)
        {
            videoPlayer.Pause();
            isPaused = true;
        }

        // 更换视频源
        videoPlayer.clip = newClip;

        // 重新准备视频
        videoPlayer.Prepare();

        // 重新显示UI（根据需要可以选择是否显示）
        ShowUI();
    }

    void ShowUI()
    {
       // rawImage.gameObject.SetActive(true);
        progressBar.gameObject.SetActive(true);
        volumeBar.gameObject.SetActive(true);
        playPauseButton.gameObject.SetActive(true);
    }

    void HideUI()
    {
        //rawImage.gameObject.SetActive(false);
        progressBar.gameObject.SetActive(false);
        volumeBar.gameObject.SetActive(false);
        playPauseButton.gameObject.SetActive(false);
    }

    IEnumerator HideUIAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        HideUI();
    }
}
