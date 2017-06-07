﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace ResourcesServerGit
{
    public class Question
    {
        string questionString;

        public string QuestionString
        {
            get { return questionString; }
            set { questionString = value; }
        }
        string questionWord;

        public string QuestionWord
        {
            get { return questionWord; }
            set { questionWord = value; }
        }
        string[] possiblities;

        public string[] Possiblities
        {
            get { return possiblities; }
            set { possiblities = value; }
        }
        string correctAnswer;

        public string CorrectAnswer
        {
            get { return correctAnswer; }
            set { correctAnswer = value; }
        }
        public Question() {}
    }
    public class QuestionBase
    {
        List<Question> questions;
        public enum QuestionTypes
        {
            TOEFL,
            ESL
        }
        //comment

        public QuestionBase(string filename, QuestionTypes type)
        {
            switch(type)
            {
                case QuestionTypes.ESL:
                    foreach(string line in File.ReadAllLines(filename))
                    {
                        Regex qWordRE= new Regex(@".*\[(.*)\].*");
                        Regex pRE = new Regex(".*|(.*)[|\"]");
                        Regex cRE = new Regex(".*:(.*)");
                        Regex qRE = new Regex("\"(.*)\\.|");
                        
                        if (qWordRE.Match(line).Groups.Count != 1) throw new Exception("more than one question word");
                        Console.Write(qWordRE.Matches(line)[0].Groups[0].Value);
                        Console.Write(pRE.Matches(line).Count);
                        if (pRE.Match(line).Groups.Count != 4) throw new Exception("number of possibilities incorrect");
                        if (cRE.Match(line).Groups.Count != 1) throw new Exception("more than one correctAnswer");
                        if (qRE.Match(line).Groups.Count != 1) throw new Exception("more than one question");

                    }
                    break;
            }
        }
    }
}
