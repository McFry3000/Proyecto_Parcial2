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
        LoadQuestion();
        CheckPlayerState();
    }



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
        if (CheckPlayerState())
        {
            if (currentQuestion < questionAmount)
            {
                bool isCorrect = currentLesson.opciones[CorrectAnswerfromUser] == correctAnswer;

                AnswerContainer.SetActive(true);
                if(isCorrect)
                {
                    AnswerContainer.GetComponent<Image>().color = Green;
                    Questiongood.text="Respuesta correcta. " + question + ": " + correctAnswer;
                }
                else
                {

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

    private IEnumerator ShowResultAndLoadQuestion(bool isCorrect)
    {
        yield return new WaitForSeconds(2.5f); //Ajusta el tiempo

        //Oculta el contenedor de respuestas
        AnswerContainer.SetActive(false);

        //cargar pregunta
        LoadQuestion();

        //Activar el boton despues de mostrar el resultado
        //Puedes hacer esto aqui o en LoadQuestion(), dependiendo de tu estructura
        //por ejemplo, si el boton esta en el mismo GameObject que el script
        //GetComponent<Button>().interactable = true;
        CheckPlayerState();
    }


    public void setPlayerAnswer(int _answer)
    {
        CorrectAnswerfromUser = _answer;
    }

    public bool CheckPlayerState()
    {
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
