using Server;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ResourcesServerGit
{
    public interface IClassifier
    {
        double[] answerQuestion(Question q, Model m);
    }
}
