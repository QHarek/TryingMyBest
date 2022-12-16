using UnityEngine;
using UnityEngine.UI;

public class UIStatsController : MonoBehaviour
{
    [SerializeField] Image _hpBar;

    public void UpdateHPBar()
    {
        _hpBar.fillAmount = FindObjectOfType<PlayerStats>().CurrentHealth / FindObjectOfType<PlayerStats>().MaxHealth;
    }

    public void DestroyStatusBars()
    {
        Destroy(gameObject);
    }
}
