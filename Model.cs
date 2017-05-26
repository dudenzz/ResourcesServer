using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server
{
    class Model
    {
        Dictionary<string, float[]> vectors;
        int size = 0;
        int current = 0;
        int dim = 0;
        public delegate void read10000del(int current, int size);
        public event read10000del read10000;
        void readLine(string line)
        {
            if (current % 10000 == 0)
            {
                Console.WriteLine("Reading vectors from file: currently at vector number " + current.ToString() + "/" + size.ToString() + "(" + (100 * current / (1.0 * size)).ToString() + "%)");
                read10000(current, size);
            } 
            current++;
            string[] tokens = line.Split(' ');
            float[] vec = new float[300];
            for (int i = 1; i < dim + 1; i++)
                vec[i - 1] = float.Parse(tokens[i].Replace('.',','));
            vectors.Add(tokens[0], vec);
        }
        public void read(string filename)
        {
            string line;
            StreamReader lineReader = new StreamReader(filename);
            line = lineReader.ReadLine();
            size = int.Parse(line.Split(' ')[0]);
            dim = int.Parse(line.Split(' ')[1]);
            while((line = lineReader.ReadLine()) != null)
            {
                readLine(line);
            }
        }
        public Model()
        {
            vectors = new Dictionary<string, float[]>();
        }
    }
}

