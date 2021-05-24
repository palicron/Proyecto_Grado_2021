using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using Random = UnityEngine.Random;


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

    public class QuestionsStatus
    {
        public int[] status;
    }

    static QuestionsStatus paperQuestions = new QuestionsStatus();
    static QuestionsStatus plasticQuestions = new QuestionsStatus();
    static QuestionsStatus glassQuestions = new QuestionsStatus();
    static QuestionsStatus metalQuestions = new QuestionsStatus();
        
    QuestionsStatus[] questionsStatus = { paperQuestions, plasticQuestions, glassQuestions, metalQuestions };

    public TextAsset questionsJSON;

    [System.Serializable]
    public class Question
    {
        public string question;
        public string[] answers;
        public int res;
    }

    [System.Serializable]
    public class QuestionSet
    {
        public Question[] questionSet;
    }

    [System.Serializable]
    public class Questions
    {
        public QuestionSet[] questions;
    }

    public Questions questions = new Questions();

    public GameObject minigameUI;
    public GameObject mainMenu;
    public GameObject quizScreen;
    public GameObject resultScreen;
    public GameObject iconBonus;
    public Image bonusSprite;

    Color green = new Color32(66, 255, 66, 255);

    Color red = new Color32(255, 46, 40, 255);

    Animator anim;

    void Start()
    {
        questions = JsonUtility.FromJson<Questions>(questionsJSON.text);
        questionsStatus[0].status = PlayerPrefsX.GetIntArray("PaperQ", 0, questions.questions[0].questionSet.Length);
        questionsStatus[1].status = PlayerPrefsX.GetIntArray("PlasticQ", 0, questions.questions[1].questionSet.Length);
        questionsStatus[2].status = PlayerPrefsX.GetIntArray("GlassQ", 0, questions.questions[2].questionSet.Length);
        questionsStatus[3].status = PlayerPrefsX.GetIntArray("MetalQ", 0, questions.questions[3].questionSet.Length);
        anim = iconBonus.GetComponent<Animator>();
    }

    void OnDestroy()
    {
        PlayerPrefsX.SetIntArray("PaperQ", paperQuestions.status);
        PlayerPrefsX.SetIntArray("PlasticQ", plasticQuestions.status);
        PlayerPrefsX.SetIntArray("GlassQ", glassQuestions.status);
        PlayerPrefsX.SetIntArray("MetalQ", metalQuestions.status);
    }

    private bool IsEmptyArray(int[] list)
    {
        return !(Array.Exists(list, e => e == 0));
    }

    private static bool isTrue(bool b)
    {
        return b;
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
            anim.SetBool("correct", true);
            questionsStatus[actualMaterial].status[actualQuestion] = 1;
            UI_SFX.instance.PlayAnswer(true);
        }
        else
        {
            UI_SFX.instance.PlayAnswer(false);
        }
        resultGmO.text = resultText;

    }

    public void ShowQuestion()
    {
        QuestionsStatus selectedMaterial = questionsStatus[actualMaterial];
        if (IsEmptyArray(selectedMaterial.status))
        {
            ErrorDialog.instance.ThrowError("Ya completaste todas las preguntas de esta categoría");
            return;
        }
        mainMenu.SetActive(false);
        resultScreen.SetActive(false);
        quizScreen.SetActive(true);
        Question[] questionList = questions.questions[actualMaterial].questionSet;
        actualQuestion = Random.Range(0, questionList.Length);
        Question selectedQuestion = questionList[actualQuestion];
        Debug.Log(selectedMaterial.status.Length);
        questionGmO.text = selectedQuestion.question;
        for(int i = 0; i < 4; i++)
        {
            answersGmO[i].text = selectedQuestion.answers[i];
        }
        actualAnswer = selectedQuestion.res;
    }
}
