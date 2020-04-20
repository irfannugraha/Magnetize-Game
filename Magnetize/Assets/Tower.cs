using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
    public PlayerController playerController;
    
    void OnMouseDown()
    {
        // Tugas : Tower yang di klik akan diassign ke dalam closestTower pada class playerController
        playerController.closestTower = this.gameObject;
        this.gameObject.GetComponent<SpriteRenderer>().color = Color.green;
    }

    void OnMouseEnter()
    {
        this.gameObject.GetComponent<SpriteRenderer>().color = Color.gray;
    }

    private void OnMouseExit() {
        this.gameObject.GetComponent<SpriteRenderer>().color = Color.white;
    }

}
