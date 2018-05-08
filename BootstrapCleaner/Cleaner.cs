using System;
using System.Collections;
using System.Collections.Generic;

namespace BootstrapCleaner
{
    //currently can find all classes in html
    public class Cleaner : iCleaner
    {
        public string Clean(string Input)
        {
            //string result = findHtmlClasses(Input);

            //delete on final version and change to return a list
            //List<string> splits = findHtmlClasses(Input);
             List<string> splits = findCssClasses(Input);
                string r = "";
             foreach(string s in splits)
             {
                 r += "++" + s + "++";
             }
             return r;
             //delete on final version and change to return a list

            
            //return result;
        }

        string html_Class_Delimiter = "class=";
        string css_Class_Delimiter = ".";

        string html_Id_Delimiter = "id=";
        string css_Id_Delimiter = "#";

        char[] cssSelectors = new char[] { ',','>', '+', '~', '[', ':', '{' , ' ', '\\' };
        string[] escapeStrings = new string[] { "/t", "/n" };

        private bool isCssSelector(char c)
        {
            bool found = false;
            int i = -1;
            while(!found && i < cssSelectors.Length -1)
            {
                i++;
                if (c == cssSelectors[i])
                {
                    return true;
                }
               
            }
            return false;
        } //used in thhe css classes splitter

        List<string> htmlClasses = new List<string>();

        List<string> cssClasses = new List<string>();

        private List<string> spaceSplit(List<string> Input) {
            List<string> result = new List<string>();
            foreach (string s in Input) {

                string current = s;
                bool spaces = true;

                while (spaces) 
                {
                    int posSpace = current.IndexOf(" ");
                    if (posSpace >= 0)
                    {
                        int StopIndex = current.Length - posSpace;
                        result.Add(current.Substring(0, posSpace));
                        current = current.Substring(posSpace+1, current.Length - posSpace -1);
                    }
                    else
                    {
                        result.Add(current);
                        spaces = false;
                    }
                }
            }
            return result;
        } //splits html classes based on the spaces

        private List<string> findHtmlClasses(string Input) {
            string result = "";
            for (int i = 0; i < Input.Length; i++) {
                char s = Input[i];
                if (Input[i] == 'C' || Input[i] == 'c')
                    if (Input[i+1] == 'L' || Input[i+1] == 'l')
                        if (Input[i+2] == 'A' || Input[i+2] == 'a')
                            if (Input[i+3] == 'S' || Input[i+3] == 's')
                                if (Input[i+4] == 'S' || Input[i+4] == 's')
                                    if (Input[i+5] == '=')
                                    {
                                        int last = Input.IndexOf("\"", i + 7);// start at i+7 because i+6 is the first quote
                                        if (last==-1)
                                        {
                                            last = Input.IndexOf("\"", i + 7);
                                        }
                                        if (last == -1) { break; }

                                        int subLength = last - (i + 7);

                                       htmlClasses.Add(Input.Substring(i + 7, subLength));

                                      
                                    }
            }
            return spaceSplit(htmlClasses);
        }

        private void deleteCssBraces(ref string Input) //delets the contents of braces but not the braces so they can be used to delimit the end of a css class
        {
            //find first brace
            bool bfound = false;
            int lastpos = 0;
            while (!bfound)
            {

               

                int startpos = Input.IndexOf('{',lastpos);
                if (startpos >= 0)
                {
                    int endpos = Input.IndexOf('}', startpos);
                    string firstHalf = Input.Substring(0, startpos+1);
                    string secondHalf = Input.Substring(endpos, Input.Length-(endpos));
                    Input = firstHalf + secondHalf;
                    lastpos = startpos+1;
                }
                else
                {
                    bfound = true;
                }
            }
            
        }
        
        private List<string> findCssClasses(string Input) {
            List<string> result = new List<string>();


            deleteCssBraces(ref Input);
            string Temp = Input;

            for (int i = 0; i < Temp.Length - 1; i++)
            {



                int startpos = -1;
                if (Temp[i] == '.')
                {
                    startpos = i + 1;
                }

                //loop through remaining characters and check if its a selector
                if (startpos >= 0)
                {
                    int endpos = -1;
                    bool bfound = false;
                    for (int j = startpos; j < Temp.Length - 1 && !bfound; j++)
                    {
                        //loop through css selectors
                        for (int k = 0; k < cssSelectors.Length - 1 && !bfound; k++)
                        {
                            if (Temp[j] == cssSelectors[k])
                            {
                                endpos = j - 1;
                                bfound = true;
                            }
                        }

                    }

                    if (bfound)
                    {
                        cssClasses.Add(Temp.Substring(startpos, endpos - startpos +1));
                        //reassess the end condition for a class

                        string cut = Temp.Substring(endpos, Temp.Length - endpos);
                        Temp = cut;

                    }
                }


            }


            return cssClasses;
            return result;

        }
        
       /* private List<string> removeEscapeChars(List<string> Inputs)
        {
            //delete all found escape strings
           
            List<string> Results = new List<string>();
            foreach(string Input in Inputs)
            {
                bool bfound = true;
                foreach (string escapeString in escapeStrings)
                {
                    while (bfound)
                    {
                        int pos = Input.IndexOf(escapeString);
                        if(pos > 0)
                        {
                            string firstHalf = Input.Substring(0, pos - 1);
                            string 
                        }
                    }
                }
            }



            return Results;
        }*/
    

    }

    public interface iCleaner {

          string Clean(string Input);
    }
    
}
