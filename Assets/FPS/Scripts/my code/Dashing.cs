using System;
using System.Collections;
using System.Collections.Generic;
using Unity.FPS.Gameplay;
using Unity.VisualScripting;
using UnityEngine;

public class Dashing : MonoBehaviour
{
    private PlayerCharacterController movePlayer;
    public float dashSpeed = 20;
    public double dashTime = 0.25;

    public KeyCode dashKey = KeyCode.E;

    private void Start()
    {
        movePlayer = GetComponent<PlayerCharacterController>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(dashKey))
        {
            Debug.Log("Ket Pressed");
            StopCoroutine(Dash());
        }
    }

    IEnumerator Dash()
    {
        float startTime = Time.time;

        while (Time.time < startTime + dashTime)
        {
            movePlayer.m_Controller.Move(movePlayer.CharacterVelocity * dashSpeed * Time.deltaTime);
            yield return null;
        }
    }
}
