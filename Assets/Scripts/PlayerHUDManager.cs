using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class PlayerHUDManager : NetworkBehaviour
{
    public static int playerID = 0;

    public GameObject canvas;
    private TurnManager turnManager = null;

    [ClientCallback]
    private void Awake()
    {
        if (isLocalPlayer)
            CmdAddListenerOnTurnChanged();
        //turnManager.AddListener(OnTurnChanged);
        Debug.Log("Awaking");
    }


    [ClientCallback]
    private void OnDestroy()
    {
        if (isLocalPlayer)
            CmdRemoveListenerOnTurnChanged();
        //turnManager.RemoveListener(OnTurnChanged);
        Debug.Log("Destroying");
    }

    // Start is called before the first frame update
    void Start()
    {
        turnManager = FindObjectOfType<TurnManager>();
        if (turnManager != null)
        {
            Debug.Log("Found TurnManager");
            turnManager.PrintMessage("printing from TurnManager");
        }
        if (isLocalPlayer)
        {
            GameObject go = Instantiate(canvas);
            go.transform.GetChild(0).transform.position += new Vector3(Random.Range(-10f, 10f), 0, 0);
            go.GetComponent<PlayerHUD>().SetPlayerHUDManager(this);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    [ClientRpc]
    public void RpcOnTurnChanged()
    {
        Debug.Log("Turn changed. Active battler: " + turnManager.activeBattler.characterName);
    }

    [Command]
    public void CmdCommand1()
    {
        Debug.Log("Command1");
        new GameObject("Command1 - Created");
    }

    [Command]
    private void CmdAddListenerOnTurnChanged()
    {
        turnManager.AddListener(RpcOnTurnChanged);
    }

    [Command]
    private void CmdRemoveListenerOnTurnChanged()
    {
        turnManager.RemoveListener(RpcOnTurnChanged);
    }


}
