using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Media;

namespace VennDiagrams
{
    class Calculate
    {
        private List<char> formula;
        private Dictionary<Char, CombinedGeometry> shapes;
        private CombinedGeometry result;

        public Calculate(string _formula, Dictionary<Char, CombinedGeometry> simpleShapes)
        {
            formula = new List<Char>(_formula);
            PrepareFormulaToCalculations();
            DictionarySet(simpleShapes);
            result = CalculateResult();
        }

        public List<char> Formula
        {
            get { return formula; }
        }

        public CombinedGeometry Result
        {
            get { return result; }
        }

        private void PrepareFormulaToCalculations()
        {
            formula.Add('+');
            formula.Add('(');
            formula.Add('A');
            formula.Add('-');
            formula.Add('A');
            formula.Add(')');

            formula.Insert(0, '(');
            formula.Add(')'); //add ( ) to be able calculate
        }

        private void DictionarySet(Dictionary<Char, CombinedGeometry> simpleShapes)
        {
            shapes = new Dictionary<char, CombinedGeometry>();
            foreach (KeyValuePair<Char, CombinedGeometry> item in simpleShapes)
            {
                shapes.Add(item.Key, item.Value);
            }
        }

        private CombinedGeometry CalculateResult()
        {

            int numberOfSets = CalculateNumberOfSets();

            while (numberOfSets > 1)
            {

                for (int i = 0; i < formula.Count; i++)
                { // check '
                    if (formula[i] == '\'' && Char.IsLetter(formula[i - 1]))
                    {
                        char inputSet = formula[i - 1];
                        char newSet = CreateNewShape('U', '-', inputSet);
                        formula[i - 1] = newSet;
                        formula.RemoveAt(i);
                        i--;
                    }
                }

                bool bracketOpen = false;
                int openPosition = -1;
                for (int i = 0; i < formula.Count; i++)
                { //brackets change () -> {}
                    if (formula[i] == '(')
                    {
                        bracketOpen = true;
                        openPosition = i;
                    }
                    else if (bracketOpen && formula[i] == ')')
                    {
                        bracketOpen = false;
                        formula[openPosition] = '{';
                        formula[i] = '}';
                    }
                }

                bracketOpen = false;
                openPosition = -1;
                for (int i = 0; i < formula.Count; i++)
                {
                    if (formula[i] == '{')
                    {
                        bracketOpen = true;
                        openPosition = i;
                    }
                    else if (bracketOpen && formula[i] == '}')
                    {
                        bracketOpen = false;
                        formula[openPosition] = '[';
                        formula[i] = ']';
                        for (int j = openPosition + 1; j < i - 2; j += 2)
                        {
                            char Set1 = formula[j];
                            char operation = formula[j + 1];
                            char Set2 = formula[j + 2];
                            char resultSet = CreateNewShape(Set1, operation, Set2);
                            formula[j] = '#';
                            formula[j + 1] = '#';
                            formula[j + 2] = resultSet;
                        }
                    }
                }

                for (int i = 0; i < formula.Count; i++)
                { //clear # [ ]
                    if ("#[]".IndexOf(formula[i]) != -1)
                    {
                        formula.RemoveAt(i);
                        i--;
                    }
                }

                numberOfSets = CalculateNumberOfSets();
            }

            return shapes[formula[0]];
        }

        private int CalculateNumberOfSets()
        {
            int numberOfSets = 0;
            foreach (char ch in formula)
            {
                if (Char.IsLetter(ch))
                {
                    numberOfSets++;
                }
            }
            return numberOfSets;
        }

        private char CreateNewShape(char firstSet, char operation, char secondSet)
        {
            CombinedGeometry shape1 = shapes[firstSet];
            CombinedGeometry shape2 = shapes[secondSet];

            CombinedGeometry resultShape = new CombinedGeometry();

            switch (operation)
            {
                case '+':
                    {
                        resultShape = new CombinedGeometry
                            (GeometryCombineMode.Union, shape1, shape2);
                        break;
                    }
                case '*':
                    {
                        resultShape = new CombinedGeometry
                            (GeometryCombineMode.Intersect, shape1, shape2);
                        break;
                    }
                case '-':
                    {
                        resultShape = new CombinedGeometry
                            (GeometryCombineMode.Exclude, shape1, shape2);
                        break;
                    }
            }
            char letter = 'D';
            for (int i = 'D'; i <= 'Z'; i++)
            {
                letter = (char)i;
                if (!shapes.ContainsKey(letter))
                {
                    break;
                }
            }

            shapes.Add(letter, resultShape);
            return letter;
        }
    }
}
