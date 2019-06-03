using UnityEngine;
using UnityEngine.UI;

public class AntBody : MonoBehaviour
{
    void OnMouseDown()
    {
        SoundManager.PlaySplatSound();

        GameObject ant = GetComponentInParent<AgentControl>().gameObject;
        NetworkObjectHandler networkHandler = GameObject.Find(PlayerPrefs.GetString("uniqueIdentity")).GetComponentInChildren<NetworkObjectHandler>();
        networkHandler.TellServerToDestroyAnt(ant.gameObject);

        // 20 percent chance to drop ant egg
        float rand = Random.value;
        if (rand < 0.1f)
        {
            networkHandler.TellServerToSpawnAntEgg(ant.gameObject);
        }
    }

}
