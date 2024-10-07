using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardDatabase : MonoBehaviour
{
    public static List<CardInfo> Cards = new List<CardInfo>();
    public Sprite[] images;

    public static CardDatabase Instance {
        get;
        private set;
    }

    private void Awake(){
        if(Instance != null){
            Destroy(gameObject);
        } else {
            Instance = this;
        }

        int id = 0;
        for(int family = 1; family < 4; family++){
            for(int border = 1; border < 4; border++){
                for(int number = 1; number < 6; number++){
                    Cards.Add(new CardInfo(id, family, border, number, images[id]));
                    id++;
                }
            }
        }
    }

    public Sprite getImage(int Id){
        return Cards[Id].image;
    } 
}