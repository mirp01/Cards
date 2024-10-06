using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Card : MonoBehaviour
{
    public int id; // 1 to 45
    public int family; // 1:hunter  , 2: wizard , 3: artificies
    public int number; // 1 to 5
    public int border;  // 1: border1, 2: border2, 3:border3
    public Image imageDisplay;

    void Start() {

    }

    // Update is called once per frame
    void Update() {
        
    }

    public void setValues(int Id){
        this.id = Id;
        imageDisplay.sprite = CardDatabase.Instance.getImage(id);
        this.family = CardDatabase.Cards[Id].family;
        this.number = CardDatabase.Cards[Id].number;
        this.border = CardDatabase.Cards[Id].border;

    }
}
