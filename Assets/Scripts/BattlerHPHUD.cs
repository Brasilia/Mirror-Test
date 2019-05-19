using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Mirror;

public class BattlerHPHUD : NetworkBehaviour
{
    public Battler battler;
    public Slider slider;
    public Text text;

    private void Awake()
    {
        //slider = GetComponent<Slider>();
        //text = GetComponent<Text>();
        
    }

    // Start is called before the first frame update
    void Start()
    {
        text.text = battler.name;
    }

    // Update is called once per frame
    void Update()
    {
        //transform.position = battler.transform.position;
        slider.value = (float)battler.hitpoints / battler.maxHitpoints;
        text.text = battler.characterName;
    }
}
