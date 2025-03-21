using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Proje_Part_2
{
    public class Neuron
    {
        public double weight1;
        public double weight2;

        public Neuron()
        {
            Random r = new Random();
            weight1 = r.NextDouble();
            weight2 = r.NextDouble();
        }
    }

    internal class Program
    {
        static void Main(string[] args)
        {
            //dataseti[0] --> Çalışma Süresi, dataseti[1] --> Devam Süresi, dataseti[2] --> Gercek Not
            double[,] dataSeti = {{7.6, 11, 77},
                            {8, 10, 70},
                            {6.6, 8, 55},
                            {8.4, 10, 78},
                            {8.8, 12, 95},
                            {7.2, 10, 67},
                            {8.1, 11, 80},
                            {9.5, 9, 87},
                            {7.3, 9, 60},
                            {8.9, 11, 88},
                            {7.5, 11, 72},
                            {7.6, 9, 58},
                            {7.9, 10, 70},
                            {8, 10, 76},
                            {7.2, 9, 58},
                            {8.8, 10, 81},
                            {7.6, 11, 74},
                            {7.5, 10, 67},
                            {9, 10, 82},
                            {7.7, 9, 62},
                            {8.1, 11, 82}};
            Neuron neuron = new Neuron();
            int epoch = 100;
            double lambda = 0.05;
            double[] weights = new double[] { neuron.weight1, neuron.weight2 };

            Console.WriteLine("Agirlik 1: " + weights[0] + "\n" +
                              "Agirlik 2: " + weights[1] + "\n" +
                              "Epok Degeri: " + epoch + "\n" +
                              "Lambda Degeri: " + lambda + "\n");

            dataSeti = normalizasyon(dataSeti);
            double[] output = hesapla(dataSeti, epoch, lambda, weights);
            double mse = mseHesapla(dataSeti, output);
            ciktiYazdir(dataSeti, output);
            Console.WriteLine($"MSE Degeri: {mse.ToString("0.######")}");
            disardanAlinanlariHesaplaVeYazdir(weights);
            //deneyDoldur(dataSeti, weights); //Deney bölümünde kolay çıktı elde edilimi için yazılmış metod.
        }
        static double[,] normalizasyon(double[,] dataSeti)
        {
            for (int i = 0; i < dataSeti.GetLength(0); i++)
            {
                dataSeti[i, 0] = dataSeti[i, 0] / 10;
                dataSeti[i, 1] = dataSeti[i, 1] / 15;
                dataSeti[i, 2] = dataSeti[i, 2] / 100;
            }

            return dataSeti;
        }
        static double[] hesapla(double[,] dataSeti, int epoch, double lambda, double[] weights)
        {
            double[] output = new double[dataSeti.GetLength(0)];

            for (int i = 0; i < epoch; i++)
            {
                for (int j = 0; j < output.Length; j++)
                {
                    output[j] = weights[0] * dataSeti[j, 0] + weights[1] * dataSeti[j, 1];
                    weights[0] = weights[0] + lambda * (dataSeti[j, 2] - output[j]) * dataSeti[j, 0];
                    weights[1] = weights[1] + lambda * (dataSeti[j, 2] - output[j]) * dataSeti[j, 1];
                }
            }

            return output;

        }
        static double mseHesapla(double[,] dataSeti, double[] outputs)
        {
            double mse = 0;
            double fark = 0;
            for (int i = 0; i < dataSeti.GetLength(0); i++)
            {
                fark = dataSeti[i, 2] - outputs[i];
                mse += fark * fark;
            }

            mse /= dataSeti.GetLength(0);

            return mse;
        }
        static void ciktiYazdir(double[,] dataSeti, double[] outputs)
        {
            Console.WriteLine("1. Input       2. Input       Gercek Not       Neuron Not");
            for (int i = 0; i < dataSeti.GetLength(0); i++)
            {
                Console.Write(dataSeti[i, 0].ToString("N3"));
                Console.Write("          ");
                Console.Write(dataSeti[i, 1].ToString("N3"));
                Console.Write("          ");
                Console.Write(dataSeti[i, 2].ToString("N3"));
                Console.Write("            ");
                Console.Write(outputs[i].ToString("N3"));
                Console.WriteLine();
            }
        }
        static void disardanAlinanlariHesaplaVeYazdir(double[] weights)
        {
            double[,] dataSeti = new double[5, 2];

            Console.Write("Girdileri giriniz: ");
            string girdi = Console.ReadLine();
            string[] inputs = girdi.Split(' ');
            double[] inputsDouble = new double[15];
            int count = 0;

            foreach (string input in inputs)
            {
                inputsDouble[count] = double.Parse(input);
                count++;
            }

            int count1 = 0;
            for (int i = 0; i < 5; i++)
            {
                for (int j = 0; j < 2; j++)
                {
                    if (j == 0)
                    {
                        dataSeti[i, j] = inputsDouble[count1] / 10;
                    }
                    else
                    {
                        dataSeti[i, j] = inputsDouble[count1] / 15;
                    }
                    count1++;
                }
            }

            double[] output = new double[5];

            for (int j = 0; j < 5; j++)
            {
                output[j] = weights[0] * dataSeti[j, 0] + weights[1] * dataSeti[j, 1];
            }

            Console.WriteLine("1. Input       2. Input       Neuron Not");
            for (int i = 0; i < dataSeti.GetLength(0); i++)
            {
                Console.Write(dataSeti[i, 0].ToString("N3"));
                Console.Write("          ");
                Console.Write(dataSeti[i, 1].ToString("N3"));
                Console.Write("          ");
                Console.Write(output[i].ToString("N3"));
                Console.WriteLine();
            }
        }
        static void deneyDoldur(double[,] dataSeti, double[] weights)
        {
            double[] output = new double[dataSeti.GetLength(0)];
            int[] epochs = new int[] { 10, 50, 100 };
            double[] lambdas = new double[] { 0.01, 0.025, 0.05 };
            double[] weightsCopy = new double[2];
            for (int i = 0; i < epochs.Length; i++)
            {
                weightsCopy[0] = weights[0];
                weightsCopy[1] = weights[1];
                int epoch = epochs[i];
                for (int j = 0; j < lambdas.Length; j++)
                {
                    double lambda = lambdas[j];
                    for (int k = 0; k < epoch; k++)
                    {
                        for (int m = 0; m < output.Length; m++)
                        {
                            output[m] = weightsCopy[0] * dataSeti[m, 0] + weightsCopy[1] * dataSeti[m, 1];
                            weightsCopy[0] = weightsCopy[0] + lambda * (dataSeti[m, 2] - output[m]) * dataSeti[m, 0];
                            weightsCopy[1] = weightsCopy[1] + lambda * (dataSeti[m, 2] - output[m]) * dataSeti[m, 1];
                        }
                    }
                    double mse = mseHesapla(dataSeti, output);
                    Console.WriteLine($"{mse.ToString("0.######")}");
                }
            }


        }
    }
}