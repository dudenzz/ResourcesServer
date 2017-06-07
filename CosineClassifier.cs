using Server;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ResourcesServerGit
{
    class CosineClassifier : IClassifier
    {
        double[] answerQuestion(Question q, Model m)
        {
            double[] sims = new double[q.Possiblities.Length];
            for(int i = 0; i<q.Possiblities.Length; i++)
            { 
                sims[i] = m.cosine(q.Possiblities[i],q.QuestionWord);
            }
            return sims;
        }
}
