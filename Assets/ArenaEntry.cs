using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArenaEntry : MonoBehaviour
{
    [SerializeField] private Camera _cameraEntry;
    private Camera _playerCamera;
    private Animator _cameraAnimator;
    private GameObject _mouseManager;
    private void Start()
    {
        _playerCamera = GameObject.FindWithTag("MainCamera").GetComponent<Camera>();
        _cameraAnimator = _cameraEntry.GetComponent<Animator>();
        _mouseManager = GameObject.Find("MouseManager");
    }

    private void OnTriggerEnter(Collider other)
    {
        Invoke("TurnCameraMainOn", _cameraEntry.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).length);
        if (other.CompareTag("Player"))
        {
            
            foreach (var cam in Camera.allCameras) {
                cam.enabled = false;
            }
            print("player entered trigger");
            _mouseManager.SetActive(false);
            _cameraEntry.enabled = true;
            _cameraAnimator.enabled = true;
            Destroy(GetComponent<Collider>());
        }
    }

    void TurnCameraMainOn()
    {
        _cameraEntry.enabled = false;
        _playerCamera.enabled = true;
        _mouseManager.SetActive(true);
    }
}
