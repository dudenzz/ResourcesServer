﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Sandbox
{
    class Program
    {
        static void Main(string[] args)
        {
            string test = "\"You know that I do not [approve] teen's smoking.|support|boast|scorn|anger\":support";
            Regex qWordRE = new Regex(@".*\[(.*)\].*");
            Regex pRE = new Regex(".*|(.*)[|\"]");
            Regex cRE = new Regex(".*:(.*)");
            Regex qRE = new Regex("\"(.*)\\.|");
            var v = qWordRE.Match(test);
            foreach(var group in qWordRE.Match(test).Groups)
            {

            }

        }
    }
}
