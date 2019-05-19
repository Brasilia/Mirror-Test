using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class Battler : NetworkBehaviour
{
    public int maxHitpoints = 100;
    [SyncVar]
    public int hitpoints = 100;
    [SyncVar]
    public string characterName;

    public PlayerHUDManager playerBrain;

    public override void OnStartServer()
    {
        // base.OnStartServer();
        //SetRandomName();
    }

    [ServerCallback]
    private void Awake()
    {
        SetRandomName();
    }

    

    [Server]
    public void SetRandomName()
    {
        Debug.Log("Setting random name");
        if (characterName == "")
        {
            const string chars = "abcdefghijklmnopqrstuvwxyz";
            const string vogals = "aeiou";
            for (int i = 12; i > 1; i = (int)(i / 1.2f))
            {
                if (Random.Range(1, i + 1) != i || characterName.Length < 1)
                {
                    if (Random.Range(0f, 1f) < 0.65f)
                    {
                        characterName += chars[Random.Range(0, 26)];
                    }
                    else
                    {
                        characterName += vogals[Random.Range(0, 5)];
                    }
                }
                if (characterName.Length == 1)
                {
                    characterName = characterName.ToUpper();
                }
            }
        }
        //this.name = "Battler - " + characterName;
        this.name = characterName;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        TakeDamage(10);
    }

    [Server]
    private void TakeDamage(int value)
    {
        hitpoints = hitpoints > value ? hitpoints - value : 0;
    }
}
