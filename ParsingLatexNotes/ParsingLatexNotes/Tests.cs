using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ParsingLatexNotes
{
    public partial class Form1 : Form
    {
        public void runTests()
        {
            // getFileName
            string result = getFileName("hi\\hello\\");
            if (getFileName("hello.txt") != "hello") throw new Exception();
            if (getFileName("hello") != "hello") throw new Exception();
            if (getFileName("hi\\hello.txt") != "hello") throw new Exception();
            if (getFileName("hi\\hello") != "hello") throw new Exception();
            if (getFileName("hi\\hello\\") != "hello\\") throw new Exception();

            // splitByLine
            {
                List<string> split1 = new List<string>() {
                    "\nh\ne\n\nllo\n"
                    // "\ne\n\nllo\n"
                };
                List<string> exp = new List<string>() {
                    "", "h", "e", "", "llo", ""
                    //"", "e", "", "llo", ""
                };
                List<string> buh = splitByLine(split1);

                // if (exp != buh) { throw new Exception(); }

                if (exp.Count != buh.Count) {
                    throw new Exception();
                }
                for (int i = 0; i < exp.Count; i++)
                {
                    if (exp[i] != buh[i])
                    {
                        throw new Exception();
                    }
                }
            }

            {
                List<string> splits = new List<string>() {
                "he\nll\no",
                "",
                "HELLO",
                "\n",
                "\nh\ne\n\nllo\n"
            };
                List<string> expected = new List<string>()
            {
                "he", "ll", "o",
                "",
                "HELLO",
                "", "",
                "", "h", "e", "", "llo", ""
            };

                List<string> yo = splitByLine(splits);
                if (expected.Count != yo.Count)
                {
                    throw new Exception();
                }
                for (int i = 0; i < expected.Count; i++)
                {
                    if (expected[i] != yo[i])
                    {
                        throw new Exception();
                    }
                }
            }
        }

        /*
        Valid

        Valid
         * ##da
         * ##df
         * #df#
         * #da#

        */
    }
}
