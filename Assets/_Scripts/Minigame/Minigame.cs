using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Minigame : MonoBehaviour
{
    public Sprite[] icons = new Sprite[4];
    public Text questionGmO;
    public Text[] answersGmO = new Text[4];
    public Text resultGmO;
    int actualMaterial;
    int actualAnswer;
    int actualQuestion;
    // Por convención, se manejará un orden en la lista para las preguntas
    // En la posición 0 las preguntas relacionadas al Papel
    // En la posición 1 las preguntas relacionadas al Plástico
    // En la posición 2 las preguntas relacionadas al Vidrio
    // En la posición 3 las preguntas relacionadas al Metal
    public QuestionList paperQuestions;
    public QuestionList plasticQuestions;
    public QuestionList glassQuestions;
    public QuestionList metalQuestions;
    QuestionList[] questionArray;
    public GameObject minigameUI;
    public GameObject mainMenu;
    public GameObject quizScreen;
    public GameObject resultScreen;
    public GameObject iconBonus;
    public Image bonusSprite;

    Color green = new Color32(66, 255, 66, 255);

    Color red = new Color32(255, 46, 40, 255);

    void Start()
    {
        questionArray = new QuestionList[4] { paperQuestions, plasticQuestions, glassQuestions, metalQuestions };
    }
    public void ExitGame()
    {
        minigameUI.SetActive(false);
        UI_Status.instance.SetOpen(false, MenuType.MiniGame);
        Time.timeScale = 1.0f;
    }

    public void Menu()
    {
        mainMenu.SetActive(true);
        resultScreen.SetActive(false);
    }

    public void SelectMaterial(int i)
    {
        actualMaterial = i;
        ShowQuestion();
    }

    public void SelectAnswer(int p)
    {
        resultScreen.SetActive(true);
        quizScreen.SetActive(false);
        string resultText = "Incorrecto. ¡Sigue intentando!";
        resultGmO.color = red;
        if (p == actualAnswer)
        {
            resultText = "¡Respuesta correcta!";
            resultGmO.color = green;
            PlayerScore.instance.UpdateScore(actualMaterial, 10);
            bonusSprite.sprite = icons[actualMaterial];
            iconBonus.GetComponent<Animator>().SetBool("correct", true);
            questionArray[actualMaterial].list.RemoveAt(actualQuestion);
        }
        resultGmO.text = resultText;

    }

    public void ShowQuestion()
    {
        QuestionList selectedMaterial = questionArray[actualMaterial];
        if (selectedMaterial.list.Count == 0)
        {
            ErrorDialog.instance.ThrowError("Ya completaste todas las preguntas de esta categoría");
            return;
        }
        mainMenu.SetActive(false);
        resultScreen.SetActive(false);
        quizScreen.SetActive(true);
        actualQuestion = Random.Range(0, selectedMaterial.list.Count);
        GameQuestion selectedQuestion = selectedMaterial.list[actualQuestion];
        questionGmO.text = selectedQuestion.question;
        for(int i = 0; i < 4; i++)
        {
            answersGmO[i].text = selectedQuestion.answers[i];
        }
        actualAnswer = selectedQuestion.answer;
    }
}
[System.Serializable]
public class GameQuestion
{
    public string question;
    public string[] answers = new string[4];
    public int answer;
}
[System.Serializable]
public class QuestionList
{
    public List<GameQuestion> list;
}
