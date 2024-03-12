using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]

//Crea un Scriptable Object que sirve para crear las lecciones, asi como las preguntas y las opciones de las mismas, la informacio de dicho SO puede ser alterada sin niguna preocupacion de que el siguiente codigo se vea afectado
    public class Leccion
    {
        public int ID;
        public string lessons;
        public List<string> opciones;
        public int correctAnswer;
    }

