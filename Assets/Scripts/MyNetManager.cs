using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class MyNetManager : NetworkManager
{
    public List<NetworkConnection> connections = new List<NetworkConnection>();

    // Start is called before the first frame update
    //void Start()
    //{
        
    //}

    // Update is called once per frame
    //void Update()
    //{
        
    //}

    public override void OnClientConnect(NetworkConnection conn)
    {
        base.OnClientConnect(conn);
        connections.Add(conn);
    }

    public override void OnClientDisconnect(NetworkConnection conn)
    {
        base.OnClientDisconnect(conn);
        connections.Remove(conn);
    }
}
