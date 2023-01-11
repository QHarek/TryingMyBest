using UnityEngine;
using UnityEngine.Events;

public sealed class OreClusterBehavior : MonoBehaviour
{
    private readonly UnityEvent _clusterDestroyed = new UnityEvent();
    private GameObject _hittingPlayer;
    private float _currentDurability;
    private int _oreAmount;

    internal OreClusterData ClusterData;

    private void Start()
    {
        MeshRenderer[] meshRenderers = GetComponentsInChildren<MeshRenderer>();
        foreach (var meshRenderer in meshRenderers)
        {
            if(meshRenderer.gameObject.name.Contains("Ore_"))
                meshRenderer.material = ClusterData.OreMaterial;
        }
        _currentDurability = ClusterData.Durability;
        _oreAmount = Random.Range(ClusterData.OreAmountMin, ClusterData.OreAmountMax);

        _clusterDestroyed.AddListener(GiveRewardToPlayer);
        _clusterDestroyed.AddListener(SendGenerationRequest);
        _clusterDestroyed.AddListener(DestroyCluster);
    }

    public void ApplyDamage(GameObject player, float damage)
    {
        _currentDurability -= damage;
        if (_currentDurability == 0)
        {
            _hittingPlayer = player;
            _clusterDestroyed.Invoke();
        }
    }

    private void SendGenerationRequest()
    {
        GetComponentInParent<OrePlacer>().GenerateNewCluster(ClusterData.OreType);
    }

    private void GiveRewardToPlayer()
    {
        var ore = new Ore(ClusterData.OreData);
        _hittingPlayer.GetComponent<PlayerInventory>().AddItem(ore, _oreAmount);
    }

    private void DestroyCluster()
    {
        Destroy(gameObject);
    }

    private void OnDestroy()
    {
        _clusterDestroyed.RemoveAllListeners();
    }
}
