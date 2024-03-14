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
    public List<Option> opciones; // Lista de opciones para las respuestas
    public TMP_Text Questiontxt; // Texto para mostrar la pregunta
    public TMP_Text Questiongood; // Texto para mostrar el resultado de la respuesta
    public GameObject checkbutton; // Botón para enviar la respuesta
    public GameObject AnswerContainer; // Contenedor para mostrar el resultado de la respuesta
    public Color Green; // Color para respuestas correctas
    public Color Red; // Color para respuestas incorrectas

    void Awake()
    {
        // Garantiza que solo haya una instancia de LevelManager
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
        // Establece la cantidad de preguntas en la lección
        questionAmount = Lesson.leccionList.Count;
        // Carga la primera pregunta
        LoadQuestion();
        // Chequea si el jugador tiene una opción seleccionada
        CheckPlayerState();
    }

    // Carga la siguiente pregunta
    private void LoadQuestion()
    {
        // Asegura que la pregunta actual esté dentro de los límites
        if (currentQuestion < questionAmount)
        {
            // Establece la lección actual
            currentLesson = Lesson.leccionList[currentQuestion];
            // Establece la pregunta y respuesta correcta
            question = currentLesson.lessons;
            correctAnswer = currentLesson.opciones[currentLesson.correctAnswer];
            // Muestra la pregunta en la interfaz de usuario
            Questiontxt.text = question;

            // Itera a través de las opciones y las muestra en la interfaz de usuario
            for(int i = 0; i < currentLesson.opciones.Count; i++)
            {
                opciones[i].GetComponent<Option>().OptionName = currentLesson.opciones[i];
                opciones[i].GetComponent<Option>().OptionId = i;
                opciones[i].GetComponent<Option>().Updatetext();
            }
        }
        else
        {
            Debug.Log("Fin de las preguntas");
        }
    }

    // Función para manejar la siguiente pregunta
    public void NextQuestion()
    {
        // Se verifica si el jugador ha seleccionado una respuesta
        if (CheckPlayerState())
        {
            if (currentQuestion < questionAmount)
            {
                // Verifica si la respuesta del jugador es correcta o incorrecta
                bool isCorrect = currentLesson.opciones[CorrectAnswerfromUser] == correctAnswer;

                // Muestra el contenedor de respuesta
                AnswerContainer.SetActive(true);
                if(isCorrect)
                {
                    // Muestra la respuesta correcta en verde
                    AnswerContainer.GetComponent<Image>().color = Green;
                    Questiongood.text = "Respuesta correcta. " + question + ": " + correctAnswer;
                }
                else
                {
                    // Muestra la respuesta incorrecta en rojo
                    AnswerContainer.GetComponent<Image>().color = Red;
                    Questiongood.text = "Respuesta Incorrecta. " + question + ": " + correctAnswer;
                }

                // Incrementa el índice de la pregunta actual
                currentQuestion++;

                // Muestra el resultado durante un tiempo y carga la siguiente pregunta
                StartCoroutine(ShowResultAndLoadQuestion(isCorrect));

                // Reinicia la respuesta del jugador
                CorrectAnswerfromUser = 9;
            }
            else
            {
                // Cambia de escena (acción a implementar)
            }
        }
    }

    // Corrutina para mostrar el resultado y cargar la siguiente pregunta
    private IEnumerator ShowResultAndLoadQuestion(bool isCorrect)
    {
        yield return new WaitForSeconds(2.5f); // Espera un tiempo antes de mostrar el resultado

        // Oculta el contenedor de respuestas
        AnswerContainer.SetActive(false);

        // Carga la siguiente pregunta
        LoadQuestion();

        // Activa el botón después de mostrar el resultado
        CheckPlayerState();
    }

    // Establece la respuesta del jugador
    public void setPlayerAnswer(int _answer)
    {
        CorrectAnswerfromUser = _answer;
    }

    // Verifica si el jugador ha seleccionado una respuesta y actualiza el estado del botón
    public bool CheckPlayerState()
    {
        if (CorrectAnswerfromUser != 9)
        {
            // Activa el botón si el jugador ha seleccionado una respuesta
            checkbutton.GetComponent<Button>().interactable = true;
            checkbutton.GetComponent<Image>().color = Color.white; // Cambia el color del botón
            return true;
        }
        else
        {
            // Desactiva el botón si el jugador no ha seleccionado una respuesta
            checkbutton.GetComponent<Button>().interactable = false;
            checkbutton.GetComponent<Image>().color = Color.grey; // Cambia el color del botón
            return false;
        }
    }
}
