using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Subject
{
    private static GameManager instance;
    public static GameManager Instance { get { return instance; } }

    [SerializeField] int amountFishAte = 0;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    public void PlayerAte()
    {
        NotifyObservers(PlayerAction.Eat);
        amountFishAte++;
    }

    public void PlayerDied()
    {
        NotifyObservers(PlayerAction.Die);
    }
}