using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; // 引入场景管理命名空间

public class SpecialDo : MonoBehaviour
{

    // 切换场景的方法
    public void ChangeScene(string sceneName)
    {
        // 切换到指定场景
        SceneManager.LoadScene(sceneName);
    }

    // 可选：带淡入淡出效果的场景切换
    public void ChangeSceneWithFade(string sceneName, float fadeDuration)
    {
        StartCoroutine(FadeAndSwitchScene(sceneName, fadeDuration));
    }

    // 私有协程：处理淡入淡出的场景切换
    private IEnumerator FadeAndSwitchScene(string sceneName, float fadeDuration)
    {
        // 假设有一个FadeIn和FadeOut的UI
        // 这里你需要先实现FadeOut的效果
        yield return new WaitForSeconds(fadeDuration);

        // 切换场景
        SceneManager.LoadScene(sceneName);

        // 这里你可以实现FadeIn的效果
    }
}

