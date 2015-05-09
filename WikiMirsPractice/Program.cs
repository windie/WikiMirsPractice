using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Xml;

namespace WikiMirsPractice {

    public class PageParser {
        List<string> TextToFormulas(string str) {
            List<string> res = new List<string>();
            int pos = str.IndexOf("<math>", 0);
            while (pos != -1 && pos < str.Length) {
                int pos2 = str.IndexOf("</math>", pos + 1);
                if (pos2 == -1) {
                    break;
                } else {
                    res.Add(str.Substring(pos, pos2 + "</math>".Length - pos + 1));
                }
                pos = str.IndexOf("<math>", pos2 + "</math>".Length + 1);
            }
            return res;
        }

        public List<string> Parse(string xmlpath) {
            XmlReaderSettings settings = new XmlReaderSettings();
            settings.IgnoreWhitespace = true;
            XmlReader reader = XmlReader.Create(xmlpath, settings);
            List<string> restext = new List<string>();
            while (reader.Read()) {
                if (reader.NodeType == XmlNodeType.Element && reader.Name == "text") {
                    reader.Read();
                    string rawtext = reader.Value;
                    restext = TextToFormulas(rawtext);
                    break;
                }
            }
            return restext;
        }
    }

    class Program {
        static void Main(string[] args) {
            PageParser p = new PageParser();
            List<string> res = p.Parse("../../data/Singular_value.xml");
            foreach (string s in res) {
                Console.WriteLine(s);
            }
        }
    }
}
