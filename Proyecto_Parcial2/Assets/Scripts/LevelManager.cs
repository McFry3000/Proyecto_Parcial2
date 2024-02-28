using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LevelManager : MonoBehaviour
{
    [Header("Level Data")]
    public Subject Lesson;
    [Header("Game Configuraxtion")]
    public int questionAmount;
    public int currentQuestion;
    public string question;
    public string correctAnswer;
    [Header("Current Lesson")]
    public Leccion currentLesson;
    [Header("User Interface")]
    public TMP_Text Questiontxt;

    void Start()
    {
        //Establecemos la cantidad de preguntas en la leccion
        questionAmount = Lesson.leccionList.Count;
        //Leccion actual
        currentLesson = Lesson.leccionList[currentQuestion];
        //pregunta
        question = currentLesson.lessons;
        //respuesta correcta 
        correctAnswer = currentLesson.opciones[currentLesson.correctAnswer];
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
         
        }
        else
        {
            //llegamos al final
            Debug.Log("Fin de las preguntas");
        }
    }

    public void NextQuestion()
    {
        if(currentQuestion < questionAmount)
        {
            //incrementamos el indice
            currentQuestion++;
            //carga nueva
            LoadQuestion();
        }
        else
        {
            //cambio de escena
        }
    }
}
