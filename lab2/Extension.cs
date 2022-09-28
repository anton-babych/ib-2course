using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace lab2
{
    public static class Extension
    {
        public static void GetPer(string str, out List<string> combinations)
        {
            combinations = new List<string>();
            
            char[] ch = str.ToCharArray();
            int x = ch.Length - 1;
            GetPer(ch, 0, x, ref combinations);
        }

        public static void GetPer(char[] list, out List<string> combinations)
        {
            combinations = new List<string>();
            
            int x = list.Length - 1;
            GetPer(list, 0, x, ref combinations);
        }

        private static void GetPer(char[] list, int k, int m, ref List<string> combinations)
        {
            if (k == m)
            {
                combinations.Add(new string(list));
            }
            else
                for (int i = k; i <= m; i++)
                {
                    Swap(ref list[k], ref list[i]);
                    GetPer(list, k + 1, m, ref combinations);
                    Swap(ref list[k], ref list[i]);
                }
        }

        private static void Swap(ref char a, ref char b)
        {
            if (a == b) return;

            (a, b) = (b, a);
        }


        private static void PrintRElementCombination(char[] arr, char[] temp, ref List<string> combination, int start, int
            end, int index, int r){
            if (index == r)
            {
                string t = "";
                for (int j = 0; j < r; j++)
                {
                    t += temp[j];
                }
                combination.Add(t);
                return;
            }
            for (int i = start; i <= end && end - i + 1 >= r - index; i++){
                temp[index] = arr[i];
                PrintRElementCombination(arr, temp, ref combination, i+1, end, index+1, r);
            }
        }
        private static List<string> GetCombinations(string tArr, int length){
            int n = tArr.Length;
            char[] temp = new Char[length];
            List<string> combination = new List<string>();
            PrintRElementCombination(tArr.ToCharArray(), temp, ref combination, 0, n-1, 0, length);
            return combination;
        }
        
        public static List<string> GetCombinationsAndPerm(string allSymbols, int length, bool isDebug = true)
        {
            var combinations = Extension.GetCombinations(allSymbols, length);
            List<string> st = new List<string>();

            foreach (var comb in combinations)
            {
                Extension.GetPer(comb, out List<string> pers);
                foreach (var per in pers)
                {
                    if(isDebug)Console.WriteLine(per);
                    st.Add(per);
                }
            }

            return st;
        }
    }
}