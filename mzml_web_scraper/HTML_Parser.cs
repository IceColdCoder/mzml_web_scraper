using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//Debug.
using System.Diagnostics;
using System.Collections.Specialized;
using System.Reflection.Emit;
using System.Collections;
using System.Text.RegularExpressions;

namespace mzml_web_scraper
{

    public class HTML_ELEMENT
    {
        public string _type;
        public string _params;
        public string _content;
        public HTML_ELEMENT _parent;

        public HTML_ELEMENT(string _type, string _params, string _content, HTML_ELEMENT _parent=null)
        {
            this._type = _type;
            this._params = _params;
            this._content = _content;
            this._parent = _parent;
        }

        public override string ToString()
        {
            return "HTML_ELEMENT(type=" + _type + ", params=" + _params + ", content=" + _content + ",parent=(" + (_parent is null ? "NULL": _parent._type) + ")";
        }
    }
    public class HTMLDom
    {

        public class HTML_HEAD
        {

        }

        public class HTML_BODY
        {

        }

    }
    public static class HTML_Parser
    {

        static readonly string specfile_defaults = Directory.GetCurrentDirectory() + "\\specs\\html_spec_defaults.txt";
        static readonly string specfile_singles = Directory.GetCurrentDirectory() + "\\specs\\html_spec_singles.txt";
        static readonly string specfile_comments = Directory.GetCurrentDirectory() + "\\specs\\html_spec_comments.txt";
        private static OrderedDictionary html_spec_openers;
        private static OrderedDictionary html_spec_closers;

        private static readonly string[] specfiles = {"html_audio_video.csv","html_basic","html_formatting","html_forms_and_input.csv",
            "html_frames.csv","html_images.csv","html_links.csv","html_lists.csv","html_metainfo.csv","html_programming.csv",
            "html_styles_semantics.csv","html_tables.csv" };

        private static OrderedDictionary html_spec;

        static HTML_Parser()
        {
            html_spec = new OrderedDictionary();
/*            html_spec_openers = new OrderedDictionary();
            html_spec_closers = new OrderedDictionary();
            Read_HTML_Spec_Defaults(specfile_defaults);
            Read_HTML_Spec_Singles(specfile_defaults);
            Read_HTML_Spec_Comments(specfile_defaults);*/
        }

        private static void Read_HTML_Spec_Defaults(string f_name)
        {
            using (StreamReader file = new StreamReader(f_name))
            {
                string line = "";
                string html_pattern = @"^(<(?'Tag'([^<>]*))>)?";

                Regex html_regex = new Regex(html_pattern, RegexOptions.IgnoreCase | RegexOptions.Compiled);

                while ((line = file.ReadLine()) != null)
                {

                    if (line.Length > 1)
                    {
                        Match match = html_regex.Match(line);
                        Debug.WriteLine(line);

                        string html_tag_opener = match.Groups[match.Groups.Count - 1].Value;
                        

                        if (html_tag_opener.Length > 1)
                        {
                            html_spec_openers.Add(html_tag_opener, html_tag_opener);
                            Debug.WriteLine("\t==>" + html_tag_opener + " :: " + html_tag_opener);
                        }
                        else
                        {
                            string html_tag_closer = html_tag_opener.Insert(0, "/");
                            html_spec_openers.Add(html_tag_opener, html_tag_closer);
                            html_spec_closers.Add(html_tag_closer, html_tag_opener);
                            Debug.WriteLine("\t==>" + html_tag_opener + " :: " + html_tag_closer);
                        }
                    }
                }
            }
        }


        private static void Read_HTML_Spec_Singles(string f_name)
        {
            using (StreamReader file = new StreamReader(f_name))
            {
                string line = "";
                string html_pattern = @"^(<(?'Tag'([^<>]*))>)?";

                Regex html_regex = new Regex(html_pattern, RegexOptions.IgnoreCase | RegexOptions.Compiled);

                while ((line = file.ReadLine()) != null)
                {

                    if (line.Length > 1)
                    {
                        Match match = html_regex.Match(line);
                        Debug.WriteLine(line);

                        string html_tag_opener = match.Groups[match.Groups.Count - 1].Value;


                        if (html_tag_opener.Length > 1)
                        {
                            html_spec_openers.Add(html_tag_opener, html_tag_opener);
                            Debug.WriteLine("\t==>" + html_tag_opener + " :: " + html_tag_opener);
                        }
                        else
                        {
                            string html_tag_closer = html_tag_opener.Insert(0, "/");
                            html_spec_openers.Add(html_tag_opener, html_tag_closer);
                            html_spec_closers.Add(html_tag_closer, html_tag_opener);
                            Debug.WriteLine("\t==>" + html_tag_opener + " :: " + html_tag_closer);
                        }
                    }
                }
            }
        }

        private static void Read_HTML_Spec_Comments(string f_name)
        {
            using (StreamReader file = new StreamReader(f_name))
            {
                string line = "";
                string html_pattern = @"^(<(?'Tag'([^<>]*))>)?";

                Regex html_regex = new Regex(html_pattern, RegexOptions.IgnoreCase | RegexOptions.Compiled);

                while ((line = file.ReadLine()) != null)
                {

                    if (line.Length > 1)
                    {
                        Match match = html_regex.Match(line);
                        Debug.WriteLine(line);

                        string html_tag_opener = match.Groups[match.Groups.Count - 1].Value;


                        if (html_tag_opener.Length > 1)
                        {
                            html_spec_openers.Add(html_tag_opener, html_tag_opener);
                            Debug.WriteLine("\t==>" + html_tag_opener + " :: " + html_tag_opener);
                        }
                        else
                        {
                            string html_tag_closer = html_tag_opener.Insert(0, "/");
                            html_spec_openers.Add(html_tag_opener, html_tag_closer);
                            html_spec_closers.Add(html_tag_closer, html_tag_opener);
                            Debug.WriteLine("\t==>" + html_tag_opener + " :: " + html_tag_closer);
                        }
                    }
                }
            }
        }

        private static void Read_HTML_CSV(string f_name, char delimiter)
        {
            using (StreamReader file = new StreamReader(f_name))
            {
                string line = "";

                while ((line = file.ReadLine()) != null)
                {
                    if (line.Length > 1)
                    {
                        string[] line_seg = line.Split(delimiter);
                        if (line_seg[1].Length == 0)
                        {
                            html_spec.Add(line_seg[0], line_seg[0]);
                        }
                        else
                        {
                            html_spec.Add(line_seg[0], line_seg[1]);
                            html_spec.Add(line_seg[1], line_seg[0]);
                        }
                    }
                }
            }
        }

        private static string Regex_MatchCollection_ToString(MatchCollection collection)
        {
            string s = "";

            foreach(Match match in collection)
            {
                s += "Captures for " + match.Value + ":";
                foreach(Capture capture in match.Captures)
                {
                    s += "\n\t" + "Index(" + capture.Index + ", Value(" + capture.Value + ")";
                }
                s += Environment.NewLine;
            }

            return s;
        }

        public static string HTML_Spec_ToString()
        {
            StringBuilder sb = new StringBuilder("[");
            IDictionaryEnumerator _e = html_spec_openers.GetEnumerator();
            while(_e.MoveNext())
            {
                sb.Append("(" + _e.Key + "," + _e.Value + "),\n");
            }
            sb[sb.Length - 3] = ']';
            return sb.ToString();
        }

        public static HTMLDom Parse_HTML(string html)
        {
            HTMLDom Dom = new HTMLDom();

            string html_pattern = @"^((<)(?<Tag>[\S^<>]+)(\s*)(?<Params>.*)(>))?";
            Regex html_regex = new Regex(html_pattern, RegexOptions.IgnoreCase | RegexOptions.Compiled);

            //Debug.WriteLine(html);
            Stack<char> S = new Stack<char>(html.ToCharArray().Reverse());
            //Array.ForEach(S.ToArray(), e => Debug.Write(e));
            //Debug.WriteLine("");

            Stack<string> MS = new Stack<string>();

            ArrayList clist = new ArrayList();

            int reader_state = 0; //0-Default, 1-Script, 2-Comments

            while (S.Count() != 0)
            {

                //If the code finds what looks like an html statement it will iterate over it using a stack in order to
                //ensure that is is a regular statement.
                char c = S.Pop();
                if (c.Equals('<'))
                {
                    clist.Clear();
                    clist.Add(c);

                    while(S.Count != 0 && !c.Equals('>'))
                    {
                        c = S.Pop();
                        //Debug.WriteLine(c);
                        clist.Add(c);
                    }

                    string p = new string((char[])clist.ToArray(typeof(char))).ToLowerInvariant();
                    Debug.WriteLine(p);
                    //Now handle it using a regex expression.
                    //Array.ForEach(clist.ToArray(), e => Debug.Write(e));
                    //Debug.WriteLine("");
                    Match M = html_regex.Match(p);
                    //GroupCollection G = M.Groups;
                    //Debug.WriteLine("\t" + M.Name + " :: " + M.Value);
                    //foreach(Group g in G)
                    //{
                    //    Debug.WriteLine("\t\t" + g.Name + " :: " + g.Value);
                    //}
                    /*Debug.WriteLine("" + M.Groups[5].Name + " :: " + M.Groups[5].Value);*/

                    //If prog finds opening bracket then simply push.
                    if (html_spec_openers.Contains(M.Groups[5].Value))
                    {
                        if (html_spec_openers[M.Groups[5].Value].Equals(M.Groups[5].Value))
                        {
                            string _element_type = (string)M.Groups[5].Value;
                            HTML_ELEMENT _element = new HTML_ELEMENT(_element_type, "", "");
                            Log.WriteTime("Indentified element type=" + _element_type);
                        }
                        else
                        {
                            Log.WriteTime("Pushed onto MStack='" + M.Groups[5].Value + "'");
                            MS.Push(M.Groups[5].Value);
                        }

                    }
                    //Check to see if the prog found a html dom child element. If not then prog expects one of more child objects.
                    else if (html_spec_closers.Contains(M.Groups[5].Value))
                    {
                        string closer_check = MS.Peek();
                        //Check for matching open and closing bracket.
                        Log.WriteTime("Checked HTML DOM potential match: Stack.Peek()='" 
                            + closer_check + "', Regex Match='" + M.Groups[5].Value + "', Openers,Closers='," 
                            + html_spec_closers[M.Groups[5].Value] + "," + html_spec_openers[closer_check]);

                        if (html_spec_openers[closer_check].Equals(M.Groups[5].Value))
                        {
                            MS.Pop();
                            Log.WriteTime("\tSuccess");
                            Debug.Assert(html_spec_closers[M.Groups[5].Value] is string);
                            string _element_type = (string)html_spec_closers[M.Groups[5].Value];
                            Log.WriteTime("Indentified element type=" + _element_type);
                            HTML_ELEMENT _element = new HTML_ELEMENT(_element_type, "", "");
                        }
                    }
                    else
                    {
                        Log.WriteTime("Failed to parse '" + M.Groups[5].Value + "' with name '" + M.Groups[5].Name + "'.");
                    }
                    Debug.WriteLine("\t" + M.Groups[5].Value + " :: " + (html_spec_openers.Contains(M.Groups[5].Value) ? html_spec_openers[M.Groups[5].Value] : html_spec_closers[M.Groups[5].Value]));

                }
            }

            if (MS.Count != 0)
            {
                Log.WriteTime("ERROR: Failed to fully unwind MatchStack with " + MS.Count + " remaining");
                Log.WriteTime("\tUnwinding Stack...");
                foreach (string er in MS)
                {
                    Log.WriteTime("\t\t" + er);
                }
            }
            return Dom;
        }
    }

}
