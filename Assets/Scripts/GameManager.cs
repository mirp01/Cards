using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    Card playerCard;
    Card CPUCard;

    [SerializeField] private Card objectToSpawn;
    int[,] winsPLAYER = new int[3, 3];
    int[,] winsCPU = new int[3, 3];

    public GameObject[] cardPos;
    public Card[] cards = new Card[4];
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(StartDelay());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    bool checkWins(){


    }

    bool compareSize(int a, int b){
        if(a==1 && b==3 ||b==1 && a==3 ){
            return true;
        }
        
            return false;
    }

    void CardComparison(int Card1, int Card2 ){ // index de las cartas (de activeDeck)
        int Card1Family = CardDatabase.Cards[Card1].family; // Access the family of Card1
        int Card2Family = CardDatabase.Cards[Card2].family; // Access the family of Card2
        int Card1Border = CardDatabase.Cards[Card1].border; // Access the border of Card1
        int Card2Border = CardDatabase.Cards[Card2].border; // Access the border of Card2
        int X = Card1Family + Card2Family;
    
        if(Card1Family != Card2Family){
            if(compareSize(Card1Family,Card2Family)){ //si es 3,1 es true, si es otro caso es falso
                if (Card1Family < Card2Family) {//si es menor gana
                winsPLAYER[Card1Family-1][Card1Border-1]=1;
                Debug.Log("Card 1 wins (smaller family)");//cambiar los log a otra cosa
                } else {
                winsCPU[Card2Family-1][Card2Border-1]=1;
                Debug.Log("Card 2 wins (smaller family)");
                }
            }else {
                if (Card1Family > Card2Family) { //si es mayor gana
                    winsPLAYER[Card1Family-1][Card1Border-1]=1;
                    Debug.Log("Card 1 wins (bigger family)");
                } else {
                    winsCPU[Card2Family-1][Card2Border-1]=1;
                    Debug.Log("Card 2 wins (bigger family)");
                }
            }
        }else{
            int Card1Number = CardDatabase.Cards[Card1].number; // Access the family of Card1
            int Card2Number = CardDatabase.Cards[Card2].number; 
            if (Card1Number > Card2Number) { //si es mayor gana
                winsPLAYER[Card1Family-1][Card1Border-1]=1;
                Debug.Log("Card 1 wins (more power)");
            } 
            else if (Card1Number < Card2Number) {
                winsCPU[Card2Family-1][Card2Border-1]=1;
                Debug.Log("Card 2 wins (more power)");
            }else{
                Debug.Log("Empate");
            }
        }
    }

    public void savePlayerCard(Card PlayerCard){
        this.playerCard = PlayerCard;
    }

    void fillBoard(){
        StartCoroutine(CardDelay());
    }

    public IEnumerator StartDelay(){
        int timeFromStart = 0;
        while(timeFromStart < 3){
            timeFromStart += 1;
            yield return new WaitForSeconds(1);
        }
        fillBoard();
    }

    public IEnumerator CardDelay(){
        for(int i = 0; i < 4; i++){
            cards[i] = Instantiate(objectToSpawn, cardPos[i].transform);
            cards[i].setValues(Player.Instance.playerDeck.PullCard());
            
            yield return new WaitForSeconds(1);
            
        }
        
    }

}   