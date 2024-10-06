using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    Card playerCard;
    Card CPUCard;

    public Deck CPUDeck;

    [SerializeField] private Card objectToSpawn;

    public GameObject[] cardPos;
    public GameObject CPUposition;
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

    bool compareSize(int a, int b){
        if(a==1 && b==3 ||b==1 && a==3 ){
            return true;
        }else{
            return false;
        }
    }

    void CardComparison(int Card1, int Card2 ){ // index de las cartas (de activeDeck)
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
            }else {
                if (Card1Family > Card2Family) { //si es mayor gana
                    Debug.Log("Card 1 wins (bigger family)");
                } else {
                    Debug.Log("Card 2 wins (bigger family)");
                }
            }
        }else{
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
        PlayerSelectCard(2);
        
    }

    public void PlayerSelectCard(int cardNum){
        this.playerCard = cards[cardNum];
        CPUCard = Instantiate(objectToSpawn, CPUposition.transform);
        this.CPUCard.setValues(CPUDeck.PullCard());

        // Cosas para mover la cámara y la tarjeta

        CardComparison(playerCard.id, CPUCard.id);

        // Cosas para sacar la carta de escena y guardar si ganó o no

        StartCoroutine(WaitforResult(cardNum));
        
    }

    public void refillDeck(int cardToRefill){
        Player.Instance.playerDeck.ReturnCard(cards[cardToRefill].id);
        cards[cardToRefill].setValues(Player.Instance.playerDeck.PullCard());
    }

    public IEnumerator WaitforResult(int i){
            yield return new WaitForSeconds(3);
        
        refillDeck(i);
        
    }

    public void checkUserWon(){
        int tempFam1;
        int tempFam2;
        int tempFam3;

        for (int i = 0; i < 3 ; i++){
            for(int j = 0; j < 3; j++){
                //winsPlayer[i][j];
            }
        }
    }

}   

