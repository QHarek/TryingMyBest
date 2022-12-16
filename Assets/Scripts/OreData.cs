using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "New OreData", menuName = "New Ore Data", order = 51)]
public class OreData : ScriptableObject
{
    [SerializeField] private Image _icon;
    [SerializeField] private int _cost;
    [SerializeField] private int _weight;
    [SerializeField] private int _stackSize;
    [SerializeField] private string _description;
}
