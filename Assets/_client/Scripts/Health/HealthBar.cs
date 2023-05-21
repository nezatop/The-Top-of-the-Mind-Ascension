using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    private GameObject playerHealth;

    [SerializeField] private Image totalhealthBar;
    [SerializeField] private Image currenthealthBar;

    private void Start()
    {
        if (playerHealth == null) { playerHealth = GameObject.FindGameObjectWithTag("Player"); }
        else totalhealthBar.fillAmount = playerHealth.GetComponent<Health>().currentHealth / 10;
    }

    private void Update()
    {
        if(playerHealth == null) { playerHealth = GameObject.FindGameObjectWithTag("Player"); }
        else currenthealthBar.fillAmount = playerHealth.GetComponent<Health>().currentHealth / 10;
    }
}
