using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace VennDiagrams
{
    class Input
    {
        private string formula;
        private bool isValid;
        private string errorType;

        public Input(string input)
        {
            formula = removeWhiteCharacters(input).ToUpper();
            isValid = isValidCheck();
        }

        public string Formula
        {
            get { return formula; }
        }

        public bool IsValid
        {
            get { return isValid; }
        }

        public string ErrorType
        {
            get { return errorType; }
        }

        public string removeWhiteCharacters(String input)
        {
            return Regex.Replace(input, @"\s+", "");
        }

        public bool isValidCheck()
        {
            if (IsNullOrEmpty())
                return false;

            if (!OnlyValidateCharacters())
                return false;

            if (!FirstCharValid())
                return false;

            if (!LastCharValid())
                return false;

            if (!SameNumberOfOpeningAndClosingBrackets())
                return false;

            if (ClosingBeforeOpening())
                return false;

            if (!AllNextCharaktersValid())
                return false;

            return true;
        }

        public bool IsNullOrEmpty()
        {
            if (String.IsNullOrEmpty(formula))
            {
                errorType = "Empty formula";
                return true;
            }
            return false;
        }


        public bool OnlyValidateCharacters()
        {
            for (int i = 0; i < formula.Length; i++)
            {
                if ("ABCU+*-'()".IndexOf(formula[i]) == -1)
                {

                    errorType = "ERROR wrong character: " + formula[i];
                    return false;
                }
            }
            return true;
        }

        public bool FirstCharValid()
        {
            if ("ABCU(".IndexOf(formula[0]) == -1)
            {
                errorType = "ERROR wrong first character: " + formula[0];
                return false;
            }
            return true;
        }

        public bool LastCharValid()
        {
            if ("ABCU)'".IndexOf(formula[formula.Length - 1]) == -1)
            {
                errorType = "ERROR wrong last character: " + formula[formula.Length - 1];
                return false;
            }
            return true;
        }

        public bool SameNumberOfOpeningAndClosingBrackets()
        {
            int openBrackets = 0;
            int closeBrackets = 0;

            foreach (char ch in formula)
            {
                if (ch == '(')
                    openBrackets++;
                else if (ch == ')')
                    closeBrackets++;
            }

            if (openBrackets != closeBrackets)
            {
                errorType = "ERROR : number of opening and closing brackets";
                return false;
            }

            return true;
        }

        public bool ClosingBeforeOpening()
        {
            int openMinusCloseBrackets = 0;
            for (int i = 0; i < formula.Length; i++)
            {
                if (formula[i] == '(')
                    openMinusCloseBrackets++;

                else if (formula[i] == ')')
                    openMinusCloseBrackets--;

                if (openMinusCloseBrackets < 0)
                {
                    errorType = "ERROR : closing bracket before opening bracket";
                    return true;
                }
            }

            return false;
        }

        public bool AllNextCharaktersValid()
        {
            for (int i = 0; i < formula.Length - 1; i++)
            {

                if ("ABCU".IndexOf(formula[i]) != -1 && "+*-)'".IndexOf(formula[i + 1]) == -1)
                {
                    errorType = "ERROR wrong next character of: " + formula[i];
                    return false;
                }
                else if ("+*-".IndexOf(formula[i]) != -1 && "ABCU('".IndexOf(formula[i + 1]) == -1)
                {
                    errorType = "ERROR wrong next character of: " + formula[i];
                    return false;
                }
                else if ("(".IndexOf(formula[i]) != -1 && "ABCU(".IndexOf(formula[i + 1]) == -1)
                {
                    errorType = "ERROR wrong next character of: " + formula[i];
                    return false;
                }
                else if (")".IndexOf(formula[i]) != -1 && "+*-)'".IndexOf(formula[i + 1]) == -1)
                {
                    errorType = "ERROR wrong next character of: " + formula[i];
                    return false;
                }
                else if ("'".IndexOf(formula[i]) != -1 && "+*-)'".IndexOf(formula[i + 1]) == -1)
                {
                    errorType = "ERROR wrong next character of: " + formula[i];
                    return false;
                }
            }

            return true;
        }
    }
}
