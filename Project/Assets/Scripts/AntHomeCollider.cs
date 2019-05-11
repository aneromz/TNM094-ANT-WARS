using UnityEngine;
using UnityEngine.UI;

public class AntHomeCollider : MonoBehaviour
{
    public Image healthBar;

    private const float DAMAGE_TIME_INTERVAL = 0.5f;
    private const float COLOR_INTERPOLATION_TIME = 0.5f;

    private float damageTimer = 0f;
    private float interpolationTimer = COLOR_INTERPOLATION_TIME;

    private bool isTakingDamage = false;
    private static bool canTakeDamage;

    public AntHomeNetwork homeNetwork;

    private NetworkObjectHandler networkHandler;

    private void Awake()
    {
        CanTakeDamage(true);

        networkHandler = GameObject.Find(PlayerPrefs.GetString("uniqueIdentity")).GetComponentInChildren<NetworkObjectHandler>();
    }

    private void Update()
    {
        if (isTakingDamage)
        {
            DisplayWarningSignal();
        }
    }

    void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.tag == "RedAnt" && tag == "RedHome")
        {
            if (canTakeDamage)
                TakeDamage(collision.gameObject.GetComponentInParent<AgentControl>().GetOwnerId());
        }
        else if (collision.gameObject.tag == "BlueAnt" && tag == "BlueHome")
        {
            if (canTakeDamage)
                TakeDamage(collision.gameObject.GetComponentInParent<AgentControl>().GetOwnerId());
        }
    }

    private void TakeDamage(string ownerId)
    {
        isTakingDamage = true;

        damageTimer += Time.deltaTime;
        if (damageTimer > DAMAGE_TIME_INTERVAL)
        {
            // Reset damage timer
            damageTimer = 0f;

            // Make sure only the player who spawned the ant apply the damage
            if (ownerId != PlayerPrefs.GetString("uniqueIdentity"))
                return;

            // Reduce health
            networkHandler.TellServerToDamageHome(homeNetwork.gameObject);
        }
    }

    public void UpdateHealthBar(float currentHealth)
    {
        healthBar.fillAmount = currentHealth / 100f;
    }

    private void DisplayWarningSignal()
    {
        if (interpolationTimer > 0f)
        {
            // Interpolate the health bar color from white to green
            healthBar.color = Color.Lerp(Color.green, Color.white, interpolationTimer / COLOR_INTERPOLATION_TIME);
            interpolationTimer -= Time.deltaTime;
        }
        else
        {
            isTakingDamage = false;
            interpolationTimer = COLOR_INTERPOLATION_TIME;
        }
    }

    static private void CanTakeDamage(bool flag)
    {
        canTakeDamage = flag;
    }

    public void HomeIsDead()
    {
        CanTakeDamage(false);
    }
}