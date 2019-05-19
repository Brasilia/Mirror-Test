using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

//This component must have server authority only
public class TurnManager : NetworkBehaviour
{
    public int turn;
    private int battlers = 2;

    public GameObject battlerPrefab;

    public Battler activeBattler;

    public delegate void OnTurnChangedDelagate();
    [SyncEvent]
    public event OnTurnChangedDelagate EventOnTurnChanged;

    // Start is called before the first frame update
    void Start()
    {
        GetComponent<NetworkManager>();
        StartCoroutine(CicleTurns());
        Debug.Log("Coroutine started and stepped out of");
        StartCoroutine(PlaceBattlers());
    }

    private IEnumerator PlaceBattlers()
    {
        while (NetworkServer.connections.Count < 2)
        {
            Debug.Log("Waiting. Connections: " + NetworkServer.connections.Count);
            yield return new WaitForSeconds(1);
        }
        Debug.Log("Connections: " + NetworkServer.connections + " of length " + NetworkServer.connections.Count);
        Debug.Log("First player name: " + NetworkServer.connections[0].playerController.name);
        //Debug.Log("Second player name: " + NetworkServer.connections[1].playerController.name);
        InstantiateBattlers();
        
    }

    private void InstantiateBattlers()
    {
        List<int> connectionKeys = new List<int>();
        foreach (KeyValuePair<System.Int32, NetworkConnection> entry in NetworkServer.connections)
        {
            Debug.Log("Key: " + entry.Key);
            connectionKeys.Add(entry.Key);
            //i++;
        }
        for (int i = 1; i <= 2; i++)
        {
            GameObject pBattlers = new GameObject("p" + i + " Battlers");
            List<Vector2> takenPositions = new List<Vector2>();
            for (int j = 0; j < 3; j++)
            {
                GameObject obj = Instantiate(battlerPrefab, pBattlers.transform);
                obj.GetComponent<Battler>().playerBrain = NetworkServer.connections[connectionKeys[i-1]].playerController.GetComponent<PlayerHUDManager>(); //FIXME - gambiarra no dictionary
                do
                {
                    obj.transform.position = new Vector2(Random.Range(-2, 0) * -(i * 2 - 3), Random.Range(-1, 2)) * 2f;
                }
                while (takenPositions.Contains(obj.transform.position));
                takenPositions.Add(obj.transform.position);

                //obj.GetComponent<Battler>().CmdSetRandomName();
                NetworkServer.Spawn(obj);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PrintMessage(string message)
    {
        Debug.Log(message);
    }

    public void EndTurn()
    {
        Debug.Log("EndTurn");
    }

    private IEnumerator CicleTurns()
    {
        while (true){
            if (NetworkServer.connections.Count < 2)
            {
                Debug.Log("Waitting for players to connect in order to cicle turns.");
                yield return new WaitForSeconds(2);
            }
            
            yield return new WaitForSeconds(5);
            battlers = FindObjectsOfType<Battler>().Length;
            if (battlers != 0)
            {
                turn += 1;
                turn %= battlers;
                activeBattler = FindObjectsOfType<Battler>()[turn];
                EventOnTurnChanged?.Invoke();
            }
            

        }  
    }

    public void AddListener(OnTurnChangedDelagate method)
    {
        EventOnTurnChanged += method;
    }

    public void RemoveListener(OnTurnChangedDelagate method)
    {
        EventOnTurnChanged -= method;
    }

}
