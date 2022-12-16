using UnityEngine;

[CreateAssetMenu(fileName = "New OreClusterData", menuName = "New Ore Cluster Data", order = 51)]
public class OreClusterData : ScriptableObject
{
    [SerializeField] private int _oreAmountMin;
    [SerializeField] private int _oreAmountMax;
    [SerializeField] public float _durability;
    [SerializeField] private Material _oreMaterial;    
    [SerializeField] private oreTypes _oreType;
    [SerializeField] private GameObject _oreItemPrefab;

    public float Durability => _durability;
    public Material OreMaterial => _oreMaterial;
    public oreTypes OreType => _oreType;
    public int OreAmountMin => _oreAmountMin;
    public int OreAmountMax => _oreAmountMax;
    public GameObject OreItemPrefab => _oreItemPrefab;

    public enum oreTypes
    {
        copperOre = 0,
        ironOre = 1,
        goldOre = 2
    }
}
