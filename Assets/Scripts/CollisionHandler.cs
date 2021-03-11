using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    [SerializeField] private float delayBeforeRestartLevel = 1f;
    
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log($"{name} triggered by {other.gameObject.name}");
        DisablePlayerControls();
        ReloadLevelTimeout();
    }

    private void DisablePlayerControls()
    {
        GetComponent<PlayerControls>().enabled = false;
    }

    private void ReloadLevelTimeout()
    {
        Invoke(nameof(ReloadLevel), delayBeforeRestartLevel);
    }

    private void ReloadLevel()
    {
        var currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex); 
    }
}
