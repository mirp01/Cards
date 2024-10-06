using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class Card{
    public int id; // 1 to 45
    public int family; // 1:hunter  , 2: wizard , 3: artificies
    public int number; // 1 to 5
    public int border;  // 1: border1, 2: border2, 3:border3

    public Sprite image;

    public Card(){

    }

    public Card(int Id, int Family, int Border, int Number, Sprite Image){
        this.id = Id;
        this.family = Family;
        this.border = Border;
        this.number = Number;
        this.image = Image;
    }

}
