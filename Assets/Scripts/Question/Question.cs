using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum QuestionType
{
    MultiChoice,
    Scalar,
    Text,
}

public class Question
{
    private QuestionType _questionType;
    private string _questionTitle;
    private string _questionAnswer;

    public Question(QuestionType type, string questionTitle, string questionAnswer)
    {
        
    }

    public Question()
    {
        
    }
}
