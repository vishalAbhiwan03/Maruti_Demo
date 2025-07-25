using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneLoader : MonoBehaviour
{
    [SerializeField] private Button loadSceneBtn;
    [SerializeField] private Button startLineBtn;
    [SerializeField] private GameObject lineObject;
    [SerializeField] private GameObject startPanel;
    [SerializeField] private GameObject disclainerPanel;
    [SerializeField] private GameObject finishPanel;

    private void Start()
    {
        loadSceneBtn.onClick.AddListener(LoadScene);
        startLineBtn.onClick.AddListener(StartLine);
    }

    private void LoadScene()
    {
        P3DPaintedPercentage.instance.CalculatePainted();
        SceneManager.LoadScene("FinalScene");

        // Subscribe to sceneLoaded so we can act after scene finishes loading
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // unsubscribe to avoid repeated calls
        SceneManager.sceneLoaded -= OnSceneLoaded;

        Debug.Log($"✅ Scene {scene.name} loaded. Waiting to disable Animator halfway…");
        StartCoroutine(DisableAnimatorAtHalfway());
    }

    private IEnumerator RemovePanel()
    {
        yield return new WaitForSeconds(2f);
        startPanel.SetActive(false);
        disclainerPanel.SetActive(false);
    }
    
    private IEnumerator FinishPanelSpawn()
    {
        yield return new WaitForSeconds(20f);
        finishPanel.SetActive(true);
        
    }
    private void StartLine()
    {
        StartCoroutine(RemovePanel());
        Animator lineAnimator = lineObject.GetComponent<Animator>();
        Debug.Log("▶️ Starting line animation");
        lineAnimator.enabled = true;
        StartCoroutine(FinishPanelSpawn());
    }

    private IEnumerator DisableAnimatorAtHalfway()
    {
        yield return null; // wait one frame so Animator updates

        Animator animator = lineObject.GetComponent<Animator>();
        if (animator == null)
        {
            Debug.LogWarning("⚠️ No Animator found on lineObject!");
            yield break;
        }

        AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0);
        float clipLength = stateInfo.length;
        float halfTime = clipLength * 0.47f;

        Debug.Log($"⏳ Waiting {halfTime:F2} seconds to disable Animator at halfway.");

        yield return new WaitForSeconds(halfTime);

        animator.enabled = false;

        Debug.Log("⛔ Animator disabled at halfway point.");
    }
}