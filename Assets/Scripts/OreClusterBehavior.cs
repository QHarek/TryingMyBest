using UnityEngine;
using UnityEngine.Events;

public class OreClusterBehavior : MonoBehaviour
{
    private readonly UnityEvent _clusterDestroyed = new UnityEvent();
    private GameObject _hittingPlayer;
    private float _currentDurability;
    private int _oreAmount;

    [HideInInspector] internal OreClusterData oreClusterData;

    private void Start()
    {
        MeshRenderer[] meshRenderers = GetComponentsInChildren<MeshRenderer>();
        foreach (var meshRenderer in meshRenderers)
        {
            if(meshRenderer.gameObject.name.Contains("Ore_"))
                meshRenderer.material = oreClusterData.OreMaterial;
        }
        _currentDurability = oreClusterData.Durability;
        _oreAmount = Random.Range(oreClusterData.OreAmountMin, oreClusterData.OreAmountMax);

        _clusterDestroyed.AddListener(GiveRewardToPlayer);
        _clusterDestroyed.AddListener(SendGenerationRequest);
        _clusterDestroyed.AddListener(DestroyCluster);
    }

    public void ApplyDamage(GameObject player, float damage)
    {
        _currentDurability -= damage;
        Debug.Log(name + " HP left: " + _currentDurability);
        if (_currentDurability == 0)
        {
            _hittingPlayer = player;
            _clusterDestroyed.Invoke();
        }
    }

    private void SendGenerationRequest()
    {
        GetComponentInParent<OrePlacer>().GenerateNewCluster(oreClusterData.OreType);
    }

    private void GiveRewardToPlayer()
    {
        Debug.Log(_oreAmount);
        _hittingPlayer.GetComponent<PlayerInventory>().AddItem(oreClusterData.OreItemPrefab, _oreAmount);
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
