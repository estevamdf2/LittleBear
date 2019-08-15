using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/**
 Responsavel por renderizar o menu do jogo 
 */
public class MenuScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        //Mudar de cena se qq tecla for apertada
		if (Input.anyKeyDown){
            Debug.Log("pressionou qq tecla");
            SceneManager.LoadScene("Principal");
        }
        
	}
}
