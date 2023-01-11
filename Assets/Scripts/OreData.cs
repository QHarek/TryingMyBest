using UnityEngine;

[CreateAssetMenu(fileName = "New OreData", menuName = "New Ore Data", order = 51)]
public sealed class OreData : ScriptableObject
{
    [SerializeField] private Sprite _icon;
    [SerializeField] private int _cost;
    [SerializeField] private int _weight;
    [SerializeField] private int _stackSize;
    [SerializeField] private string _name;
    [SerializeField] private string _description;

    public Sprite Icon => _icon;
    public int Cost => _cost;
    public int Weight => _weight;
    public int StackSize => _stackSize;
    public string Name => _name;
    public string Description => _description;
}