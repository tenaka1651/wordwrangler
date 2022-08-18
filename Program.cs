using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;

namespace WordWrangler
{
    class Program
    {
        Dictionary<string, List<string>> _wordlist;

        static void Main(string[] args)
        {
            var anagrams = FindAnagrams("scar");
            foreach (var anagram in anagrams)
            {
                Console.WriteLine(anagram);
            }
        }

        static List<string> FindAnagrams(string word)
        {
            var alphaWord = Alphabetize(word);
            if (_wordlist.ContainsKey(alphaWord))
                return _wordlist[alphaWord];
        }

        static void LoadWords()
        {
            var assembly = Assembly.GetExecutingAssembly();
            const string resourceName = "WordWrangler.dictionary.words_en.txt";

            using (var stream = assembly.GetManifestResourceStream(resourceName))
            using (var reader = new StreamReader(stream))
            {
                var word = "";
                while ((word = reader.ReadLine()) != null)
                {
                    var alpha = Alphabetize(word);
                    if (_wordlist.ContainsKey(alpha))
                        _wordlist[alpha].Add(word);
                    else
                    {
                        _wordlist.Add(alpha, new List<string>()
                        {
                            word
                        });
                    }
                }
            }
        }

        static string Alphabetize(string s)
        {
            // Convert to char array, then sort and return
            var a = s.ToCharArray();
            Array.Sort(a);
            return new string(a);
        }
    }
}
