using Server;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ResourcesServerGit
{
    interface IClassifier
    {
        bool answerQuestion(Question q, Model m);
    }
}
