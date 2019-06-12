using System;
using System.Collections.Generic;
using System.Text;

namespace Sort {
    
    public class Sorter {

        public enum AlgorithmName {
            InsertionSort,
            SelectionSort,
            HeapSort,
            CocktailSort,
            QuickSort
        }

        public static AlgorithmName[] AllAlgorithms = {
            AlgorithmName.InsertionSort,
            AlgorithmName.SelectionSort,
            AlgorithmName.HeapSort,
            AlgorithmName.CocktailSort,
            //AlgorithmName.QuickSort
        };

        public class Result {

            public int[] Values    { get; private set; }
            public TimeSpan Time { get; private set; }

            public Result(int[] values, TimeSpan time) {
                Values = values;
                Time   = time;
            }
        }

        public static Result Sort(AlgorithmName algorithm, int[] input) {

            System.Diagnostics.Stopwatch sw = new System.Diagnostics.Stopwatch();
            sw.Start();

            int[] resultValues = null;

            if (algorithm == AlgorithmName.InsertionSort) {
                resultValues = RunInsertionSort(input);
            } else
            if (algorithm == AlgorithmName.SelectionSort) {
                resultValues = RunSelectionSort(input);
            } else
            if (algorithm == AlgorithmName.HeapSort) {
                resultValues = RunHeapSort(input);
            } else
            if (algorithm == AlgorithmName.CocktailSort) {
                resultValues = RunCocktailSort(input);
            } else
            if (algorithm == AlgorithmName.QuickSort) {
                resultValues = RunQuickSort(input);
            }

            sw.Stop();

            Result result = new Result(resultValues, sw.Elapsed);
            return result;
        }
        
        private static int[] RunInsertionSort(int[] input) {
            //http://www.algorytm.org/algorytmy-sortowania/sortowanie-przez-wstawianie-insertionsort/insert-1-cs.html
            int bufor = 0;
            int[] output = new int[input.Length];
            output[0] = input[0];  
            for (int i = 1; i < input.Length; i++) {
                for (int j = i; j > 0; j--) {
                    if (output[j - 1] > input[i]) {                                                             
                        bufor = output[j - 1];                                    
                        output[j - 1] = input[i];
                        output[j] = bufor;
                    } else {
                        output[j] = input[i];                           
                        break;
                    }
                }
            }
            return output;
        }

        private static int[] RunSelectionSort(int[] input) {
            //http://www.algorytm.org/algorytmy-sortowania/sortowanie-przez-wymiane-wybor-selectionsort/select-1-cs.html
            int[] output = new int[input.Length];
            System.Buffer.BlockCopy(input, 0, output, 0, input.Length * 4);
            int bufor;
            for (int i = 0; i < output.Length; i++) {
                for (int j = i + 1; j < output.Length; j++) {
                    if (output[i] >= output[j]) {
                        bufor = output[i];
                        output[i] = output[j];
                        output[j] = bufor;
                    }
                }
            }
            return output;
        }

        private static int[] RunHeapSort(int[] input) {
            //https://www.tutorialspoint.com/heap-sort-in-chash
            int[] output = new int[input.Length];
            System.Buffer.BlockCopy(input, 0, output, 0, input.Length * 4);
            
            for (int i = (output.Length / 2) - 1; i >= 0; i--) {
                heapify(output, output.Length, i);
            }
            for (int i = output.Length-1; i>=0; i--) {
                int temp = output[0]; 
                output[0] = output[i];
                output[i] = temp;
                heapify(output, i, 0); 
            }

            return output;
        }
        
        static void heapify(int[] arr, int n, int i) {
            int largest = i;
            int left = 2*i + 1;
            int right = 2*i + 2;
            if (left < n && arr[left] > arr[largest])
            largest = left;
            if (right < n && arr[right] >    arr[largest]) 
            largest = right;
            if (largest != i) {
            int swap = arr[i];
            arr[i] = arr[largest]; 
            arr[largest] = swap;
            heapify(arr, n, largest);
            }
        }

        private static int[] RunCocktailSort(int[] input) {
            //http://lo28.internetdsl.pl/BabelSort/koktajlowe.htm
            int[] output = new int[input.Length];
            System.Buffer.BlockCopy(input, 0, output, 0, input.Length * 4);

            int i = 0;
            int min = 0;
            int max=input.Length-1;

            bool continueWhile=true;
   
            while(continueWhile) {
                continueWhile=false;
                for(i=min;i < max; i++) {
                    if(output[i]>output[i+1]) {
                       // zamien(i,i+1);
                        int temp = output[i];
                        output[i] = output[i+1];
                        output[i+1] = temp;
                        continueWhile=true;
                    }
                }
                max--;
                for(i = max; i > min; i--){
                    if(output[i] < output[i - 1]) {
                        //zamien(i-1,i);
                        int temp = output[i];
                        output[i] = output[i-1];
                        output[i-1] = temp;
                        continueWhile = true;
                    }
                }
                min++;
            }
            return output;
        }

        
        private static int[] RunQuickSort(int[] input) {
            //http://www.algorytm.org/algorytmy-sortowania/sortowanie-szybkie-quicksort/quick-1-cs.html
            int[] output = new int[input.Length];
            System.Buffer.BlockCopy(input, 0, output, 0, input.Length * 4);
            QuickSort(output, 0, output.Length-1);
            return  output;
        }

        private static void QuickSort(int[] array, int left, int right) {
            var i = left;
            var j = right;
            var pivot = array[(left + right) / 2];
            while (i < j)
            {
            while (array[i] < pivot) i++;
            while (array[j] > pivot) j--;
            if (i <= j)
            {
            // swap
            var tmp = array[i];
            array[i++] = array[j];  // ++ and -- inside array braces for shorter code
            array[j--] = tmp;
            }
            }
            if (left < j) QuickSort(array, left, j);
            if (i < right) QuickSort(array, i, right);
        }
    }
}
