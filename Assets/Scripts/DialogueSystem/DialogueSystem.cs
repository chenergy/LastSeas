using UnityEngine;
using UnityEngine.UI;
using System.Collections;

/// <summary>
/// The dialogue system controller.
/// </summary>
public class DialogueSystem : MonoBehaviour
{
    public Camera m_camera;
    public AudioSource m_audioSource;
    public Transform m_currentTarget;

    public Image m_characterImage;
    public Text m_text;

    [Header("Debug")]
    public string m_testText = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum.";
    public AudioClip m_testClip;
    public Transform m_testTarget;

    private Coroutine m_dialogueRoutine;

    /// <summary>
    /// Use this for initialization.
    /// </summary>
    public void Start()
    {
        m_characterImage.gameObject.SetActive(enabled);
        m_text.gameObject.SetActive(enabled);

        PlayDialogue(m_testTarget, m_testText, 5f, m_testClip);
    }

    /// <summary>
    /// Update is called once per frame.
    /// </summary>
    public void LateUpdate()
    {
        if (m_currentTarget != null)
        {
            m_camera.transform.position = m_currentTarget.transform.position;
            m_camera.transform.rotation = m_currentTarget.transform.rotation;
        }
    }

    public void PlayDialogue(Transform target, string text, float duration, AudioClip clip)
    {
        if (m_dialogueRoutine != null)
        {
            StopCoroutine(m_dialogueRoutine);
            m_dialogueRoutine = null;
        }

        m_currentTarget = target;
        m_audioSource.Stop();
        m_audioSource.clip = clip;
        m_audioSource.Play();

        m_dialogueRoutine = StartCoroutine(_PlayDialogueText(text, duration));
    }

    private void SetUIActive(bool enabled)
    {
        m_characterImage.gameObject.SetActive(enabled);
        m_text.gameObject.SetActive(enabled);
    }

    private IEnumerator _PlayDialogueText(string text, float duration)
    {
        SetUIActive(true);

        float timer = 0;
        int totalChars = text.Length;
        while (timer < duration)
        {
            m_text.text = text.Substring(0, (int)Mathf.Lerp(0, totalChars, timer / duration));

            timer += Time.deltaTime;
            yield return null;
        }

        m_text.text = text;

        yield return new WaitForSeconds(1f);
        SetUIActive(false);
        m_dialogueRoutine = null;
    }
}

