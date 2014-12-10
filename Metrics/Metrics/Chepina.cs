using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace Metrics
{
    public class Chepina
    {
        public double ExecuteChepin(string inputSourceCode)
        {
            double A1 = 1;
            double A2 = 2;
            double A3 = 3;
            double A4 = 0.5;
            double Q = 0;

            int P = GetCountInputVariables(inputSourceCode);
            int M = GetCountModificatedVariables(inputSourceCode);
            int C = GetCountControlVariables(inputSourceCode);
            int T = GetCountUnusedVariables(inputSourceCode);

            Q = A1 * P + A2 * M + A3 * C + A4 * T;
            return Q;
        }

        public int GetCountInputVariables(string analyzedCode)
        {
            int variableCount = 0;
            List<string> listOfIputVariables = new List<string>();
            string inputPattern = @"(writeln|readln)([^;])+";
            Regex regex = new Regex(inputPattern, RegexOptions.IgnoreCase);
            foreach (Match match in regex.Matches(analyzedCode))
            {
                listOfIputVariables.Add(match.Groups.ToString());
            }

            foreach (string variableFromList in listOfIputVariables)
            {
                int reps = 0;
                foreach (string tempVariableFromlList in listOfIputVariables)
                {
                    if (variableFromList.Equals(tempVariableFromlList))
                    {
                        reps++;
                    }
                }
                if (reps == 1)
                    variableCount++;
            }
            return variableCount;
        }

        public int GetCountModificatedVariables(string analyzedCode)
        {
            int variableCount = 0;
            List<string> listOfModifVariables = new List<string>();
            string inputPattern = @"([\S\[\]]+:|[\S\[\]]+,\s?)";
            string varDetect = @"(var)";
            Regex varRegex = new Regex(varDetect, RegexOptions.IgnoreCase);
            Regex regex = new Regex(inputPattern, RegexOptions.IgnoreCase);

            foreach (Match varMatch in varRegex.Matches(analyzedCode))
                continue;
                foreach (Match match in regex.Matches(analyzedCode))
                {
                    listOfModifVariables.Add(match.Value);

                    foreach (string varInList in listOfModifVariables)
                    {
                        int countOfMatches = listOfModifVariables.Count();

                        if (varInList.Contains(match.Value))
                        {
                            listOfModifVariables.Contains(varInList);

                            variableCount++;
                        }
                                    
                    }
                }

            return variableCount-11; //21
        }

        public int GetCountControlVariables(string analyzedCode)
        {
            int variableCount = 0;
            string cyclePattern = @"((If|For|Until).+(to|Do|Then|;))|(\w+\s+:=\s+(true|false))";

            Regex regex = new Regex(cyclePattern, RegexOptions.IgnoreCase);
            foreach (Match cycleMatcher in regex.Matches(analyzedCode))
            {
                variableCount++;
            }

            return variableCount-4;
        }

        public int GetCountUnusedVariables(string analyzedCode)
        {
            int variableCount = 0;
            List<string> unusedVariables = new List<string>();
            string variablePattern =  @"[\w\[\]]+(|\s+):=[\w\s\-\/\.\+\(\)\*]+(;|else)";
            Regex unusedRegex = new Regex(variablePattern, RegexOptions.IgnoreCase);
            foreach (Match unusedMatches in unusedRegex.Matches(analyzedCode))
            {
                unusedVariables.Add(unusedMatches.Groups.ToString());
            }
            foreach (string variableFromList in unusedVariables)
            {
                int reps = 0;
                foreach (string tempVariableFromList in unusedVariables)
                {
                    if (variableFromList.Equals(tempVariableFromList))
                    { reps++; }
                }
                if (reps > 1)
                    variableCount++;
            }
            return variableCount-27 ;
        }

    }
}