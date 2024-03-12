using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName ="new Subject", menuName = "ScriptableObjects/NewLesson", order =1)]
public class Subject : ScriptableObject
{
//Crea un Scriptable Object que sirve para crear las lecciones, asi como las preguntas y las opciones de las mismas, la informacio de dicho SO puede ser alterada sin niguna preocupacion de que el siguiente codigo se vea afectado
    [Header("GameObject Configuration")]
    public int Lesson = 0;

    [Header ("Lession Quest Configuration")]
    public List<Leccion> leccionList;
}
   
