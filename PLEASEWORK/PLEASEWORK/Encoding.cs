using System;
using System.Diagnostics;
using System.Numerics;
using System.Security.Cryptography.X509Certificates;

namespace PLEASEWORK
{

    public partial class Form1
    // other encoding ideas:
    //    - chunk encoding
    //    - changing the amount of change for dictionary creation
    //    - creating multiple dictionaries to switch between based on a
    //      given number
    //    - shuffle letters, symbols, and numbers when encoding (a = ')
    //    - 
    {

        // list of letters, symbols, and numbers
        public List<char> upperCharList = [.. "ABCDEFGHIJKLMNOPQRSTUVWXYZ"];
        public List<char> charList = [.. "abcdefghijklmnopqrstuvwxyz"];
        public List<char> symbolList = [.. @"';:.>,</?]}[{=+-_)(*&^%$#@!`~|\ ", '"', .. "\r\n"
            + "℃©®℉℗™№¦§×÷—⁅⁆›»«‹⟩⟨⟪⟫‸¨¯⁗‷‴‡•‰‱⁽⁾‥⁚⁝…·․⁇⸘‽¿⁈‼¡°" +
                "ΑαΒβΓγΔδΕεΖζΗηΘθΙιΚκΛλΜμΝνΞξΟοΠπΡρΣσςΤτΥυΦφΧχΨψΩω" +
                "ÃÕÑÄËÏÖÜŸÂÊÎÔÛÀÈÌÒÙÁÉÍÓÚÝáéíóúýàèìòùâêîôûäëïöüÿãõñ" +
                "∲∵∸∻∾≁∱∮∫∢∶∹∿≂≃≀∺∷∴≄≇≊≍≐≓≔≑≎≋≈≅≆≉≌≏≒≕≮≢≟≙⁰³⁶⁹⁺⅟↉⁻ⁿ⁷⁴¹²⁵⁸ⁱ⁼₀₃₆₉₊₋₍₇₄₁₂₅₈₎₌∀∃∄ⅼⅽⅭⅬ"];
        // " +
        //"℃©®℉℗™№¦§×÷—⁅⁆›»«‹⟩⟨⟪⟫‸¨¯⁗‷‴‡•‰‱⁽⁾‥⁚⁝…·․⁇⸘‽¿⁈‼¡°" +
        //"ΑαΒβΓγΔδΕεΖζΗηΘθΙιΚκΛλΜμΝνΞξΟοΠπΡρΣσςΤτΥυΦφΧχΨψΩω" +
        //"ÃÕÑÄËÏÖÜŸÂÊÎÔÛÀÈÌÒÙÁÉÍÓÚÝáéíóúýàèìòùâêîôûäëïöüÿãõñ" +
        //"∲∵∸∻∾≁∱∮∫∢∶∹∿≂≃≀∺∷∴≄≇≊≍≐≓≔≑≎≋≈≅≆≉≌≏≒≕≮≢≟≙⁰³⁶⁹⁺⅟↉⁻ⁿ⁷⁴¹²⁵⁸ⁱ⁼₀₃₆₉₊₋₍₇₄₁₂₅₈₎₌∀∃∄ⅼⅽⅭⅬ" +
        //"
        public List<char> numList = [.. "1234567890"];


        public List<Func<List<char>, KeyValuePair<string, int>>> encodingLayers = [];
        public List<Func<KeyValuePair<string, int>, string>> decodingLayers = [];


        // code to generate character and encoded character dictionaries with an
        // option to reverse for decoding - when called, it returns a list of
        // dictionaries
        public List<Dictionary<char, char>> CreateDicts(int seed, bool backwards = false)
        {
            List<List<char>> lists = [[.. charList], [.. symbolList], [.. numList], 
            [.. upperCharList]];
            List<Dictionary<char, char>> dicts = [[], [], [], []];

            Debug.WriteLine($"  > Creating Dictionaries using seed [{seed}].");

            for (int i = 0; i < 4; i++)
            {
                // index counter
                int t = 1;
                foreach (char c in lists[i].ToList())
                {
                    char newC = lists[i][((seed * t) + (t - 1)) %
                        lists[i].Count];
                    if (backwards) dicts[i].Add(newC, c);
                    else dicts[i].Add(c, newC);
                    lists[i].Remove(newC);
                    t++;
                }
            }
            return dicts;
        }

        // creates encoding dictionaries that have mixed encoded chars (a = ;)
        public List<Dictionary<char, char>> CreateMixedDicts(int seed, bool backwards = false)
        {
            //Random r = new(((seed + 72) * 4) + 16);

            List<List<char>> lists = [[.. charList], [.. symbolList
                ],
                [.. numList], [.. upperCharList]];
            List<List<char>> placeholderLists = [[.. charList], [.. symbolList],
                [.. numList], [.. upperCharList]];
            List<Dictionary<char, char>> dicts = [[], [], [], []];

            Debug.WriteLine($"  > Creating MIXED Dictionaries using seed [{seed}].");

            int randomDictIndex;
            int t = 1;

            for (int i = 0; i < 4; i++)
            {
                // index counter
                foreach (char c in placeholderLists[i].ToList())
                {
                    randomDictIndex = (((seed * t) + (t - 1)) % (((seed + (t * 
                        ((seed % t) + 1))) % (t + 2)) + 3) + 4)
                        % lists.Count;

                    // create dictionaries and remove the encoded character from
                    // copied libraries to avoid duplicates
                    if (backwards) dicts[i].Add(lists[randomDictIndex][((seed
                        * t) + (t - 1)) % lists[randomDictIndex].Count], c);
                    else dicts[i].Add(c, lists[randomDictIndex][((seed * t) + 
                        (t - 1)) % lists[randomDictIndex].Count]);
                    lists[randomDictIndex].Remove(lists[randomDictIndex][((seed
                        * t) + (t - 1)) % lists[randomDictIndex].Count]);

                    if (lists[randomDictIndex].Count < 1) lists.Remove(lists[randomDictIndex]);
                    t++;
                }
            }

            //foreach (Dictionary<char, char> d in dicts) Debug.WriteLine($"  > [{PrintDict(d)}]");
            //Debug.WriteLine("");

            return dicts;
        }

        public List<Dictionary<char, char>> CreateSymbolDicts(int seed, bool backwards = false)
        {
            List<char> newSymbolList = [.. "‐‑‒–―‖‗‘’‚‛“”„‟†‣‧′″‵‶※‾‿⁀⁁⁂⁃⁄⁉⁊⁋⁌⁍⁎⁏" +
                "⁐⁑⁒⁓⁔⁕⁖⁘⁙⁛⁜⁞₠₡₢₣₤₥₦₧₨₩₪₫€₭₮₯₰₱₲₳₴₵₶₷₸₹₺₻₼₽₾₿⃀℀℁ℂ℄℅℆ℇ℈ℊℋℌℍℎℏℐℑℒℓ" +
                "℔ℕ℘ℙℚℛℜℝ℞℟℠℡℣ℤ℥Ω℧ℨ℩KÅℬℭ℮ℯℰℱℲℳℴℵℶℷℸℹ℺℻ℼℽℾℿ⅀⅁⅂⅃⅄ⅅⅆⅇⅈⅉ⅊⅋⅌⅍ⅎ⅏←↑" +
                "→↓↔↕↖↗↘↙↚↛↜↝↞↟↠↡↢↣↤↥↦↧↨↩↪↫↬↭↮↯↰↱↲↳↴↵↶↷↸↹↺↻↼↽↾↿⇀⇁⇂⇃⇄⇅⇆⇇⇈⇉⇊⇋⇌⇍⇎⇏⇐⇑⇒⇓⇔⇕⇖" +
                "⇗⇘⇙⇚⇛⇜⇝⇞⇟⇠⇡⇢⇣⇤⇥⇦⇧⇨⇩⇪⇫⇬⇭⇮⇯⇰⇱⇲⇳⇴⇵⇶⇷⇸⇹⇺⇻⇼⇽⇾⇿∁∂∅∆∇∈∉∊∋∌∍∎∏∐∑" +
                "−∓∔∕∖∗∘∙√∛∜∝∞∟∠∡∣∤∥∦∧∨∩∪∬∭∯∰∳∼∽≖≗≘≚≛≜≝≞≠"];
            List<List<char>> lists = [[.. charList], [.. symbolList], [.. numList], [.. upperCharList]];
            List<Dictionary<char, char>> dicts = [[], [], [], []];

            Debug.WriteLine($"  > Creating SYMBOL ONLY Dictionaries using seed [{seed}].");

            //int randomDictIndex;
            int t = 1;
            char newChar;

            //for (int i = 0; i < 97; i++)
            //{
            //    newChar = tempSymbolList[((seed
            //            * (i + 1)) + ((i + 1) - 1)) % tempSymbolList.Count];
            //    newSymbolList.Add(newChar);
            //    tempSymbolList.Remove(newChar);
            //}
            //Debug.WriteLine(string.Join(", ", newSymbolList));

            for (int i = 0; i < 4; i++)
            {
                // index counter
                foreach (char c in lists[i].ToList())
                {
                    newChar = newSymbolList[((seed
                        * t) + (t - 1)) % newSymbolList.Count];

                    // create dictionaries and remove the encoded character from
                    // copied libraries to avoid duplicates
                    if (backwards) dicts[i].Add(newChar, c);
                    else dicts[i].Add(c, newChar);
                    newSymbolList.Remove(newChar);

                    t++;
                }
            }

            //foreach (Dictionary<char, char> d in dicts) Debug.WriteLine($"  > [{PrintDict(d)}]");
            //Debug.WriteLine("");

            return dicts;
        }


        // returns the index of whatever dictionary should be used for a given
        // character - returns -1 if it isn't found
        public static int FindDict(char c, List<Dictionary<char, char>> dList)
        {
            int num = dList[0].ContainsKey(c) ? 0 : -1;
            num = dList[1].ContainsKey(c) ? 1 : num;
            num = dList[2].ContainsKey(c) ? 2 : num;
            num = dList[3].ContainsKey(c) ? 3 : num;

            return num;
        }

        // general encoding function
        public KeyValuePair<string, List<int>> Encode(string message)
        {
            // current message, list of encoded messages and seeds (this is
            // where the 0 comes from), list of seeds
            List<char> messageChars = [.. message];
            List<KeyValuePair<string, int>> encLayers = [new(message, 0)];
            List<int> seeds = [];

            // goes through each provided encoding layer and encodes
            foreach (Func<List<char>, KeyValuePair<string, int>> func in encodingLayers)
            {
                Debug.WriteLine($"  > Encoding layer [{encLayers.Count + 1}]");
                encLayers.Add(func(messageChars));
                //Debug.WriteLine($"\n\n  > Uncoded text: {string.Join("", messageChars)}" +
                //    $"\n  > Encoded Text: {encLayers[^1].Key}\n     - Seed Used: " +
                //    $"[{encLayers[^1].Value}]" +
                //    $"\n\n    -------\n");
                messageChars = [.. encLayers[^1].Key];
            }

            encodingLayers.Clear();

            // adds all used seeds to a list for decoding, then reverses it
            // for easier use while decoding. Also prints all layers of
            // encoding
            foreach (KeyValuePair<string, int> enc in encLayers) 
            {
                seeds.Add(enc.Value);
                Debug.WriteLine($"  > Layer [{encLayers.IndexOf(enc)}] ([{(
                    enc.Value < 100 ? (enc.Value < 10 ? $"00{enc.Value}" : 
                    $"0{enc.Value}") : enc.Value)}]) -> {enc.Key}");
            }
            seeds.Reverse();
            Debug.WriteLine("\n    -------\n\n");

            return new KeyValuePair<string, List<int>>(string.Join("", messageChars), seeds);
        }


        // encoding method specific to character-by-character encoding
        public KeyValuePair<string, int> CharByCharEncoding(List<char> message)
        {
            // creates the seed and length of chars before switching dictionaries
            int createDictsSeed = setSeed == 0 ? rand.Next(3, 999) : setSeed;
            int switchLength = (createDictsSeed % ((createDictsSeed % ((message.Count % 4)
                + 2)) + 1));
            List<char> encoded = [];

            // creating the encoded dictionaries
            List<List<Dictionary<char, char>>> mixedDictList = [
                CreateMixedDicts((createDictsSeed * message.Count)
                + (message.Count % 16)),
                CreateMixedDicts(message.Count + switchLength),
                CreateMixedDicts(switchLength * ((createDictsSeed % 72) + 56))
                ];

            List<List<Dictionary<char, char>>> symbolDictList = [
                CreateSymbolDicts(((switchLength * 2) % message.Count)
                + createDictsSeed),
                CreateSymbolDicts(createDictsSeed + ((createDictsSeed /
                (switchLength + 1)) * message.Count)),
                CreateSymbolDicts((message.Count * 3) + (createDictsSeed
                + ((message.Count - switchLength) * 2)))
                ];

            List<List<Dictionary<char, char>>> dictLists = mixedDictList;

            message = Shuffle(message, createDictsSeed);

            List<char> missingChars = [];

            // index variables for dictionary and current character
            int dictNum = 0;
            int t = 1;
            List<Dictionary<char, char>> currentDictList;

            for (int i = 0; i < 2; i++)
            {
                foreach (char c in message)
                {
                    // creates different switch lengths, then chooses one of five dictionaries
                    // using a different equation
                    switchLength = (((createDictsSeed % ((switchLength % ((t % 4) + 2)) + 1)) +
                        (t % 3)) % 4 + (((message.Count * t) % ((createDictsSeed / 4) + 1))
                        % 5)) + 1;
                    currentDictList = dictLists[Math.Abs((message.Count - (t * switchLength))
                        + (dictNum * (t + createDictsSeed))) % 3];
                    // if the character isn't found in a dictionary, then it
                    // doesn't encode it
                    dictNum = FindDict(c, currentDictList);
                    if (dictNum == -1)
                    {
                        encoded.Add(c);
                        missingChars.Add(c);
                        continue;
                    }

                    Debug.WriteLine($"{c} | {currentDictList[dictNum][c]}");

                    encoded.Add(currentDictList[dictNum][c]);

                    t++;
                }
                if (dictLists == mixedDictList)
                {
                    message = Shuffle(encoded, message.Count + (createDictsSeed / 2));
                    encoded = [];
                    dictLists = symbolDictList;
                    t = 1;
                    switchLength = (createDictsSeed % ((createDictsSeed % ((message.Count % 4) +
                        2)) + 1));
                    dictNum = 0;
                    Debug.WriteLine($"switching dicts\n\n [{string.Join("", message)}] - [{switchLength}]");
                }
            }

            // prints all chars that were not found in provided dictionaries
            if (missingChars.Count > 0) Debug.WriteLine($"\n  ! The following " +
                $"characters were not found and therefore were not encoded:\n" +
                $"      [{string.Join(", ", missingChars)}]\n");

            // shuffles the encoded message twice
            encoded = Shuffle(encoded, (createDictsSeed - ((encoded.Count % 3) + 1)));

            return new KeyValuePair<string, int>(string.Join("", Shuffle(encoded, 
                (createDictsSeed - ((encoded.Count % 7) + 1)))), createDictsSeed);
        }

        public int GetIndexOfChar(char c)
        {
            int i = charList.Contains(c) ? 0 : -1;
            i = symbolList.Contains(c) ? 1 : i;
            i = numList.Contains(c) ? 2 : i;
            i = upperCharList.Contains(c) ? 3 : i;

            return i;
        }

        // Shuffling code
        public static List<char> Shuffle(List<char> message, int seed, bool unShuffle = false)
        {
            //Debug.WriteLine($"  > {(unShuffle ? "Uns" : "S")}huffling text:" +
            //    $"   {string.Join("", message)}");

            List<char> newMessage = [.. message];
            // list of indexes for the message
            int[] messageIndexes = [..  Enumerable.Range(0, message.Count)];

            // creates new random instance
            Random r = new((seed + (message.Count - 1)) * ((seed * ((message.Count
                / ((seed % 4) + 1)) + 6)) + 1));

            // temporary counter for the amount of shuffle for the indexes
            int tempI = (seed % 5) + 1;

            // shuffles the indexes and changes the random seed per iteration
            while (tempI > 0) { r.Shuffle(messageIndexes); r = new((seed + 
                (message.Count - (messageIndexes[0] + 2))) * 
                (messageIndexes[(messageIndexes[(seed + 1) % message.Count]
                + 2) % message.Count] + 17)); tempI--; }

            // updates the message indexes to the shuffled indexes
            for (int i = 0; i < message.Count; i++)
            {
                if (unShuffle) newMessage[i] = message[messageIndexes[i]];
                else newMessage[messageIndexes[i]] = message[i];
            }

            Debug.WriteLine($"  > Successfully {(unShuffle ? "Uns" : "S")}huffled message.");
            //Debug.WriteLine($"  > Shuffled: {string.Join("", message)}" +
            //    $"\n  > Unshuffled: {string.Join("", newMessage)}");

            return newMessage;
        }
    }
}
