This is a repository for parsing latex notes by tags. The purpose is to to be able to apply filters and save time making study notes. 
Here is the dictionary for the current tags

        Dictionary<string, string> tags = new Dictionary<string, string>(){
            {"Defintion", "d"},
            {"Algorithm", "a"},
            {"List", "l"},
            {"Theorem", "t"},
            {"Method", "m"}, // aka solution technique
            {"Proof", "p"},
            {"Formula", "f"},
            {"Example", "e"},
            {"Code", "c"},
            {"Notation", "n"},
            {"Solution", "s"},
            {"Grid/Table", "g"}
        };

The syntax is ##d to begin a definition and #d# to end a definition. That is, 

##d
photosynthesis - ...
#d#

Note that ##d and #d# must have their own separate lines (whitespace on either side is fine though).

You can also use combinations of letters to combine tags. Here is the current list of those

        List<string> double_tags = new List<string>()
        {
            "dt", // theoerem with a name, which is also treated as a definition
            "da", // algorithm with a name, which is also treated as a definition
            "dm", // method/technique with a name, which is also treated as a definition
            "ld"  // list of items, where each item is its own definition
        };


The title of the file must be of the form: <course> - <lecture number>. For example, 

CS 245 - 1.tex



-----------------------------------------------
Parse Latex Notes
-----------------------------------------------

This is a work in progress C# program for parsing the notes

When you apply a filter, whenever it comes a desired tag, it pushes it to a stack.
When a closing tag is encountered the tag is popped from the stack.
The program reads any lines when the stack is non-empty.