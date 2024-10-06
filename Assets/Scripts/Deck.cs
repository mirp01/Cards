using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Deck : MonoBehaviour
{

    public List<int> activeDeck;
    // Start is called before the first frame update
    void Start()
    {
        for(int i = 0; i < CardDatabase.Cards.Count; i++){
            activeDeck.Add(i);
        }
        Shuffle();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void Shuffle(){
        int temp_value;
        int random_index;
        int size = activeDeck.Count;
        for (int i = 0; i < size; i++){
            temp_value = activeDeck[i];
            random_index = Random.Range(0, size);
            activeDeck[i] = activeDeck[random_index];
            activeDeck[random_index] = temp_value;
        }
    }
}
