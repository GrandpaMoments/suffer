using System;
using System.Diagnostics;

namespace PLEASEWORK
{
    public partial class Form1
    {
        // General method to decode
        public string Decode(KeyValuePair<string, List<int>> encoded)
        {
            // current encoded message, list of decoded messages, list of seeds
            // used for decoding, empty variable for specific KVP usage
            string encodedMessage = encoded.Key;
            List<string> decLayers = [encoded.Key];
            List<int> seeds = encoded.Value;
            KeyValuePair<string, int> decodeKVP;
            int layerIndex = 0;

            // reverses path of encoding to decode
            decodingLayers.Reverse();

            // goes through each encoding layer and decodes it, while printing
            // the encoded and decoded message with the seed used
            foreach (Func<KeyValuePair<string, int>, string> func in decodingLayers)
            {
                Debug.WriteLine($"  > Decoding layer " +
                    $"[{layerIndex + 1}]");
                decodeKVP = new(encodedMessage, encoded.Value[layerIndex]);
                decLayers.Add(func(decodeKVP));
                //Debug.WriteLine($"\n\n  > Encoded Text: {decodeKVP.Key}" +
                //    $"\n  > Decoded Text: {decLayers[^1]}\n     - Seed Used: " +
                //    $"[{decodeKVP.Value}]" +
                //    $"\n\n    -------\n\n    Seeds used:");
                encodedMessage = decLayers[^1];
                layerIndex++;
            }

            decodingLayers.Clear();

            // Prints the seeds used and each layer's decoded message
            foreach (string dec in decLayers) {
                int ind = seeds[decLayers.IndexOf(dec)];
                Debug.WriteLine($"  > Layer " +
                $"[{decLayers.IndexOf(dec)}]" +
                $"([{(ind < 100 ? (ind < 10 ? $"00{ind}" : $"0{ind}")
                : ind)}]) -> {dec}");
            }
            Debug.WriteLine("\n    -------\n\n");

            return encodedMessage;
        }


        // decoding method for character by character decoding
        public string CharByCharDecoding(KeyValuePair<string, int> encoded)
        {
            // variables for the current encoded message, a list of previously
            // encoded messages, the key/seed, and an amount of chars to decode
            // before switching dictionaries
            string encodedMessage = encoded.Key;
            List<char> message = [.. encodedMessage];
            int seed = encoded.Value;
            int switchLength = (seed % ((seed % ((message.Count % 4) + 
                2)) + 1));

            // creates decoding dictionaries
            List<List<Dictionary<char, char>>> mixedDictList = [
                CreateMixedDicts((seed * message.Count)
                + (message.Count % 16), true),
                CreateMixedDicts(message.Count + switchLength, true),
                CreateMixedDicts(switchLength * ((seed % 72) + 56), true)
                 ];

            List<List<Dictionary<char, char>>> symbolDictList = [
                CreateSymbolDicts(((switchLength * 2) % message.Count)
                + seed, true),
                CreateSymbolDicts(seed + ((seed /
                (switchLength + 1)) * message.Count), true),
                CreateSymbolDicts((message.Count * 3) + (seed
                + ((message.Count - switchLength) * 2)), true)
                ];

            List<List<Dictionary<char, char>>> dictLists = symbolDictList;

            message = Shuffle(message, encoded.Value - ((message.Count
                % 7) + 1), true);
            message = Shuffle(message, encoded.Value - ((message.Count
                % 3) + 1), true);

            List<char> missingChars = [];
            List<char> decoded = [];

            // variables for current dictionary index and current char index
            int dictNum = 0;
            int t = 1;
            List<Dictionary<char, char>> currentDictList;

            // decoding process
            for (int i = 0; i < 2; i++)
            {
                foreach (char c in message)
                {
                    switchLength = (((seed % ((switchLength % ((t % 4) + 2)) + 1))
                        + (t % 3)) % 4 + (((message.Count * t) % ((seed / 4) + 1))
                        % 5)) + 1;
                    currentDictList = dictLists[Math.Abs((message.Count - (t *
                        switchLength)) + (dictNum * (t + seed))) % 3];

                    // if the character isn't found, the character is not encoded
                    dictNum = FindDict(c, currentDictList);
                    if (dictNum == -1)
                    {
                        decoded.Add(c);
                        missingChars.Add(c);
                        continue;
                    }

                    Debug.WriteLine($"{c} | {currentDictList[dictNum][c]}");

                    decoded.Add(currentDictList[dictNum][c]);

                    t++;
                }
                if (dictLists == symbolDictList)
                {
                    message = Shuffle(decoded, message.Count + (seed / 2), true);
                    decoded = [];
                    dictLists = mixedDictList;
                    t = 1;
                    switchLength = (seed % ((seed % ((message.Count % 4) +
                        2)) + 1));
                    dictNum = 0;
                    Debug.WriteLine($"switching dicts\n\n [{string.Join("", message)}] - [{switchLength}]");
                }
            }

            // prints any chars that were missing from provided dictionaries
            if (missingChars.Count > 0) Debug.WriteLine($"\n  ! The following characters " +
                $"were not found and therefore were not decoded:\n" +
                $"      [{string.Join(", ", missingChars)}]\n");

            Debug.WriteLine(string.Join("", Shuffle(decoded, seed, true)));

            return string.Join("", Shuffle(decoded, seed, true));
        }
    }
}
