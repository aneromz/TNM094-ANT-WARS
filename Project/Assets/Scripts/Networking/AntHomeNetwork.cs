using UnityEngine.Networking;

public class AntHomeNetwork : NetworkBehaviour
{
    [SyncVar]
    private float health = 100f;

    public AntHomeCollider homeCollider;

    private void Awake()
    {
        //homeCollider = GetComponentInChildren<AntHomeCollider>();
    }

    [Command]
    public void CmdTakeDamage()
    {
        // If health is 0, home is dead
        if (health < 0f)
        {
            homeCollider.HomeIsDead();
            RpcDisplayGameOver();

            return;
        }

        // Decrease health
        health -= 6f;

        // Update health bar on all clients
        RpcUpdateHealthBar();
    }

    [ClientRpc]
    private void RpcUpdateHealthBar()
    {
        homeCollider.UpdateHealthBar(health);
    }

    [ClientRpc]
    private void RpcDisplayGameOver()
    {
        homeCollider.gameObject.SetActive(false);
        FindObjectOfType<CustomNetworkManagerUI>().ShowGameOverPanel(tag);
    }

}
