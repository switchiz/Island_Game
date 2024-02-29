using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hp_UI : MonoBehaviour
{
    Animator animator;

    Player player;

    readonly int hp_sprite = Animator.StringToHash("Player_Hp");

    private void Start()
    {
        animator = GetComponent<Animator>();
        player = GameManager.Instance.Player;
    }

    void Update()
    {
        animator.SetInteger(hp_sprite, player.hp);
    }
}
