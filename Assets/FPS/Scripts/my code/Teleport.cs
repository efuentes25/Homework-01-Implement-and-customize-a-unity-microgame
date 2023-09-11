using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using Unity.FPS.Gameplay;
using UnityEngine;

public class Teleport : MonoBehaviour
{
    public KeyCode teleportKey = KeyCode.F;

    PlayerCharacterController playerController;
    void Awake()
    {
        playerController = gameObject.GetComponent<PlayerCharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(teleportKey))
        {
            StartCoroutine("TeleportToStart");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("finalBoss"))
        {
            StartCoroutine("TeleportTo");
        }
        if (other.CompareTag("nextStage"))
        {
            StartCoroutine("TeleportToNextStage");
        }
    }

    IEnumerator TeleportTo()
    {
        playerController.TeleportDisabled = true;
        yield return new WaitForSeconds(0.1f);
        gameObject.transform.position = new Vector3(-84.30f,4.01f,100.80f);
        yield return new WaitForSeconds(1f);
        playerController.TeleportDisabled = false;
    }
    
    IEnumerator TeleportToStart()
    {
        playerController.TeleportDisabled = true;
        yield return new WaitForSeconds(0.1f);
        gameObject.transform.position = new Vector3(-28.50f,-0.244329453f,-7.25f);
        yield return new WaitForSeconds(1f);
        playerController.TeleportDisabled = false;
    }
    
    IEnumerator TeleportToNextStage()
    {
        playerController.TeleportDisabled = true;
        yield return new WaitForSeconds(0.1f);
        gameObject.transform.position = new Vector3(37.47f,-7.75f,0.39f);
        yield return new WaitForSeconds(1f);
        playerController.TeleportDisabled = false;
    }
}
