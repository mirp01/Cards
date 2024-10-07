using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tracker : MonoBehaviour
{
    public GameObject[] positionsFamily1;
    public GameObject[] positionsFamily2;
    public GameObject[] positionsFamily3;

    public void showLogo(int family, int color){
        //Debug.Log(family);
        //Debug.Log(color);
        switch(family){
            case 0:
                positionsFamily1[color].SetActive(true);
            break;
            case 1:
                positionsFamily2[color].SetActive(true);
            break;
            case 2:
                positionsFamily3[color].SetActive(true);
            break;
        }
    }
}
