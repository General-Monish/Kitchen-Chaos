using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSound : MonoBehaviour
{
    private Player Player;
    private float FootStepsTimer;
    private float FootStepsTimerMax=.1f;

    public void Awake()
    {
        Player = GetComponent<Player>();
    }

    private void Update()
    {
        FootStepsTimer -= Time.deltaTime;
        if (FootStepsTimer < 0f)
        {
            FootStepsTimer = FootStepsTimerMax;


            if (Player.IsWalk())
            {
                float volume = 1f;
                SoundManager.Instance.PlayFootStepSound(Player.transform.position, volume);
            }
        }
    }


}
