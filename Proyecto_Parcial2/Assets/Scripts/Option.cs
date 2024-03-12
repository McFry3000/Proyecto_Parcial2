using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Option : MonoBehaviour
{
    public int OptionId;
    public string OptionName;
    
//se inicia con las opciones de las primeras preguntas, obteniendo el componente Text Mesh Pro para ponerle las preguntas
    public void Start()
    {
        transform.GetChild(0).GetComponent<TMP_Text>().text = OptionName;
    }

    void Update()
    {
        
    }
//se cambia el texto en UI con las opciones de las siguientes preguntas, de igual manera usando el componente Text Mesh Pro
    public void Updatetext()
    {
        transform.GetChild(0).GetComponent<TMP_Text>().text = OptionName;
    }
    //Checa si se seleccciona una opcion en la UI, usando el script de LevelManager
    public void SelectOptions()
    {
        LevelManager.Instance.setPlayerAnswer(OptionId);
        LevelManager.Instance.CheckPlayerState();
    }
}
