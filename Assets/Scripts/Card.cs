using System.Collections;
using System.Collections.Generic;
using UnityEngine;
 [System.Serializable]
public class Card : MonoBehaviour
{
    public int Family; //1:hunter  , 2: wizard , 3: artificies
    public int Number; //power
    public int Border;  //1: border1, 2: border2, 3:border3

    public Card()
    {

    }

    public Card(int Family, int Number, int Border){
        this.Family = Family;
        this.Number = Number;
        this.Border=Border;
    }

}
