using UnityEngine.Networking;

public class AntHomeNetwork : NetworkBehaviour
{
    [SyncVar]
    private float health = 100f;
    [SyncVar]
    private int currentStage = 1;

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

        if (health < 75f && currentStage == 1)
        {
            currentStage = 2;
            RpcUpdateStage(currentStage);
        }
        else if (health < 50f && currentStage == 2)
        {
            currentStage = 3;
            RpcUpdateStage(currentStage);
        }
        else if (health < 25f && currentStage == 3)
        {
            currentStage = 4;
            RpcUpdateStage(currentStage);
        }



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

    [ClientRpc]
    private void RpcUpdateStage(int stage)
    {
        homeCollider.SetStage(stage);
    }
}
