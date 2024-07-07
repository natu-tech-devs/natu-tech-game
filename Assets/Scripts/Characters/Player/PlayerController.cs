using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Player))]
public class PlayerController : MonoBehaviour
{
    InputAction input;

    GameInput test;

    private Player _player;
    Player player
    {
        get
        {
            if (_player == null)
            {
                _player = GetComponent<Player>();
            }
            return _player;
        }
    }

    void Start()
    {
    }

    public void shoot(InputAction.CallbackContext context)
    {
        if (context.performed)
            player.sphereAttack();
    }
}
