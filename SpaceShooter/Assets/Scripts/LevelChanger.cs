using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelChanger : MonoBehaviour
{
    [SerializeField]
    private Animator animator;
    private int _levelToLoad;

    public void FadeToLevel(int levelIndex)
    {
        _levelToLoad = levelIndex;
        animator.SetTrigger("FadeOut");
    }

    public void OnFadeComplete()
    {
        SceneManager.LoadScene(_levelToLoad);
    }
}
