using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    Card playerCard;
    Card CPUCard;
    public Deck CPUDeck;

    [SerializeField] private Card objectToSpawn;
    bool[][] winsPlayer = new bool[3][];
    public Tracker PlayerTracker;
    bool[][] winsCPU = new bool[3][];
    public Tracker CPUTracker;

    public GameObject[] cardPos;
    public GameObject CPUposition;
    public Card[] cards = new Card[4];
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(StartDelay());
        for(int i = 0; i < 3; i ++){
            winsPlayer[i] = new bool[] {false,false,false};
            winsCPU[i] = new bool[] {false,false,false};
        }
        
    }

    bool compareSize(int a, int b){
        if((a==1 && b==3 )||(b==1 && a==3) ){
            return true;
        } else {
            return false;
        }
    }

    void CardComparison(int Card1, int Card2 ){ // index de las cartas (de activeDeck)
        int Card1Family = CardDatabase.Cards[Card1].family; // Access the family of Card1
        int Card1Border = CardDatabase.Cards[Card1].border; // Access the border of Card1
        int Card2Family = CardDatabase.Cards[Card2].family; // Access the family of Card2  
        int Card2Border = CardDatabase.Cards[Card2].border; // Access the border of Card2
    
        if(Card1Family != Card2Family){
            if(compareSize(Card1Family,Card2Family)){ //si es 3,1 es true, si es otro caso es falso
                if (Card1Family < Card2Family) {//si es menor gana
                winsPlayer[Card1Family-1][Card1Border-1]=true;
                PlayerTracker.showLogo(Card1Family-1,Card1Border-1);
                checkWin(0);
                Debug.Log("Card 1 wins (smaller family)");//cambiar los log a otra cosa
                } else {
                winsCPU[Card2Family-1][Card2Border-1]=true;
                CPUTracker.showLogo(Card2Family-1,Card2Border-1);
                checkWin(1);
                Debug.Log("Card 2 wins (smaller family)");
                }
            } else {
                if (Card1Family > Card2Family) { //si es mayor gana
                    winsPlayer[Card1Family-1][Card1Border-1]=true;
                    PlayerTracker.showLogo(Card1Family-1,Card1Border-1);
                    checkWin(0);
                    Debug.Log("Card 1 wins (bigger family)");
                } else {
                    winsCPU[Card2Family-1][Card2Border-1]=true;
                    CPUTracker.showLogo(Card2Family-1,Card2Border-1);
                    checkWin(1);
                    Debug.Log("Card 2 wins (bigger family)");
                }
            }
        } else {
            int Card1Number = CardDatabase.Cards[Card1].number; // Access the family of Card1
            int Card2Number = CardDatabase.Cards[Card2].number; 
            if (Card1Number > Card2Number) { //si es mayor gana
                winsPlayer[Card1Family-1][Card1Border-1]=true;
                PlayerTracker.showLogo(Card1Family-1,Card1Border-1);
                checkWin(0);
                Debug.Log("Card 1 wins (more power)");
            } else if (Card1Number < Card2Number) {
                winsCPU[Card2Family-1][ Card2Border-1]=true;
                CPUTracker.showLogo(Card2Family-1,Card2Border-1);
                checkWin(1);
                Debug.Log("Card 2 wins (more power)");
            } else {
                Debug.Log("Empate");
            }
        }
    }

    void fillBoard(){
        StartCoroutine(CardDelay());
    }

    public IEnumerator StartDelay(){
        int timeFromStart = 0;
        while(timeFromStart < 3){
            timeFromStart += 1;
            yield return new WaitForSeconds(0.1f);
        }
        fillBoard();
    }

    public IEnumerator CardDelay(){
        for(int i = 0; i < 4; i++){
            cards[i] = Instantiate(objectToSpawn, cardPos[i].transform);
            cards[i].setValues(Player.Instance.playerDeck.PullCard());
            yield return new WaitForSeconds(0.1f);
            
        } 
        PickCard();       
    }


    public void PickCard(){
        PlayerSelectCard(0);

    }

    public void PlayerSelectCard(int cardNum){
        this.playerCard = cards[cardNum];
        CPUCard = Instantiate(objectToSpawn, CPUposition.transform);
        this.CPUCard.setValues(CPUDeck.PullCard());

        // Cosas para mover la cámara y la tarjeta
        
        CardComparison(this.playerCard.id, CPUCard.id);

        // Cosas para sacar la carta de escena y guardar si ganó o no

        CPUDeck.ReturnCard(this.CPUCard.id);
        StartCoroutine(refillDeck(cardNum));
        
    }

    public IEnumerator refillDeck(int cardToRefill){
        yield return new WaitForSeconds(0.1f);
        Player.Instance.playerDeck.ReturnCard(cards[cardToRefill].id);
        cards[cardToRefill].setValues(Player.Instance.playerDeck.PullCard());
    }

    void checkWin(int table){
        bool[][] tempTable;
        if(table == 0){
            tempTable = winsPlayer;
        } else {
            tempTable = winsCPU;
        }
        int differentFamilies = 0;
        int differentColors = 0;
        for(int i = 0; i < 3; i++){
            for (int j = 0; j< 3; j++){
                if(tempTable[i][j]){
                    differentColors++;
                    if(differentFamilies <= i){
                        differentFamilies++;
                    }
                }
            }
            if (differentColors == 3){
                if(table == 0){
                    userWon();
                } else {
                    CPUWon();
                }
            } else {
                differentColors = 0;
            }
        }
        if(differentFamilies == 3){
            if(table == 0){
                userWon();
            } else {
                CPUWon();
            }

        }
        return;
    }

    void userWon(){
        Debug.Log("User Won");
    }

    void CPUWon(){
        Debug.Log("CPU Won");
    }



}   