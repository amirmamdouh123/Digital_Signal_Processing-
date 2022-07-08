using DSPAlgorithms.DataStructures;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DSPAlgorithms.Algorithms
{
    public class Derivatives : Algorithm
    {
        public Signal InputSignal { get; set; }
        public Signal FirstDerivative { get; set; }
        public Signal SecondDerivative { get; set; }

        public override void Run()
        {
            List<float> firtemp = new List<float>();
            List<float> xtemp = new List<float>();
            List<float> res1 = new List<float>();
            List<float> res2 = new List<float>();
            Signal F = new Signal(InputSignal.Samples, false);
            Signal F2 = new Signal(InputSignal.Samples, false);
            float N = InputSignal.Samples.Count;
            for (int i = 0; i < InputSignal.Samples.Count; i++)
            {
                // Shift Right
                F.SamplesIndices[i] = F.SamplesIndices[i] + 1;
                // Shift Left
                F2.SamplesIndices[i] = F2.SamplesIndices[i] - 1;
            }
            firtemp.Add(0);
            for(int i = 0; i < InputSignal.Samples.Count; i++)
            {
                firtemp.Add(F.Samples[i]);
            }
            for (int i = 0; i < InputSignal.Samples.Count-1; i++)
            {
                res1.Add(InputSignal.Samples[i] - firtemp[i]);
            }
            FirstDerivative = new Signal(res1, false);
            xtemp.Add(0);
            for (int i = 0; i < InputSignal.Samples.Count; i++)
            {
                xtemp.Add(InputSignal.Samples[i]);
            }
            for (int i = 0; i < InputSignal.Samples.Count-1; i++)
            {
                res2.Add(F2.Samples[i] - xtemp[i] - res1[i]);
            }
            SecondDerivative = new Signal(res2, false);
        }
    }
}