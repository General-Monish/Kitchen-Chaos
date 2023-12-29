using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class animation_controller : MonoBehaviour
{
    private const string walk = "iswalk";

    [SerializeField] private Player player;

    private Animator anim;

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        anim.SetBool(walk, player.IsWalk());
    }
}
