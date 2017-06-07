using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server
{

    public class Model
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
            float[] vec = new float[dim];
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
        float[] this[string i]
        {
            get
            {
                return vectors[i];
            }
            set
            {
                vectors[i] = value;
            }
        }
        public double cosine(string w1, string w2)
        {
            if(vectors.Keys.Contains(w1) && vectors.Keys.Contains(w2))
                return cosine(this[w1], this[w2]);
            return 0.0;
        }
        double cosine(float[] v1, float[] v2)
        {
            double totalNominator = 0;
            double totalDenominator1 = 0;
            double totalDenominator2 = 0;
            for(int i = 0; i<dim; i++)
            {
                totalNominator += v1[i] * v2[i];
                totalDenominator1 += v1[i] * v1[i];
                totalDenominator2 += v2[i] * v2[i];
            }
            return totalNominator / (Math.Sqrt(totalDenominator1) * Math.Sqrt(totalDenominator2));
        }
        
        public string[] most_similar(string word, int n)
        {
            string[] ret = new string[n];
            double[] sims = new double[n];
            for (int i = 0; i < n; i++ )
            {
                ret[i] = "";
                sims[i] = -0.1;
            }
            foreach(string w in vectors.Keys)
            {
                if(sims[n-1] < cosine(word,w))
                {
                    sims[n - 1] = cosine(word, w);
                    ret[n] = w;
                    for (int i = 0; i < n; i++)
                    {
                        double max = -1;
                        int m_i = 0;
                        for (int j = i; j < n; j++)
                        {
                            if (sims[n-1] > max)
                            {
                                max = sims[n - 1];
                                m_i = j;
                            }
                        }
                        double temp_d = sims[0];
                        string temp_s = ret[0];
                        sims[0] = sims[m_i];
                        ret[0] = ret[m_i];
                        sims[m_i] = temp_d;
                        ret[m_i] = temp_s;
                    }
                }
            }
            return ret;
        }
    
        
    }
}

