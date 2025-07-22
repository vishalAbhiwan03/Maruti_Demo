using UnityEngine;
using UnityEngine.SceneManagement;

public class PaintedObject : MonoBehaviour
{
    private static PaintedObject instance;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject); 
        }
    }
}
