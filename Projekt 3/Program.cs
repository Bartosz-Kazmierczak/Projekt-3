using System;
using System.Collections.Generic;
using System.Text;

namespace Sort {

    class Program {

        private const string OUTPUT_PATH = "output.csv";

        private static void Main(string[] args) {

            bool quit = false;
            while (!quit) {
                Console.WriteLine("1. Testy");
                Console.WriteLine("2. Uruchom glowny program");
                Console.WriteLine("3. Zamknij");
                int cmd = int.Parse(Console.ReadLine());
                Console.WriteLine();
                if (cmd == 1) {
                    RunTests();
                } else if (cmd == 2) {
                    RunMainProgram();
                } else {
                    quit = true;
                }
            }
        }

        private static void RunTests() {

            for(int i = 0; i < InputGenerator.AllInputTypes.Length; i++){

                Random rnd = new Random(Guid.NewGuid().GetHashCode());
                int[] inputValues = InputGenerator.Generate(rnd, InputGenerator.AllInputTypes[i], 10, -100000, 100000);

                Console.WriteLine("-------------------------");
                Console.WriteLine("Input type: " + InputGenerator.AllInputTypes[i].ToString());
                Console.WriteLine("-------------------------");
                for(int k = 0; k < inputValues.Length; k++) {
                    Console.Write(inputValues[k].ToString() + " ");
                }
                Console.WriteLine();

                for(int j = 0; j < Sorter.AllAlgorithms.Length; j++) {
                    Sorter.Result result = Sorter.Sort(Sorter.AllAlgorithms[j], inputValues);
                    Console.WriteLine(Sorter.AllAlgorithms[j].ToString() + ": ");
                    for(int k = 0; k < result.Values.Length; k++) {
                        Console.Write(result.Values[k].ToString() + " ");
                    }
                    Console.WriteLine();
                }
                Console.WriteLine();
                Console.WriteLine();
            }
        }

        private static void RunMainProgram() {
            
            Summary summary = new Summary();

            Console.Write("MINIMALNA  WIELKOSC ZBIORU = ");
            int valuesCountMin = int.Parse(Console.ReadLine());
            Console.Write("MAKSYMALNA WIELKOSC ZBIORU = ");
            int valuesCountMax = int.Parse(Console.ReadLine());
            Console.Write("MINIMALNA  WARTOSC = ");
            int valueMin       = int.Parse(Console.ReadLine());
            Console.Write("MAKSYMALNA WARTOSC = ");
            int valueMax       = int.Parse(Console.ReadLine());
            Console.Write("ILOSC PROBEK = ");
            int samplesCount   = int.Parse(Console.ReadLine());

            int diff = valuesCountMax - valuesCountMin;
            int sampleStep = (diff/samplesCount);
            
            Random rnd = new Random(Guid.NewGuid().GetHashCode());
            
            int counterMax = InputGenerator.AllInputTypes.Length * Sorter.AllAlgorithms.Length * samplesCount;
            int counter = 0;
            
            Console.WriteLine();
            Console.WriteLine("Rozpoczynam dzialanie");

            foreach(InputGenerator.InputType inputType in InputGenerator.AllInputTypes){
                for(int i = 0; i < samplesCount + 1; i++) {
                    int inputSize = valuesCountMin + (i * sampleStep);
                    int[] input = InputGenerator.Generate(rnd, inputType, inputSize, valueMin, valueMax);
                    foreach(Sorter.AlgorithmName algorithmName in Sorter.AllAlgorithms) {
                        Sorter.Result result = Sorter.Sort(algorithmName, input);
                        Summary.Row row = new Summary.Row(algorithmName, inputType, inputSize, result.Time);
                        summary.AddRow(row);
                        counter++;
                        Console.WriteLine("[" + counter.ToString() + "/" + counterMax.ToString() + "] " + algorithmName.ToString() + " " + inputType.ToString() + " " + result.Time.TotalMilliseconds.ToString() + " ms");
                    }
                }
            }
            
            string text = string.Empty;
            Summary.Row[] allRows = summary.GetRows();
            for(int i = 0; i < allRows.Length; i++){
                text += allRows[i].Algorithm + ";" 
                    + allRows[i].InputType + ";" 
                    + allRows[i].InputCount + ";" 
                    + allRows[i].Time.TotalMilliseconds.ToString().Replace('.',',') 
                    + System.Environment.NewLine;
            }
            System.IO.File.WriteAllText(OUTPUT_PATH, text);

            Console.WriteLine("Wyniki zostały wyeksportowane do pliku " + OUTPUT_PATH);
            Console.WriteLine("Nacisnij dowolny klawisz, aby kontynuowac.");
            Console.ReadKey();
        } 
    }
}
