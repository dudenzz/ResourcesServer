using Server;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace ResourcesServerGit
{
    public class Question
    {
        public override string ToString()
        {
            string returnString = QuestionString + "\n";
            for (int i = 0; i < possiblities.Length; i++)
                returnString += "\t" + i.ToString() + ") " + possiblities[i] + "\n";
            returnString += "Correct answer: " + correctAnswer;
            return returnString;
        }
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
    public class AnsweredQuestion
    {
        Question q;

        public Question Q
        {
            get { return q; }
            set { q = value; }
        }
        bool correct;

        public bool Correct
        {
            get { return correct; }
            set { correct = value; }
        }
        double[] similarities;

        public double[] Similarities
        {
            get { return similarities; }
            set { similarities = value; }
        }
        public AnsweredQuestion(Question q, double[] similarities)
        {
            this.q = q;
            this.similarities = similarities;
            double max = -100;
            int chosen = -1;
            for(int i = 0; i<q.Possiblities.Length; i++)
            {
                if(similarities[i] > max)
                {
                    chosen = i;
                    max = similarities[i];
                }
            }
            if (q.Possiblities[chosen].Equals(q.CorrectAnswer))
                correct = true;
        }
    }
    public class QuestionBase
    {
        bool modelAssigned = false;
        bool classifierAssigned = false;
        List<AnsweredQuestion> answeredQuestions = new List<AnsweredQuestion>();

        public List<AnsweredQuestion> AnsweredQuestions
        {
            get { return answeredQuestions; }
            set { answeredQuestions = value; }
        }
        IClassifier classifier;
        Model model;
        string qBname;

        public string QBname
        {
            get { return qBname; }
            set { qBname = value; }
        }
        public bool ClassifierAssigned
        {
            get { return classifierAssigned; }
            set { classifierAssigned = value; }
        }
        public bool ModelAssigned
        {
            get { return modelAssigned; }
            set { modelAssigned = value; }
        }
        List<Question> questions;
        public enum QuestionTypes
        {
            TOEFL,
            ESL,
            INVALID
        }
        //comment

        public QuestionBase(string filename, string name, QuestionTypes type)
        {
            questions = new List<Question>();
            qBname = name;
            switch(type)
            {
                case QuestionTypes.ESL:
                    foreach(string line in File.ReadAllLines(filename))
                    {
                        Regex qWordRE = new Regex(@".*\[(.*)\].*");
                        Regex pRE = new Regex(".*\\|([a-z]+)\\|([a-z]+)\\|([a-z]+)\\|([a-z]+)\\\".*");
                        Regex cRE = new Regex(".*:(.*)");
                        Regex qRE = new Regex("\"([^\\|]*\\.?) ?\\|");
                        
                        if (qWordRE.Match(line).Groups.Count != 2) throw new Exception("more than one question word");
                        if (pRE.Match(line).Groups.Count != 5) throw new Exception("number of possibilities incorrect");
                        if (cRE.Match(line).Groups.Count != 2) throw new Exception("more than one correctAnswer");
                        if (qRE.Match(line).Groups.Count != 2) throw new Exception("more than one question");

                        string qWord = qWordRE.Match(line).Groups[1].Value;
                        string[] p = new string[] { 
                            pRE.Match(line).Groups[1].Value, 
                            pRE.Match(line).Groups[2].Value, 
                            pRE.Match(line).Groups[3].Value, 
                            pRE.Match(line).Groups[4].Value };
                        string correct = cRE.Match(line).Groups[1].Value;
                        string question = qRE.Match(line).Groups[1].Value;
                        Question q = new Question();
                        q.CorrectAnswer = correct;
                        q.Possiblities = p;
                        q.QuestionString = question;
                        q.QuestionWord = qWord;
                        questions.Add(q);
                        Console.WriteLine(q);
                    }
                    break;
                case QuestionTypes.TOEFL:
                    //todo
                    break;
                case QuestionTypes.INVALID:
                    //todo
                    break;
                default:
                    //todo
                    break;
            }
        }
        public void assignClassifier(IClassifier c)
        {
            classifier = c;
        }
        public void assignModel(Model m)
        {
            model = m;
        }
        public void answerQuestions()
        {
            foreach (Question q in questions)
            {
                answeredQuestions.Add(new AnsweredQuestion(q,classifier.answerQuestion(q, model)));
            }
        }

    }
}
