
using UnityEngine;
using UnityEngine.SceneManagement;

public class WSBHpBarManger : MonoBehaviour
{
    [SerializeField]
    private WSBPlayerController player = null;
    [SerializeField]
    private WSBHpBar hpBar = null;

    private float damage;

    private void Update()
    {

        player.Damage(damage);
        hpBar.UpdateHpBar(player.MaxHp, player.CurHp);


    }
}
