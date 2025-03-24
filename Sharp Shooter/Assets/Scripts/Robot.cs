using StarterAssets;
using UnityEngine;
using UnityEngine.AI;

public class Robot : MonoBehaviour
{
    NavMeshAgent agent;
    // [SerializeField] Transform target;
    FirstPersonController player;

    void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
    }
    void Start()
    {
        // シーン内で FirstPersonController コンポーネントを持つオブジェクトを検索し、その最初の一つをplayerに格納する
        player = FindFirstObjectByType<FirstPersonController>();
    }

    // Update is called once per frame
    void Update()
    {
        agent.SetDestination(player.transform.position);
    }
}
