using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class PlayerHUD : MonoBehaviour
{
    private PlayerHUDManager playerHUDManager;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetPlayerHUDManager(PlayerHUDManager phm)
    {
        playerHUDManager = phm;
    }

    public void Command1()
    {
        playerHUDManager.CmdCommand1();
    }

    public void EndTurn()
    {
        FindObjectOfType<TurnManager>().EndTurn(); //FIXME: o host consegue acessar
    }
}
