using System;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    [SerializeField] private float delayBeforeRestartLevel = 1f;
    
    [SerializeField] private ParticleSystem explosion;
    [SerializeField] private GameObject masterTimeline;

    
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log($"{name} triggered by {other.gameObject.name}");
        
        DisablePlayerControls();
        ReloadLevelTimeout();
        PlayExplosionParticles();
        GetComponent<MeshRenderer>().enabled = false;
        Array.ForEach(GetComponents<Collider>(), c => c.enabled = false);
        masterTimeline.GetComponent<PlayableDirector>().Pause();
    }

    private void PlayExplosionParticles()
    {
        if (!explosion.isPlaying)
        {
            explosion.Play();
        }
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
