using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ParsingLatexNotes
{
    public partial class Form1 : Form
    {
        string original_directory = AppDomain.CurrentDomain.BaseDirectory + "Original\\";
        string parsed_directory = AppDomain.CurrentDomain.BaseDirectory + "Parsed\\";
        string extracted_path = AppDomain.CurrentDomain.BaseDirectory + "extracted.tex";
        
        string file_title = "";
        List<string> file_lines;
        // List<string> new_file_lines;

        // parsed file entries
        string course_code; // ************************???????????????
        int course_num;
        int lecture_num;
        string parsed_file_title;

        // Tag Names
        Dictionary<string, string> tags = new Dictionary<string, string>(){
            {"Defintion", "d"},
            {"Algorithm", "a"},
            {"List", "l"},
            {"Theorem", "t"},
            {"Method", "m"},
            {"Proof", "p"},
            {"Formula", "f"},
            {"Example", "e"},
            {"Code", "c"},
            {"Notation", "n"},
            {"Solution", "s"},
            {"Grid/Table", "g"}
        };

        List<string> double_tags = new List<string>()
        {
            "dt", // -> d then t
            "da",
            "dm",
            "ld" // -> d for every last - or item
        };

        List<string> extraction_tags = new List<string>();

        List<string> parsed_courses = new List<string>();
        
        public Form1()
        {
            InitializeComponent();

            /*
            FileStream fs = new FileStream("test.tex", FileMode.Create);
            StreamWriter sw = new StreamWriter(fs);

            sw.Write("hello world");

            sw.Close();
            fs.Close();
            */

            runTests();

            addDirectories();
            foreach (KeyValuePair<string, string> tag in tags)
            {
                cbTag.Items.Add(tag.Key);
            }
            updateCBCourse();
        }

        void checkSyntax()
        {
            string errors = "";

            #region Course, date & lecture #
            List<string> pieces = splitAsList(file_title, " - ");
            if (pieces.Count != 2)
            {
                errors += "Incorrect title format: must separate course and lecture # with ' - '\n";
            }
            try
            {
                string first = pieces.First().Trim();
                string last = pieces.Last().Trim();

                int space = inString(first, " ");
                if (space == -1)
                {
                    throw new Exception();
                }
                course_code = first.Substring(0, space).ToUpper();
                course_num = Convert.ToInt32(first.Substring(space, first.Length - space).Trim());
                lecture_num = Convert.ToInt32(last);
                parsed_file_title = course_code + " " + course_num.ToString() + " - " + intToString(lecture_num, 3);
            }
            catch
            {
                errors += "Incorrect title format: it must be of the form: MATH 239 - 3, where the last number is the lecture #\n";
            }
            #endregion

            #region matched notation

            Stack<string> matched = new Stack<string>();
            foreach (string line in file_lines) {
                string trimmed = line.Trim();
                if (trimmed.Length >= 3)
                {
                    if (trimmed[0] == '%') {
                        string tag = trimmed.Replace("%", String.Empty).Trim();
                        if (trimmed[1] == '%')
                        {
                            foreach (string entry in matched)
                            {
                                if (entry == tag)
                                {
                                    errors += tag + " already exists\n";
                                }
                            }
                            matched.Push(tag);
                        }
                        else if (trimmed[trimmed.Length - 1] == '%')
                        {
                            if (matched.Count <= 0 || matched.Peek() != tag)
                            {
                                errors += "Unmatched ending: " + trimmed + "\n";
                            }
                            else
                            {
                                matched.Pop();
                            }
                        }
                    }
                }
            }

            while (matched.Count > 0)
            {
                errors += "Unmatched beginning: " + matched.Pop() + "\n";
            }

            #endregion

            if (errors != "") throw new Exception(errors);
        }

        /*
        void splitDoubleSyntax()
        {
            Stack<KeyValuePair<string, int>> large_tags = new Stack<KeyValuePair<string, int>>();

            for (int i = 0; i < file_lines.Count; i++)
            {
                string line = file_lines.ElementAt(i).Trim();
                if (line.Length > 3 && line.Substring(0, 1) == "#")
                {
                    string tag = line.Replace("#", String.Empty);
                    if (line.Substring(line.Length - 1, 1) == "#")
                    {
                        large_tags.Push(new KeyValuePair<string, int>(tag, i));
                    }
                    else if (line.Substring(1, 1) == "#")
                    {
                        KeyValuePair<string, int> top = large_tags.Pop();
                        if (top.Key != tag) { throw new Exception("Unmatched double syntax"); }

                        
                    }
                }
            }

            if (large_tags.Count > 0)
            {
                throw new Exception("Unmatched double syntax");
            }
        }
        */

        void parseLists()
        {
            Stack<int> tag_line_numbers = new Stack<int>();

            for (int i = 0; i < file_lines.Count; i++)
            {
                string line = file_lines.ElementAt(i).Trim();
                if (line.Length >= 3 && line.Contains("l") && line.Substring(0, 1) == "%")
                {
                    string tag = line.Replace("%", String.Empty);
                    if (line.Substring(1, 1) == "%")
                    {
                        tag_line_numbers.Push(i);
                    }
                    else if (line.Substring(line.Length - 1, 1) == "%")
                    {
                        int top = tag_line_numbers.Pop();

                        bool contains_item = false;
                        for (int a = top + 1; a < i; a++)
                        {
                            if (file_lines.ElementAt(a).Contains("\\item"))
                            {
                                contains_item = true;
                                break;
                            }
                        }

                        if (!contains_item)
                        {
                            int b = i - 1;
                            string element = file_lines.ElementAt(b).Trim();

                            if (element.Length > 0 && element.Substring(0, 1) == "-")
                            {
                                file_lines[b] = "  \\item " + element.Substring(1) + "\n\\end{enumerate}";
                                b--;
                                element = file_lines.ElementAt(b).Trim();

                                while (b < top && element.Length > 0 && element.Substring(0, 1) == "-")
                                {
                                    file_lines[b] = "  \\item " + element.Substring(1);
                                    
                                    b--;
                                    element = file_lines.ElementAt(b).Trim();
                                }

                                file_lines[b + 1] = "\\begin{enumerate}\n" + file_lines[b + 1];
                            }
                        }
                    }
                }
            }

            if (tag_line_numbers.Count > 0)
            {
                throw new Exception("Unmatched double syntax");
            }

            file_lines = splitByLine(file_lines);
        }

        string intToString(int n, int length)
        {
            string result = n.ToString();
            int counter = result.Length;
            while (counter < length)
            {
                result = "0" + result;
                counter++;
            }
            return result;
        }

        public int inString(string text, string substring)
        {
            // gives position of substring the first time it occurs in the text
            if (text == null || substring == null)
            {
                return -1;
            }

            for (int a = 0; a <= text.Length - substring.Length; a++)
            {
                if (text.Substring(a, substring.Length) == substring) { return a; }
            }

            return -1;
        }

        public List<string> splitAsList(string text, string splitter)
        {
            List<string> result = new List<string>();
            int in_string = inString(text, splitter);
            string entry = "";

            while (in_string != -1)
            {
                if (in_string != 0)
                {
                    entry = text.Substring(0, in_string).Trim();
                    if (entry.Length > 0) { result.Add(entry); }
                }

                if (text.Length <= in_string + splitter.Length)
                {
                    return result;
                }
                else
                {
                    text = text.Substring(in_string + splitter.Length, text.Length - (in_string + splitter.Length));
                }
                in_string = inString(text, splitter);
            }
            if (text != "")
            {
                result.Add(text);
            }

            return result;
        }

        public List<string> splitByLine(List<string> l)
        {
            List<string> result = new List<string>();

            foreach (string element in l)
            {
                int prev = 0;
                int in_str = inString(element, "\n");

                while (in_str != -1)
                {
                    in_str += prev;
                    if (in_str > prev)
                    {
                        result.Add(element.Substring(prev, in_str - prev));
                    }
                    else
                    {
                        result.Add("");
                    }
                    prev = in_str + 1;
                    if (prev >= element.Length)
                    {
                        in_str = -1;
                    }
                    else
                    {
                        in_str = inString(element.Substring(prev), "\n");
                    }
                }
                if (prev < element.Length)
                {
                    result.Add(element.Substring(prev));
                }
                else if (prev == element.Length)
                {
                    result.Add("");
                }
            }

            return result;
        }

        public List<string> FileToList(string file_path) {
            FileStream fs = new FileStream(file_path, FileMode.Open);
            StreamReader sr = new StreamReader(fs);

            string input = sr.ReadToEnd();

            sr.Close();
            fs.Close();

            input = input.Replace("\r", String.Empty);
            return splitAsList(input, "\n");
        }

        public string getFileName(string file_path)
        {
            if (file_path == null)
            {
                return "";
            }

            int counter = file_path.Length - 2;

            while (counter >= 0 && file_path.Substring(counter, 1) != "\\") { counter--; }

            counter++;
            if (counter >= file_path.Length) { return ""; }

            string result = file_path.Substring(counter, file_path.Length - counter);
            counter = result.Length - 1;
            while (counter >= 0 && result[counter] != '.') { counter--; }
            if (counter <= 0) { counter = result.Length; }
            return result.Substring(0, counter);
        }

        public void addDirectories()
        {
            if (!Directory.Exists(original_directory))
            {
                Directory.CreateDirectory(original_directory);
            }
            if (!Directory.Exists(parsed_directory))
            {
                Directory.CreateDirectory(parsed_directory);
            }
        }

        public int TimesInString(string text, string substring)
        {
            // Finds the number of times a substring occurs in a string (the substrings do not overlap)

            int output = 0;

            if (text != null && substring != null)
            {
                if (text.Length >= substring.Length)
                {
                    for (int a = 0; a <= (text.Length - substring.Length); a++)
                    {
                        if (text.Substring(a, substring.Length) == substring)
                        {
                            output++;
                            a += substring.Length - 1;
                        }
                    }
                }
            }

            return output;
        }

        private void btnParseLatex_Click(object sender, EventArgs e)
        {
            string[] files = Directory.GetFiles(AppDomain.CurrentDomain.BaseDirectory, "*.tex");

            addDirectories();
            
            foreach (string file in files)
            {
                file_title = getFileName(file);
                file_lines = FileToList(file);

                rtbOutput.Text += "Checking " + file_title + "\n";

                try
                {
                    checkSyntax();

                    string course = course_code + " " + course_num.ToString();

                    string new_o_directory = original_directory + course + "\\";
                    string new_p_directory = parsed_directory + course + "\\";
                    if (!Directory.Exists(new_o_directory))
                    {
                        Directory.CreateDirectory(new_o_directory);
                    }
                    if (!Directory.Exists(new_p_directory))
                    {
                        Directory.CreateDirectory(new_p_directory);
                    }

                    File.Move(file, new_o_directory + parsed_file_title + ".tex");
                    
                    // splitDoubleSyntax();

                    parseLists();

                    FileStream fs = new FileStream(new_p_directory + parsed_file_title + ".tex", FileMode.Create);
                    StreamWriter sw = new StreamWriter(fs);

                    foreach (string line in file_lines)
                    {
                        sw.WriteLine(line);
                    }

                    sw.Close();
                    fs.Close();
                    
                    if (!parsed_courses.Contains(course)) { parsed_courses.Add(course); }
                    rtbOutput.Text += "No Errors\n";
                }
                catch (Exception ex)
                {
                    rtbOutput.Text += "Errors:\n" + ex.Message;
                }

                rtbOutput.Text += "-------------------------------\n";
            }
        }

        private void btnAddTag_Click(object sender, EventArgs e)
        {
            string input_tag = cbTag.Text.Trim();
            if (tags.ContainsKey(input_tag))
            {
                if (extraction_tags.Contains(input_tag))
                {
                    MessageBox.Show("Tag already exists");
                }
                else
                {
                    extraction_tags.Add(input_tag);
                    rtbTags.Text += input_tag + "\n";
                }
            }
            else
            {
                MessageBox.Show("Enter a proper tag");
            }
        }

        void updateCBCourse()
        {
            parsed_courses = new List<string>();
            string[] files = Directory.GetDirectories(parsed_directory);
            foreach (string file in files)
            {
                string new_entry = getFileName(file).Replace("\\", String.Empty);
                parsed_courses.Add(new_entry);
                cbCourse.Items.Add(new_entry);
            }
        }

        private void btnExtract_Click(object sender, EventArgs e)
        {
            if (extraction_tags.Count <= 0)
            {
                MessageBox.Show("Enter at least 1 tag");
                return;
            }

            string course = cbCourse.Text.Trim();
            string directory = parsed_courses + course;
            if (!parsed_courses.Contains(course) || !Directory.Exists(directory))
            {
                MessageBox.Show("Enter a proper course");
                return;
            }

            // Get Extraction Tag Values
            List<string> extraction_tag_values = new List<string>();
            foreach (string tag in extraction_tags)
            {
                string value;
                if (tags.TryGetValue(tag, out value))
                {
                    extraction_tag_values.Add(value);
                }
            }

            List<string> files = Directory.GetFiles(directory).ToList();
            files.Sort();

            File.Create(extracted_path);

            List<string> extra = new List<string>();
            extra.Add("\\documentclass{article}");

            foreach (string file in files)
            {
                List<string> read = FileToList(file);
                List<string> to_enter = new List<string>();
                Stack<string> read_tags = new Stack<string>();

                foreach (string line in read)
                {
                    string trimmed = line.Trim();

                    if (TimesInString(trimmed, "\\newtheorem") == 1) // other types of lines
                    {
                        string match = Regex.Match(trimmed, "\\newtheorem{.+}").ToString();
                        bool use_it = true;
                        foreach (string extra_element in extra)
                        {
                            if (extra_element.Contains(match))
                            {
                                use_it = false;
                                break;
                            }
                        }
                        if (use_it)
                        {
                            extra.Add(trimmed);
                        }
                    }
                    else if (TimesInString(trimmed, "\\usepackage") == 1)
                    {
                        string match = Regex.Match(trimmed, "\\usepackage{.+}").ToString();
                        bool use_it = true;
                        foreach (string extra_element in extra)
                        {
                            if (extra_element.Contains(match))
                            {
                                use_it = false;
                                break;
                            }
                        }
                        if (use_it)
                        {
                            extra.Add(trimmed);
                        }
                    }
                    else if (trimmed.Length >= 3)
                    {
                        if (trimmed[0] == '%')
                        {
                            string tag = trimmed.Replace("%", String.Empty).Trim();

                            foreach (char c in tag)
                            {
                                if (extraction_tag_values.Contains(c.ToString()))
                                {
                                    if (trimmed[1] == '%')
                                    {
                                        read_tags.Push(tag);
                                    }
                                    else if (trimmed[trimmed.Length - 1] == '%')
                                    {
                                        read_tags.Pop();
                                    }
                                    break;
                                }
                            }
                        }
                        else
                        {
                            if (read_tags.Count > 0)
                            {
                                to_enter.Add(line);
                            }
                        }
                    }
                    else
                    {
                        if (read_tags.Count > 0)
                        {
                            to_enter.Add(line);
                        }
                    }
                }

                extra.Add("\\begin{document}");
                                
                FileStream fs = new FileStream(extracted_path, FileMode.Append);
                StreamWriter sw = new StreamWriter(fs);

                foreach (string line in extra)
                {
                    sw.WriteLine(line);
                }

                foreach (string line in to_enter)
                {
                    sw.WriteLine(line);
                }

                sw.WriteLine("\\end{document}");

                sw.Close();
                fs.Close();
            }

            // Reset
            extraction_tags = new List<string>();
            rtbTags.Text = "";
        }

        /* TO DO
         * separate double syntax
         * latex format
         * 
         * generate **DONE**
             * UI   **DONE**
             * functionality **DONE**
        
         * extract
            * document **DONE**
            * consider double syntax **DONE**
         * check syntax double syntax **DONE**
        */
    }
}
