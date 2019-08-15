using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Personagem : MonoBehaviour
{

    public float v_forcaPulo;
    public AudioClip v_PuloAudioClip;
    public bool v_nochao;
    public bool v_naAgua;
    public AudioClip v_TrampolimAudioClip;
    public AudioClip v_AnelAudioClip;
    public AudioClip v_VidaAudioClip;
	public AudioClip v_FaseAudioClip;
	public AudioClip v_ChaveAudioClip;

    // Váriaveis contadoras

    public int v_n_vidas;
    public int v_n_aneis;
	public int v_n_chaves;

    // Caixas de texto dos objetos

    public Text txt_vidas;
    public Text txt_aneis;
    public Text txt_chaves;

    Camera v_cam;
    

    // Use this for initialization
    void Start()
    {
        v_n_vidas = 3;
        txt_aneis.text = v_n_aneis.ToString(); // converte váriavel (v_n_aneis) para texto
        txt_vidas.text = v_n_vidas.ToString(); // converte váriavel (v_n_vidas) para texto
        txt_chaves.text = v_n_chaves.ToString(); // converte váriavel (v_n_chaves) para texto
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.Space)) // Faz boneco saltar quando teclar espaço
        {
            GetComponent<Rigidbody2D>().AddForce(new Vector2(0, 200)); // Adiciona força no salto do boneco
        }


        Rigidbody2D v_ridiBody = GetComponent<Rigidbody2D>();

        float v_movimento = Input.GetAxis("Horizontal"); // Cria um movimento na horizontal eixo x

        v_ridiBody.velocity = new Vector2(v_movimento * 2, v_ridiBody.velocity.y);

        if (v_movimento < 0) // Se personagem andando para esquerda
        {
            GetComponent<SpriteRenderer>().flipX = true; // Aplique o flip
        }
        else if (v_movimento > 0) // Se personagem andando para direita
        {
            GetComponent<SpriteRenderer>().flipX = false; // Não aplique o flip
        }
        // Fazendo o personagem andar
        if (v_movimento > 0 || v_movimento < 0)
        {
            GetComponent<Animator>().SetBool("Se_Andando", true);
        }
        else
        {
            GetComponent<Animator>().SetBool("Se_Andando", false);
        }

        // Fazendo o personagem pular (Com animação)
        if (v_nochao)
        {
            GetComponent<Animator>().SetBool("Se_Pulando", false);
        }
        else
        {
            GetComponent<Animator>().SetBool("Se_Pulando", true);
        }

        // Fazendo o personagem Nadar (Com animação)
        if (v_naAgua)
        {
            GetComponent<Animator>().SetBool("Se_Nadando", true); // define checkbox true.
        }
        else
        {
            GetComponent<Animator>().SetBool("Se_Nadando", false); // define checkbox false.
        }
        
        // Fazendo o personagem pular
        if (Input.GetKeyDown(KeyCode.Space)) // Ao pressionar a tecla espaço o Personagem pula.
        {
            v_ridiBody.AddForce(new Vector2(0, v_forcaPulo)); // Adicionar força ao pulo eixo x
            GetComponent<AudioSource>().PlayOneShot(v_PuloAudioClip); // Toca o som do pulo
        }
    }
    // Verifica se o personagem está colidindo com a plataforma
    void OnCollisionEnter2D(Collision2D v_colisao)
    {
       
        if (v_colisao.gameObject.CompareTag("tag_plataforma"))
        {
            
            v_nochao = true;
        }

        if (v_colisao.gameObject.CompareTag("tag_trampolim")) // Verifica se colide com trampolim
        {
            GetComponent<Rigidbody2D>().velocity = new Vector2(0f, 6f);
            GetComponent<AudioSource>().PlayOneShot(v_TrampolimAudioClip);

        }

        if (v_colisao.gameObject.CompareTag("tag_inimigo")) // Verifica se colide com inimigo
        {
            v_n_vidas--;  // diminui quantidade de vidas
            txt_vidas.text = v_n_vidas.ToString();
            if (v_n_vidas == 0)
            {
                GetComponent<AudioSource>().PlayOneShot(v_VidaAudioClip); // toca o som de perda de vida
                Pausar();
            }
        }

    }

    // Verifica se o personagem não está colidindo com a plataforma

    void OnCollisionExit2D(Collision2D v_colisao)
    {
        
        if (v_colisao.gameObject.CompareTag("tag_plataforma"))
        {
            
            v_nochao = false;
        }
    }
    void OnTriggerEnter2D(Collider2D v_colisao)
    {
        
        if (v_colisao.gameObject.CompareTag("tag_quadrado"))
        {
            v_naAgua = true;
        }
        
		if (v_colisao.gameObject.CompareTag("tag_anel"))
        {
            Destroy(v_colisao.gameObject); // faz o anel desaparecer
            GetComponent<AudioSource>().PlayOneShot(v_AnelAudioClip); // Toca o som de coleta do anel
            v_n_aneis++; // incrementa o valor de anéis
            txt_aneis.text = v_n_aneis.ToString(); // atualiza a caixa de texto aneis
        }
		
		if (v_colisao.gameObject.CompareTag("tag_chave")) 
		{
			Destroy(v_colisao.gameObject);
			GetComponent<AudioSource>().PlayOneShot(v_ChaveAudioClip);
			v_n_chaves++;
            txt_chaves.text = v_n_chaves.ToString(); // atualiza caixa texto chaves
        }
		
		
		// mudança de fase após colisão com seta (somente se tiver coletado 12 moedas e 1 chave)
		if (v_colisao.gameObject.CompareTag("tag_novafase") && v_n_aneis > 0 && v_n_chaves > 0) // >11 >0
        {
			GetComponent<AudioSource>().PlayOneShot(v_FaseAudioClip);
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex +1);        
        }
		
                
    }
    void OnTriggerExit2D(Collider2D v_colisao)
    {
        
        if (v_colisao.gameObject.CompareTag("tag_quadrado"))
        {
            v_naAgua = false;
        }

    }
    void Pausar()
    {
        v_cam = Camera.main;
        v_cam.gameObject.GetComponent<AudioSource>().Stop();
        Time.timeScale = 0; // pausar o jogo 0=para e 1=inicia
    }
}
