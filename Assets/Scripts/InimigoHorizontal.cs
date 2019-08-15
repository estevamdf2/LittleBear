using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InimigoHorizontal : MonoBehaviour {

    public bool v_colidiu = false;
    private float v_move = -2;

    

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        GetComponent<Rigidbody2D>().velocity = new Vector2(v_move, GetComponent<Rigidbody2D>().velocity.y);

        if (v_colidiu)
        {
            Flip();  // chama a função flip
        }
	}
    private void Flip()
    {
        v_move *= -1;
        GetComponent<SpriteRenderer>().flipX = !GetComponent<SpriteRenderer>().flipX;
        v_colidiu = false;
    }

    // Verifica se o inimigo está colidindo com a plataforma
    void OnCollisionEnter2D(Collision2D v_col)
    {
        
        if (v_col.gameObject.CompareTag("tag_plataforma"))
        {
           
            v_colidiu = true;
        }
        if (v_col.gameObject.CompareTag("tag_personagem"))
        {

            Flip(); 
        }


    }

    void OnCollisionExit2D(Collision2D v_col)
    {
        if (v_col.gameObject.CompareTag("tag_plataforma"))
        {

            v_colidiu = false;
        }

    }

    

}
