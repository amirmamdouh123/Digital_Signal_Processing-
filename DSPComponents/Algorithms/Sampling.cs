using DSPAlgorithms.DataStructures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DSPAlgorithms.Algorithms
{
    public class Sampling : Algorithm
    {
        public int L { get; set; } //upsampling factor
        public int M { get; set; } //downsampling factor
        public Signal InputSignal { get; set; }
        public Signal OutputSignal { get; set; }
        public Signal TestSignal { get; set; }
        public Signal FirSignal { get; set; }




        public override void Run()
        {
            FIR FIR = new FIR();
            FIR.InputFilterType = DSPAlgorithms.DataStructures.FILTER_TYPES.LOW;
            FIR.InputFS = 8000;
            FIR.InputStopBandAttenuation = 50;
            FIR.InputCutOffFrequency = 1500;
            FIR.InputTransitionBand = 500;

            List<float> up_output = new List<float>();
            List<float> down_output = new List<float>();


            if (M == 0 && L != 0)//up sample by factor L and apply low pass filter
            {
                for (int i = 0; i < InputSignal.Samples.Count; i++)
                {
                    up_output.Add(InputSignal.Samples[i]);
                    for (int j = 0; j < L - 1; j++)
                    {
                        up_output.Add(0);
                    }
                }
                TestSignal = new Signal(up_output, false);
                FIR.InputTimeDomainSignal = TestSignal;
                FIR.Run();
                OutputSignal = FIR.OutputYn;

            }

            else if (M != 0 && L == 0)
            {
                FIR.InputTimeDomainSignal = TestSignal;
                FIR.Run();
                FirSignal = FIR.OutputYn;
                for (int i = 0; i < FirSignal.Samples.Count; i += M)
                {
                    down_output.Add(FirSignal.Samples[i]);
                }
                OutputSignal = new Signal(down_output, false);
            }

            else if (M != 0 && L != 0)
            {
                for (int i = 0; i < InputSignal.Samples.Count; i++)
                {
                    up_output.Add(InputSignal.Samples[i]);
                    for (int j = 0; j < L - 1; j++)
                    {
                        up_output.Add(0);
                    }
                }
                TestSignal = new Signal(up_output, false);
                FIR.InputTimeDomainSignal = TestSignal;
                FIR.Run();
                FirSignal = FIR.OutputYn;
                for (int i = 0; i < FirSignal.Samples.Count; i += M)
                {
                    down_output.Add(FirSignal.Samples[i]);
                }
                OutputSignal = new Signal(down_output, false);
            }
            else
            {
                Console.WriteLine("Error");
            }
        }
    }

}