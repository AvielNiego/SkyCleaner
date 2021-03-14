using UnityEngine;
using UnityEngine.Serialization;

public class Enemy : MonoBehaviour
{
    [SerializeField] private GameObject explosionFx;
    [SerializeField] private GameObject hitVfx;

    [SerializeField] private int killScore = 100;

    [SerializeField] private float life = 3f;

    private ScoreBoard scoreBoard;
    private GameObject parentGameObject;

    private void Start()
    {
        AddRigidBody();
        parentGameObject = GameObject.FindWithTag("SpawnAtRuntime");
        scoreBoard = FindObjectOfType<ScoreBoard>();
    }

    private void AddRigidBody()
    {
        var rb = gameObject.AddComponent<Rigidbody>();
        rb.useGravity = false;
    }

    private void OnParticleCollision(GameObject other)
    {
        life--;
        if (life <= 0)
        {
            scoreBoard.IncreaseScore(killScore);
            KillEnemy(transform.position);
        }
        else
        {
            ShowHitVfx(transform.position);
        }
    }

    private void KillEnemy(Vector3 position)
    {
        Instantiate(explosionFx, position, Quaternion.identity, parentGameObject.transform);
        Destroy(gameObject);
    }

    private void ShowHitVfx(Vector3 position)
    {
        Instantiate(hitVfx, position, Quaternion.identity, parentGameObject.transform);
    }
}
