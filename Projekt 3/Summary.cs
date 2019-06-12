using System;
using System.Collections.Generic;
using System.Text;

namespace Sort {
    
    public class Summary {
    
        public class Row {
            
            public Sorter.AlgorithmName     Algorithm  { get; private set; }
            public InputGenerator.InputType InputType  { get; private set; }
            public int                      InputCount { get; private set; }
            public TimeSpan                 Time       { get; private set; }

            public Row(Sorter.AlgorithmName algorithm, InputGenerator.InputType inputType, int inputCount, TimeSpan time) {
                Algorithm  = algorithm;
                InputType  = inputType;
                InputCount = inputCount;
                Time       = time;
            }
        }

        private List<Row> Rows;

        public void AddRow(Row row) {
            if (Rows == null) {
                Rows = new List<Row>();
            }
            Rows.Add(row);
        }

        public void Clear() {
            if (Rows == null) {
                Rows = new List<Row>();
            }
            Rows.Clear();
        }

        public Row[] GetRows() {
            if (Rows == null) {
                Rows = new List<Row>();
            }
            return Rows.ToArray();
        }
    }
}
