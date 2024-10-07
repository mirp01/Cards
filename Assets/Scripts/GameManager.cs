using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
//using TMPro;

public class GameManager : MonoBehaviour
{

    public Camera viewCamera;

    public GameObject resultArea;
    public TextMeshProUGUI resultText;
    Card playerCard;
    public GameObject endCardPos;
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

    void Update(){
        if (Input.GetKeyDown(KeyCode.Q)){
            PlayerSelectCard(0);
        }
        if (Input.GetKeyDown(KeyCode.W)){
            PlayerSelectCard(1);
        }
        if (Input.GetKeyDown(KeyCode.E)){
            PlayerSelectCard(2);
        }
        if (Input.GetKeyDown(KeyCode.R)){
            PlayerSelectCard(3);
        }
    }

    private bool compareFamily(int family1, int family2)
    {
        return (family1 == 1 && family2 == 3) ||
               (family1 == 3 && family2 == 2) ||
               (family1 == 2 && family2 == 1);
    }

    void CardComparison(int Card1, int Card2, int Card1Family, int Card2Family){ // index de las cartas (de activeDeck)
        int Card1Border = CardDatabase.Cards[Card1].border; // Access the border of Card1
        int Card2Border = CardDatabase.Cards[Card2].border; // Access the border of Card2
        
        if(compareFamily(Card1Family,Card2Family)){ //true si gana familia 1
            winsPlayer[Card1Family-1][Card1Border-1]=true;
            PlayerTracker.showLogo(Card1Family-1, Card1Border-1);
            checkWin(0);
            resultText.text = "You Won!";
            resultArea.SetActive(true);
            Debug.Log("Card 1 wins");
        } else {
            winsCPU[Card2Family-1][Card2Border-1]=true;
            CPUTracker.showLogo(Card2Family-1, Card2Border-1);
            checkWin(1);
            resultText.text = "CPU Won";
            resultArea.SetActive(true);
            Debug.Log("Card 2 wins");
        }
        
    }

    void CompareCardNumbers(int Card1Number, int Card2Number, int Card1, int Card2){
        int Card1Border = CardDatabase.Cards[Card1].border; // Access the border of Card1
        int Card2Border = CardDatabase.Cards[Card2].border; // Access the border of Card2
        int Card1Family = CardDatabase.Cards[Card1].family; // Access the family of Card1
        int Card2Family = CardDatabase.Cards[Card2].family; // Access the family of Card2
        
        if (Card1Number > Card2Number){
            winsPlayer[Card1Family-1][Card1Border-1]=true;
            PlayerTracker.showLogo(Card1Family-1, Card1Border-1);
            resultText.text = "You Won!";
            resultArea.SetActive(true);
            Debug.Log("Card 1 wins (more power)");
        } else if (Card1Number < Card2Number){
            winsCPU[Card2Family-1][Card2Border-1]=true;
            CPUTracker.showLogo(Card2Family-1, Card2Border-1);
            resultText.text = "CPU Won";
            resultArea.SetActive(true);
            Debug.Log("Card 2 wins (more power)");
        } else {
            Debug.Log("Empate");
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
            yield return new WaitForSeconds(1f);
            
        }
             
    }


    public IEnumerator Battle(int cardNum){
        Vector3 startPosition = viewCamera.transform.position;
        Vector3 endPosition = startPosition + Vector3.up * 1440;
        Vector3 cardStartPos = this.playerCard.transform.position;
        Vector3 cardEndPos = endCardPos.transform.position;

        float elapsedTime = 0.0f;
        float moveDuration = 3f;
        while(elapsedTime < moveDuration){
            elapsedTime += Time.deltaTime;
            float t = Mathf.Clamp01(elapsedTime/ moveDuration);

            t = t * t * (3f - 2f * t);

            viewCamera.transform.position = Vector3.Lerp(startPosition, endPosition, t);
            this.playerCard.transform.position = Vector3.Lerp(cardStartPos, cardEndPos, t);
            yield return null;
        }

        int Card1Family = this.playerCard.family; // Access the family of Card1
        int Card2Family = this.CPUCard.family; // Access the family of Card2

        if (Card1Family != Card2Family) {
            CardComparison(playerCard.id, CPUCard.id, Card1Family, Card2Family);
        } else {
            int Card1Number = this.playerCard.number; 
            int Card2Number = this.CPUCard.number;
            CompareCardNumbers(Card1Number, Card2Number, this.playerCard.id, this.CPUCard.id);
        }

        yield return new WaitForSeconds(2f);

        // Cosas para sacar la carta de escena y guardar si ganó o no

        resultArea.SetActive(false);
        this.playerCard.transform.position = cardStartPos + Vector3.down * 1000;
        CPUDeck.ReturnCard(this.CPUCard.id);


        elapsedTime = 0.0f;
        while(elapsedTime < moveDuration){
            elapsedTime += Time.deltaTime;
            float t = Mathf.Clamp01(elapsedTime/ moveDuration);

            t = t * t * (3f - 2f * t);

            viewCamera.transform.position = Vector3.Lerp(endPosition, startPosition, t);
            yield return null;
        }
        
        Vector3 hiddenPos = this.playerCard.transform.position;
        Player.Instance.playerDeck.ReturnCard(cards[cardNum].id);
        cards[cardNum].setValues(Player.Instance.playerDeck.PullCard());

        elapsedTime = 0.0f;
        moveDuration = 1f;
        while(elapsedTime < moveDuration){
            elapsedTime += Time.deltaTime;
            float t = Mathf.Clamp01(elapsedTime/ moveDuration);

            t = t * t * (3f - 2f * t);

            this.playerCard.transform.position = Vector3.Lerp(hiddenPos, cardStartPos, t);
            yield return null;
        }

    }

    public void PlayerSelectCard(int cardNum){
        this.playerCard = cards[cardNum];
        CPUCard = Instantiate(objectToSpawn, CPUposition.transform);
        this.CPUCard.setValues(CPUDeck.PullCard());
        //Debug.Log($"Card1Family: {playerCard.family}, Card1Number: {playerCard.number}, Card1Color: {playerCard.border}");
        //Debug.Log($"Card2Family: {CPUCard.family}, Card2Number: {CPUCard.number}, Card2Color: {CPUCard.border}");

        // Cosas para mover la cámara y la tarjeta
        StartCoroutine(Battle(cardNum)); 
        
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