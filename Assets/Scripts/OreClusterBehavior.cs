using UnityEngine;
using UnityEngine.Events;

public class OreClusterBehavior : MonoBehaviour
{
    private UnityEvent _clusterDestroyed = new UnityEvent();
    private GameObject _hittingPlayer;
    private int _currentDurability;

    public OreClusterData oreClusterData;

    private void Start()
    {
        MeshRenderer[] meshRenderers = GetComponentsInChildren<MeshRenderer>();
        foreach (var meshRenderer in meshRenderers)
        {
            if(meshRenderer.gameObject.name.Contains("Ore_"))
                meshRenderer.material = oreClusterData.OreMaterial;
        }
        _currentDurability = oreClusterData.Durability;
        _clusterDestroyed.AddListener(GiveRewardToPlayer);
        _clusterDestroyed.AddListener(SendGenerationRequest);
        _clusterDestroyed.AddListener(DestroyCluster);
    }

    public void ApplyDamage(GameObject player)
    {
        _currentDurability -= 1;
        Debug.Log(name + " HP left: " + _currentDurability);
        if (_currentDurability == 0)
        {
            _hittingPlayer = player;
            _clusterDestroyed?.Invoke();
        }
    }

    private void SendGenerationRequest()
    {
        GetComponentInParent<OrePlacer>().GenerateNewCluster(oreClusterData.OreType);
    }

    private void GiveRewardToPlayer()
    {
        Item item = new Item();
        _hittingPlayer.GetComponent<PlayerInventory>().Add(item);
    }

    private void DestroyCluster()
    {
        Destroy(gameObject);
    }

    private void OnDestroy()
    {
        _clusterDestroyed.RemoveListener(GiveRewardToPlayer);
        _clusterDestroyed.RemoveListener(SendGenerationRequest);
        _clusterDestroyed.RemoveListener(DestroyCluster);
    }
}
