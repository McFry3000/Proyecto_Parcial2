using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using static UnityEditor.Progress;

public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance;

    [Header("Level Data")]
    public Subject Lesson;

    [Header("Game Configuration")]
    public int questionAmount = 0;
    public int currentQuestion = 0;
    public string question;
    public string correctAnswer;
    public int CorrectAnswerfromUser = 9;

    [Header("Current Lesson")]
    public Leccion currentLesson;

    [Header("User Interface")]
    public List<Option> opciones;
    public TMP_Text Questiontxt;
    public TMP_Text Questiongood;
    public GameObject checkbutton;
    public GameObject AnswerContainer;
    public Color Green;
    public Color Red;

    void Awake()
    {
        if (Instance != null)
        {
            return;
        }
        else
        {
            Instance = this;
        }
    }
    void Start()
    {
        //Establecemos la cantidad de preguntas en la leccion
        questionAmount = Lesson.leccionList.Count;
        //Crga la pregunta inicial
        LoadQuestion();
        //checa si el jugador tiene una opcion seleccionada
        CheckPlayerState();
    }


//se carga la pregunta, teniendo en cuenta si el jugador yacompleto la pregunta anterior
   private void LoadQuestion()
    {

        //aseguramos que la pregunta actual este dentro de los limites
        if (currentQuestion < questionAmount)
        {
            //Establecemos la leccion actual
            currentLesson = Lesson.leccionList[currentQuestion];
            //ERstablecemos la pregunta
            question = currentLesson.lessons;
            //respuesta correcta
            correctAnswer = currentLesson.opciones[currentLesson.correctAnswer];
            //Pregunta en UI
            Questiontxt.text = question;

            for(int i =0; i < currentLesson.opciones.Count; i++)
            {
            //sirve para iterar a traves de una lista de opciones, usando sus identificadores provenientes desde el script option
                opciones[i].GetComponent<Option>().OptionName = currentLesson.opciones[i];
                opciones[i].GetComponent<Option>().OptionId =i;
                opciones[i].GetComponent<Option>().Updatetext();
            
            }
        }
        else
        {
            //llegamos al final
            Debug.Log("Fin de las preguntas");
        }
    }

    public void NextQuestion()
    {
    //se checa la respuesta que selecciono el jugador
        if (CheckPlayerState())
        {
            if (currentQuestion < questionAmount)
            {
            //vhecamos si la pregunta es correcta o no, dependiendo de esto, se abre un "if"
                bool isCorrect = currentLesson.opciones[CorrectAnswerfromUser] == correctAnswer;

                AnswerContainer.SetActive(true);
                if(isCorrect)
                {
                //si la respuesta es correcta, semostrara en la UI el color verde dentro de un recuadro vacio, junto con el texto siguiente
                    AnswerContainer.GetComponent<Image>().color = Green;
                    Questiongood.text="Respuesta correcta. " + question + ": " + correctAnswer;
                }
                else
                {
                //si la resouesta es incorrecta, se mostrara en la UI el color rojo dentro del recuadro vacio, junto con el texto siguiente

                    AnswerContainer.GetComponent<Image>().color = Red;
                    Questiongood.text = "Respuesta Incorrecta. " + question + ": " + correctAnswer;
                }

                //Incrementamos el indice de la pregunta actual
                currentQuestion++;

                //Mostrar el resultado durante un tiempo

                StartCoroutine(ShowResultAndLoadQuestion(isCorrect));

                //Reset respuesta del jugador
                CorrectAnswerfromUser = 9;

            }
            else
            {
                //cambio de escena
            }
        }
    }
//funcion que inicia la corrutina, la cual funciona en paralelo al codigo anterior
    private IEnumerator ShowResultAndLoadQuestion(bool isCorrect)
    {
        yield return new WaitForSeconds(2.5f); //Ajusta el tiempo para mostrar el resultado

        //Oculta el contenedor de respuestas
        AnswerContainer.SetActive(false);

        //cargar pregunta siguiente
        LoadQuestion();

        //Activar el boton despues de mostrar el resultado
        //Puedes hacer esto aqui o en LoadQuestion(), dependiendo de tu estructura
        //por ejemplo, si el boton esta en el mismo GameObject que el script
        //GetComponent<Button>().interactable = true;
        CheckPlayerState();
    }

//se asgina la respuesta del jugador
    public void setPlayerAnswer(int _answer)
    {
    /7actualiza la respuesta del jugador con el valorproporcionado
        CorrectAnswerfromUser = _answer;
    }
//funcion que evalua si el juagdor interactua con el boton para hacer el modo hover 
    public bool CheckPlayerState()
    {
    //si el jugador interactua con el boton, camvbiara de color
        if (CorrectAnswerfromUser != 9)
        {
        
            checkbutton.GetComponent<Button>().interactable = true;
            checkbutton.GetComponent<Image>().color = Color.white;
            return true;
        }
        else
        {
            checkbutton.GetComponent<Button>().interactable = false;
            checkbutton.GetComponent<Image>().color = Color.grey;
            return false;
        }

    }
}
