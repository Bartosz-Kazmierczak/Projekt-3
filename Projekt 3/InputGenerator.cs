using System;
using System.Collections.Generic;
using System.Text;

namespace Sort {

    public class InputGenerator {
    
        public enum InputType {
            Random,
            Increasing,
            Decreasing,
            Const,
            ShapedV,
            ShapedA
        }

        public static InputType[] AllInputTypes = {
            InputType.Random,
            InputType.Increasing,
            InputType.Decreasing,
            InputType.Const,
            InputType.ShapedV,
            InputType.ShapedA
        };

        public static int MinNumberValue;
        public static int MaxNumberValue;

        public static int[] Generate(Random random, InputType type, int size, int minValue, int maxValue) {


            int[] result = new int[size];
            result[0] = random.Next(minValue, maxValue);

            if (type == InputType.Random) {
                for(int i = 1; i < result.Length; i++) {
                    result[i] = random.Next(minValue, maxValue);
                }

            } else if (type == InputType.Increasing) {
                for(int i = 1; i < result.Length; i++) {
                    result[i] = result[i-1] + random.Next(0, 10);
                }

            } else if (type == InputType.Decreasing) {
                for(int i = 1; i < result.Length; i++) {
                    result[i] = result[i-1] - random.Next(0, 10);
                }

            } else if (type == InputType.Const) {
                for (int i = 0; i < result.Length; i++) {
                    result[i] = result[0];
                }

            } else if (type == InputType.ShapedV) {
                int middle = size/2;
                for(int i = 1; i < middle; i++) {
                    result[i] = result[i-1] - random.Next(0, 10);
                }
                for(int i = middle; i < result.Length; i++) {
                    result[i] = result[i-1] + random.Next(0, 10);
                }

            } else if (type == InputType.ShapedA) {
                int middle = size/2;
                for(int i = 1; i < middle; i++) {
                    result[i] = result[i-1] + random.Next(0, 10);
                }
                for(int i = middle; i < result.Length; i++) {
                    result[i] = result[i-1] - random.Next(0, 10);
                }

            }


            return result;
        }
    }
}
