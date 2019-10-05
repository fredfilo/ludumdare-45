﻿using UnityEngine;

public class Goal : MonoBehaviour
{
    // PROPERTIES
    // -------------------------------------------------------------------------

    [SerializeField] private Number m_goalNumber;

    // PRIVATE METHODS
    // -------------------------------------------------------------------------

    private void OnTriggerEnter2D(Collider2D other)
    {
        Player player = other.GetComponent<Player>();
        if (!player) {
            return;
        }

        if (player.value == m_goalNumber.value) {
            GameController.instance.isPaused = true;
            GameController.instance.OnLevelCleared();
        }
        else {
            Debug.Log("Incorrect number!");
        }
    }
}