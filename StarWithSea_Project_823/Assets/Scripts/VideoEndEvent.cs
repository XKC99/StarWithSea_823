using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Video;

public class VideoEndEvent : MonoBehaviour
{
    public UnityEvent onVideoEnd;
    private VideoPlayer videoPlayer;

    private void Awake()
    {
        videoPlayer = this.GetComponent<VideoPlayer>();
        // 订阅 loopPointReached 事件
        videoPlayer.loopPointReached += OnVideoEndReached;
    }

    // 在对象销毁时解除事件订阅，防止内存泄漏
    private void OnDestroy()
    {
        videoPlayer.loopPointReached -= OnVideoEndReached;
    }

    // 当视频播放结束时触发此方法
    private void OnVideoEndReached(VideoPlayer vp)
    {
        Debug.Log("Video End");
        OnVideoEnd();
    }

    public void OnVideoEnd()
    {
        onVideoEnd?.Invoke();
    }
}
