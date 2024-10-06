using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Deck : MonoBehaviour
{

    public List<int> shuffleDeck;
    public Queue<int> activeDeck;
    // Start is called before the first frame update
    void Start()
    {
        for(int i = 0; i < CardDatabase.Cards.Count; i++){
            shuffleDeck.Add(i);
        }
        Shuffle();
        FillDeck();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void Shuffle(){
        int temp_value;
        int random_index;
        int size = shuffleDeck.Count;
        for (int i = 0; i < size; i++){
            temp_value = shuffleDeck[i];
            random_index = Random.Range(0, size);
            shuffleDeck[i] = shuffleDeck[random_index];
            shuffleDeck[random_index] = temp_value;
        }
    }

    void FillDeck(){
        int size = shuffleDeck.Count;
        for (int i = 0; i < size; i++){
            activeDeck.Enqueue(shuffleDeck[i]);
        }
    }

    public int PullCard(){
        int value = activeDeck.Dequeue();
        return value;
    }

    public void ReturnCard(int cardId){
        activeDeck.Enqueue(cardId);
    }
}
