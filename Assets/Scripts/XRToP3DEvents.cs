using UnityEngine;
using UnityEngine.InputSystem;

public class XRToP3DEvents : MonoBehaviour
{
    [Header("Right Hand Trigger Action")]
    public InputActionReference rightHandTriggerAction;

    [Header("Effects")]
    public ParticleSystem particles;
    public AudioSource audioSource;

    private InputAction triggerAction;

    void OnEnable()
    {
        if (rightHandTriggerAction == null)
        {
            Debug.LogError("Trigger Action not assigned!");
            return;
        }

        if (particles == null) Debug.LogWarning("ParticleSystem not assigned!");
        if (audioSource == null) Debug.LogWarning("AudioSource not assigned!");

        triggerAction = rightHandTriggerAction.action;
        triggerAction.performed += OnTriggerPressed;
        triggerAction.canceled += OnTriggerReleased;
        triggerAction.Enable();
    }

    void OnDisable()
    {
        if (triggerAction != null)
        {
            triggerAction.performed -= OnTriggerPressed;
            triggerAction.canceled -= OnTriggerReleased;
            triggerAction.Disable();
        }
    }

    void OnTriggerPressed(InputAction.CallbackContext context)
    {
        if (particles != null)
        {
            particles.Play();
        }

        if (audioSource != null)
        {
            audioSource.Play();
        }
    }

    void OnTriggerReleased(InputAction.CallbackContext context)
    {
        if (particles != null)
        {
            particles.Stop();
        }

        if (audioSource != null)
        {
            audioSource.Stop();
        }
    }
}