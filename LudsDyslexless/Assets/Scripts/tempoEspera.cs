using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class tempoEspera : MonoBehaviour
{
    public int tempoEspra;
    public string nomeCena;

    void Start()
    {
        StartCoroutine("esperar");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    IEnumerator esperar() {

        yield return new WaitForSeconds(tempoEspra);

        SceneManager.LoadScene(nomeCena);
    }
}
