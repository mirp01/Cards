using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Card : MonoBehaviour
{
    public int id; // 1 to 45
    public int family; // 1:hunter  , 2: wizard , 3: artificies
    public int number; // 1 to 5
    public int border;  // 1: border1, 2: border2, 3:border3
    public Image imageDisplay;

    void Start() {
        imageDisplay.sprite = CardDatabase.Instance.getImage(id);
    }

    // Update is called once per frame
    void Update() {
        
    }
}
