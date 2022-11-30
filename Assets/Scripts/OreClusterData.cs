using UnityEngine;

[CreateAssetMenu(fileName = "OreCluster", menuName = "OreClusters/Create New Cluster", order = 51)]
public class OreClusterData : ScriptableObject
{
    [SerializeField] private int _oreAmountMin;
    [SerializeField] private int _oreAmountMax;
    [SerializeField] public int _durability;
    [SerializeField] private Material _oreMaterial;    
    [SerializeField] private oreTypes _oreType;

    private int _oreAmount;

    public int Durability => _durability;
    public Material OreMaterial => _oreMaterial;
    public oreTypes OreType => _oreType;
    public int OreAmount => _oreAmount;

    public enum oreTypes
    {
        copperOre = 0,
        ironOre = 1,
        goldOre = 2
    }

    private void OnEnable()
    {
        _oreAmount = Random.Range(_oreAmountMin, _oreAmountMax);
    }
}
