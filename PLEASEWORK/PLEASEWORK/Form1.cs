using Microsoft.VisualBasic.ApplicationServices;
using System.Diagnostics;
using System.IO;
using System.Security.Cryptography;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace PLEASEWORK
{
    public partial class Form1 : Form
    {
        // random, json options, and filepath variables
        public Random rand = new();
        readonly JsonSerializerOptions options = new() { WriteIndented = true };
        public string filePath = Path.GetFullPath("Encoding.cs").Replace(
            $@"\bin\Debug\net10.0-windows\Encoding.cs", @"\EncodedFiles");

        public int setSeed = 0;

        // ℒ₮⇅⁜⁓‑‑ℭ⇉ℭ₪

        public Form1()
        {
            InitializeComponent();

            //encodingLayers.Add(CharByCharEncoding);
            //decodingLayers.Add(CharByCharDecoding);

            //setSeed = 501;

            //Decode(Encode("thanks guys"));

            //return;

            Debug.WriteLine(filePath);

            Debug.WriteLine($"{("℃©®℉℗™№¦§×÷—⁅⁆›»«‹⟩⟨⟪⟫‸¨¯⁗‷‴‡•‰‱⁽⁾‥⁚⁝…·․⁇⸘‽¿⁈‼¡°" +
            "ΑαΒβΓγΔδΕεΖζΗηΘθΙιΚκΛλΜμΝνΞξΟοΠπΡρΣσςΤτΥυΦφΧχΨψΩω" +
            "ÃÕÑÄËÏÖÜŸÂÊÎÔÛÀÈÌÒÙÁÉÍÓÚÝáéíóúýàèìòùâêîôûäëïöüÿãõñ" +
            "∲∵∸∻∾≁∱∮∫∢∶∹∿≂≃≀∺∷∴≄≇≊≍≐≓≔≑≎≋≈≅≆≉≌≏≒≕≮≢≟≙⁰³⁶⁹⁺⅟↉⁻ⁿ⁷⁴¹²⁵⁸ⁱ⁼₀₃₆₉₊₋₍₇₄₁₂₅₈₎₌∀∃∄ⅼⅽⅭⅬ").Length}");

            //CreateSymbolDicts(66);
            //CreateSymbolDicts(66, true);

            // Creates a folder for encoded files if a folder doesn't already
            // exist
            if (!Directory.Exists(@$"{filePath}"))
            {
                Directory.CreateDirectory(@$"{filePath}");
                Debug.WriteLine("  > Created folder for encoded files.");
            }
            else Debug.WriteLine("  > Folder for encoded files already exists.");
            //// "A quick fox jumps over the lazy dog 1234567890 +_)(*&^%$#@!~`-=]}[{':;?/>.<,\\|🔏🗑️📄🔎"
            ////"The big dog ate the small deer, causing the food to explode. There are many issues to this story, " +
            ////"however many solutions can be found. Isn't it funny, the 37th time a food ate a dog? the burger patty is tasty, ISN'T IT??? hello?"
        }

        // prints a dictionary
        public static string PrintDict<TKey, TValue>(Dictionary<TKey, TValue> d) where TKey : notnull
        {
            List<string> l = [];

            foreach (var kvp in d)
            {
                l.Add($"[{kvp.Key} : {kvp.Value}]");
            }

            return string.Join(", ", l);
        }


        
        private void EncodeButton_Click(object sender, EventArgs e)
        {
            if (TextInputBox.Text.Trim().Length < 1)
            {
                MessageBox.Show("Input cannot be empty!");
                TextInputBox.Text = string.Empty;
                return;
            }
            encodingLayers.Add(CharByCharEncoding);

            KeyValuePair<string, List<int>> enc = Encode(TextInputBox.Text.Trim());

            // removes the extra key for the encoded text
            // (encoded text has the key of 0)
            enc.Value.Remove(0);

            TextOutputBox.Text = $"{enc.Key}";
            KeyOutputBox.Text = string.Join(".", enc.Value);

            KeyInputBox.Text = string.Empty;
        }

        private void DecodeButton_Click(object sender, EventArgs e)
        {
            if (TextInputBox.Text.Trim().Length < 1 || KeyInputBox.Text.Trim().Length < 1)
            {
                MessageBox.Show("Input or key cannot be empty!");
                TextInputBox.Text = string.Empty;
                KeyInputBox.Text = string.Empty;
                return;
            }
            
            List<int> intKeys = ListToInt([.. KeyInputBox.Text.Trim().Split('.')]);
            if (intKeys.Contains(-1)) return;

            decodingLayers.Add(CharByCharDecoding);

            // adds the removed 0
            if (!intKeys.Contains(0)) intKeys.Add(0);
            
            string dec = Decode(new KeyValuePair<string, List<int>>(TextInputBox.Text.Trim(),
                intKeys));

            TextOutputBox.Text = $"{dec}";
            KeyOutputBox.Text = string.Empty;
        }

        public static List<int> ListToInt(List<string> keys)
        {
            List<int> intKeys = [];

            foreach (string key in keys)
            {
                if (!int.TryParse(key, out int result))
                {
                    MessageBox.Show("Key failed to convert to int!");
                    return [-1];
                }

                intKeys.Add(result);
            }

            return intKeys;
        }

        private void Save_Click(object sender, EventArgs e)
        {
            if (TextOutputBox.Text.Trim().Length < 1 || KeyOutputBox.Text.Trim().Length < 1)
            {
                MessageBox.Show("You are only able to save encoded text! Please" +
                    " encode something before trying to save.");
                return;
            }

            KeyValuePair<string, List<int>> data = new(TextOutputBox.Text.Trim(),
                ListToInt([.. KeyOutputBox.Text.Trim().Split('.')]));
            string saveData = JsonSerializer.Serialize(data, options);

            string fileName = FolderName.Text.Trim().Length < 1 ? $"[{data.Value[0]}]  {data.Key[..(data.Key.Length < 5 ? data.Key.Length : 5)]} ({DateTime.Now.ToShortDateString().Replace('/', '-')})" : FolderName.Text.Trim();
            bool cont = true;

            if (Path.Exists(@$"{filePath}\{fileName}.json"))
            {
                int c = 1;
                while (cont)
                {
                    if (Path.Exists(@$"{filePath}\{fileName}_{c.ToString("000")}.json")) c++;
                    else
                    {
                        cont = false;
                        fileName = $"{fileName}_{c.ToString("000")}";
                    }

                    if (c == 999)
                    {
                        Debug.WriteLine($"  ! File with name {fileName} has max (999) instances.");
                        fileName = $"[{data.Value[0]}]  {data.Key[..(data.Key.Length < 5 ? data.Key.Length : 5)]} ({DateTime.Now.ToShortDateString().Replace('/', '-')})";
                        break;
                    }
                }
            }

            Debug.WriteLine($"  > File [ {fileName}.json ] saved.");

            File.WriteAllText(@$"{filePath}\{fileName}.json",
                saveData);
        }

        private void LoadFile_Click(object sender, EventArgs e)
        {
            using OpenFileDialog ofd = new();
            ofd.InitialDirectory = filePath;
            ofd.Filter = "Json Files (*.json)|*.json";
            ofd.FilterIndex = 1;
            ofd.RestoreDirectory = true;

            if (ofd.ShowDialog() == DialogResult.OK)
            {
                string tempFilePath = ofd.FileName;

                KeyValuePair<string, List<int>> data = JsonSerializer.Deserialize
                    <KeyValuePair<string, List<int>>>(File.OpenRead(tempFilePath));

                TextInputBox.Text = data.Key;
                KeyInputBox.Text = string.Join('.', data.Value.Remove(0) ? data.Value : data.Value);
            }
        }
    }
}
