using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowPlayerEffect : MonoBehaviour
{
    public static ShowPlayerEffect instance;

    [Header("Player")]
    public PlayerData P1;
    public PlayerData P2;

    [Header("P1 IMG")]
    public GameObject P1Speed;
    public GameObject P1Stun;
    public GameObject P1Mult;

    [Header("P2 IMG")]
    public GameObject P2Speed;
    public GameObject P2Stun;
    public GameObject P2Mult;

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this);
        }
        else
        {
            instance = this;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        P1Speed.SetActive(false);
        P1Stun.SetActive(false);
        P1Mult.SetActive(false);

        P2Speed.SetActive(false);
        P2Stun.SetActive(false);
        P2Mult.SetActive(false);
    }

    // Update is called once per frame
    public void ToggleP1Speed(bool status)
    {
        P1Speed.SetActive(status);
    }

    public void ToggleP1Stun(bool status)
    {
        P1Stun.SetActive(status);
    }

    public void ToggleP1Mult(bool status)
    {
        P1Mult.SetActive(status);
    }

    public void ToggleP2Speed(bool status)
    {
        P2Speed.SetActive(status);
    }

    public void ToggleP2Stun(bool status)
    {
        P2Stun.SetActive(status);
    }

    public void ToggleP2Mult(bool status)
    {
        P2Mult.SetActive(status);
    }
}
