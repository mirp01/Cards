using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    bool compareSize(int a, int b){
        if(a==1 && b==3 ||b==1 && a==3 ){
            return true;
        }
        
            return false;
    }

    void gameComparison(int Card1, int Card2 ){ //index de las cartas (de activeDeck)
       int Card1Family = CardDatabase.Cards[Card1].family; // Access the family of Card1
       int Card2Family = CardDatabase.Cards[Card2].family; // Access the family of Card2
       int X = Card1Family + Card2Family;
    
       if(Card1Family != Card2Family){
        if(compareSize(Card1Family,Card2Family)){ //si es 3,1 es true, si es otro caso es falso
                if (Card1Family < Card2Family) {//si es menor gana
                Debug.Log("Card 1 wins (smaller family)");//cambiar los log a otra cosa
                 } else {
                Debug.Log("Card 2 wins (smaller family)");
                }
            }   else {
                if (Card1Family > Card2Family) { //si es mayor gana
                    Debug.Log("Card 1 wins (bigger family)");
                } else {
                    Debug.Log("Card 2 wins (bigger family)");
                }
            }
            }
    
    
    else{
       int Card1Number = CardDatabase.Cards[Card1].number; // Access the family of Card1
       int Card2Number = CardDatabase.Cards[Card2].number; 
         if (Card1Number > Card2Number) { //si es mayor gana
                Debug.Log("Card 1 wins (more power)");
            } 
            else if (Card1Number < Card2Number) {
                 Debug.Log("Card 2 wins (more power)");
            }else{
                Debug.Log("Empate");
            }
    }

}
}   

