using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PerguntasVF : MonoBehaviour {

    [Header("Configuração Imagem Botões")]

    public Image altAimg;
    public Image altBimg;

    [Header("Configuração dos Textos")]
    public Image perguntasImg;
    public TextMeshProUGUI infoPergunta;
    //public GameObject perguntasImagem; Mudar modo de jogos depois para imagem
    public TextMeshProUGUI perguntasTxt;

    public TextMeshProUGUI notaFinalTxt;

    public TextMeshProUGUI Mensagem01Text;
    public TextMeshProUGUI Mensagem02Text;

    


    [Header("Configuração Barras")]
    public GameObject barraProgresso;
    public TextMeshProUGUI tempoJogo;
    public GameObject barraTempo;

    [Header("Configuração Botões")]
    public Button[] botoes;
    public Color corAcerto, corErro;

    [Header("Configuração Modo Jogo")]
    public bool perguntasComImagem;
    public bool utilizarAlternativasImagem;
    public bool perguntasAleat;
    public bool jogarComTempo;
    public float tempoResponder;
    public bool mostarCorreta;
    public int quantPiscar;




    [Header("Configuração Perguntas")]
    public string[] perguntas;
    public Sprite[] perguntasImage;
    public string[] correta;
    public int quantPerguntas;
    public List<int> listaPerguntas;

    [Header("Configuração Alternativas Imagens")]
    public Sprite[] alternativaAimg;
    public Sprite[] alternativaBimg;

    [Header("Configuração dos Paineis")]
    public GameObject[] paineis;
    public GameObject[] estrela;



    [Header("Configuração dos Mensagens")]
    public string[] mensagem1;
    public string[] mensagem2;
    public Color[] corMensagem;



    private int idResponder;

    private float quantRespondida;

    private float percProgresso;

    private float notaFinal;

    public float valorQuestoes;

    private int quantAcertos;

    private float percTempo;

    private float tempoTime;

    public int notaMin1Star;
    public int notaMin2Star;

    private int nEstrelas;

    private int idNivel;

    private int idBtnCorreta;

    private bool exibindoCorreta;


    private controleSom somController;

    // Start is called before the first frame update
    void Start() {
        idNivel = PlayerPrefs.GetInt("idNivel");

        somController = FindAnyObjectByType(typeof(controleSom)) as controleSom;

        notaMin1Star = PlayerPrefs.GetInt("nomeMin1Estrela");
        notaMin2Star = PlayerPrefs.GetInt("nomeMin2Estrela");


        barraTempo.SetActive(false);

        if(perguntasComImagem == true) {
            montarListPertuntasIMG();
        }
        else {
            montarListPertuntas();
        }
        

     

        progressaoBarra();
        controlleBarraTempo();

        paineis[0].SetActive(true);
        paineis[1].SetActive(false);





    }

    // Update is called once per frame
    void Update() {
        if (tempoJogo == true && exibindoCorreta == false) {
            tempoTime += Time.deltaTime;
            controlleBarraTempo();

            if (tempoTime >= tempoResponder) {
                proximaPergunta();
            }
        }
    }

    //Esta função é a responsável em montar a lista de perguntas a serem repondida
    public void montarListPertuntas() {

        if (quantPerguntas > perguntas.Length) {
            quantPerguntas = perguntas.Length;
        }

        valorQuestoes = 10 / (float)quantPerguntas;
        //Em caso de modo de jogo com perguntas aleatórias
        if (perguntasAleat == true) {
            bool addPergunta = true;
          

            while (listaPerguntas.Count < quantPerguntas) {
                addPergunta = true;
                int rand = 0;
                rand = Random.Range(0, perguntas.Length);

                foreach (int idPerg in listaPerguntas) {

                    if (idPerg == rand) {
                        addPergunta = false;
                    }
                }
                if (addPergunta == true) {
                    listaPerguntas.Add(rand);
                }
            }
        }//em caso de modo de jogo onde as perguntas não são aleatórias
        else {
            for (int i = 0; i < quantPerguntas; i++) {
                listaPerguntas.Add(i);
            }

            perguntasTxt.text = perguntas[listaPerguntas[idResponder]];

        }

        if(utilizarAlternativasImagem == true) {

            altAimg.sprite = alternativaAimg[listaPerguntas[idResponder]];
            altBimg.sprite = alternativaBimg[listaPerguntas[idResponder]];
        }
    }

    public void montarListPertuntasIMG() {
        //Em caso de modo de jogo com perguntas aleatórias
        if (quantPerguntas > perguntasImage.Length) {
            quantPerguntas = perguntasImage.Length;
        }
        valorQuestoes = 10 / (float)quantPerguntas;


        if (perguntasAleat == true) {
            bool addPergunta = true;
         
        
            valorQuestoes = 10 / (float)quantPerguntas;

            while (listaPerguntas.Count < quantPerguntas) {
                addPergunta = true;
             
                
                  int  rand = Random.Range(0, perguntasImage.Length);
               
                foreach (int idPerg in listaPerguntas) {

                    if (idPerg == rand) {
                        addPergunta = false;
                    }
                }
                if (addPergunta == true) {
                    listaPerguntas.Add(rand);
                }
            }
        }//em caso de modo de jogo onde as perguntas não são aleatórias
        else {

           
                for (int i = 0; i < quantPerguntas; i++) {
                    listaPerguntas.Add(i);
                }
           
           

        }
       
            perguntasImg.sprite = perguntasImage[listaPerguntas[idResponder]];

        if (utilizarAlternativasImagem == true) {

            altAimg.sprite = alternativaAimg[listaPerguntas[idResponder]];
            altBimg.sprite = alternativaBimg[listaPerguntas[idResponder]];
        }

    }
    //Esta função é responsável por processar a respota dada pelo jogador

    public void reponderQuestao(string alternativa) {

        if (exibindoCorreta == true) {

           return;

        }


        if (correta[listaPerguntas[idResponder]] == alternativa) {
            quantAcertos += 1;
            somController.playAcerto();

        }
        else {
            somController.playErro();
        }

        switch (correta[listaPerguntas[idResponder]]) {
            case "A":
            idBtnCorreta = 0;
            break;

            case "B":
            idBtnCorreta = 1;
            break;

        }


        if (mostarCorreta == true) {

            foreach (Button b in botoes) {
                b.image.color = corErro;
            }

            exibindoCorreta = true;
            botoes[idBtnCorreta].image.color = corAcerto;

            StartCoroutine("mostrarAlternativaCorreta");

        }
        else {
            proximaPergunta();
        }

    }

    //Função responsável em processar uma pergunta
    public void proximaPergunta() {

        idResponder += 1;

        tempoTime = 0;

        quantRespondida += 1;

        progressaoBarra();

        if (idResponder < listaPerguntas.Count) {

            if (perguntasComImagem == true) {

                perguntasImg.sprite = perguntasImage[listaPerguntas[idResponder]];

            }
            else {
                perguntasTxt.text = perguntas[listaPerguntas[idResponder]];
            }
            if (utilizarAlternativasImagem == true) {

                altAimg.sprite = alternativaAimg[listaPerguntas[idResponder]];
                altBimg.sprite = alternativaBimg[listaPerguntas[idResponder]];
            }
        }
        else {

            calcularNotaFinal();

        }



    }

    private void progressaoBarra() {

        infoPergunta.text = "Respondeu " + quantRespondida + " de " + listaPerguntas.Count + " perguntas";
        percProgresso = quantRespondida / listaPerguntas.Count;
        barraProgresso.transform.localScale = new Vector3(percProgresso, 1, 1);
    }

    void controlleBarraTempo() {

        if (jogarComTempo == true) {

            barraTempo.SetActive(true);

        }
        else {

            barraTempo.SetActive(false);
        }

        percTempo = ((tempoTime - tempoResponder) / tempoResponder) * -1;

        if (percTempo < 0) {
            percTempo = 0;
        }
        barraTempo.transform.localScale = new Vector3(percTempo, 1, 1);

    }

    void calcularNotaFinal() {

        notaFinal = Mathf.RoundToInt(valorQuestoes * quantAcertos);

        if (notaFinal > PlayerPrefs.GetInt("notaFinal_" + idNivel.ToString())) {

            PlayerPrefs.SetInt("notaFinal_" + idNivel.ToString(), (int)notaFinal);

        }
        if (notaFinal == 10) {
            nEstrelas = 3;
        }
        else if (notaFinal >= notaMin1Star) {
            nEstrelas = 2;
        }
        else if (notaFinal >= notaMin2Star) {
            nEstrelas = 1;
        }
        notaFinalTxt.text = notaFinal.ToString();

        foreach (GameObject e in estrela) {
            e.SetActive(false);
        }

        for (int i = 0; i < nEstrelas; i++) {
            estrela[i].SetActive(true);
        }

        paineis[0].SetActive(false);
        paineis[1].SetActive(true);


    }

    IEnumerator mostrarAlternativaCorreta() {
        for (int i = 0; i < quantPiscar; i++) {
            botoes[idBtnCorreta].image.color = corAcerto;
            yield return new WaitForSeconds(0.2f);
            botoes[idBtnCorreta].image.color = Color.white;
            yield return new WaitForSeconds(0.2f);
        }



        foreach (Button b in botoes) {
            b.image.color = Color.white;
        }

        exibindoCorreta = false;
        proximaPergunta();
    }

}
