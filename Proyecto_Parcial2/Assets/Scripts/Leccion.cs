using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Leccion
{
    public int ID; // Identificador de la lección
    public string lessons; // Texto de la lección
    public List<string> opciones; // Lista de opciones para la lección
    public int correctAnswer; // Índice de la respuesta correcta
}
