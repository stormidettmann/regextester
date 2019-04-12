using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using System.IO;

namespace SpragueRegex
{
    class RegexTest
    {
        static void Main(string[] args)
        {
            char choice;
            bool shouldContinue = true;

            while (shouldContinue){
                choice = Menu();
                shouldContinue = Execute(choice);
            }
            
        }

        private static bool Execute(char choice)
        {
            string input;

            if(choice == 'q')
            {
                return false;
            }

            input = getInput();

            switch (choice) {
                case 'a':
                    _SocialSecurity(input);
                    break;
                case 'b':
                    _PhoneNumber(input);
                    break;
                case 'c':
                    _EmailAddress(input);
                    break;
                case 'd':
                    _Name(input);
                    break;
                case 'e':
                    _Date(input);
                    break;
                case 'f':
                    _StreetAddress(input);
                    break;
                case 'g':
                    _CityStateZip(input);
                    break;
                case 'h':
                    _MilitaryTime(input);
                    break;
                case 'i':
                    _Currency(input);
                    break;
                case 'j':
                    _URL(input);
                    break;
                case 'k':
                    _Password(input);
                    break;
                case 'l':
                    _IONWords(input);
                    break;
            }
            //switch structure on choice to execute against input
            return true;
        }

        private static char Menu()
        {
            char choice;

            Console.Write("a. Social Security Number\n" +
                "b. US Phone Number\n" +
                "c. Email Address\n" +
                "d. Name on a class roster, assuming one or more middle initials - Last name, First name, MI\n" +
                "e. Date in MM-DD-YYYY format\n" +
                "f. House address - Street number, street name, abbreviation for road, street, boulevard or avenue\n" +
                "g. City followed by state followed by zip as it should appear on a letter\n" +
                "h. Military time, including seconds\n" +
                "i. US Currency down to the penny (ex: $123,456,789.23)\n" +
                "j. URL, including http:// (upper and lower case should be accepted)\n" +
                "k. A password that contains at least 10 characters and includes at least one upper case character, lower case character, digit, punctuation mark, and does not have more than 3 consecutive lower case characters\n" +
                "l. All words containing an odd number of alphabetic characters, ending in \"ion\"\n\n" +
                "q. quit\n\n");

            choice = Convert.ToChar(Console.ReadLine());

            return choice;
        }

        private static string getInput() //TODO No Error Checking Here
        {
            string input;
            input = Console.ReadLine();
            return input;
        }

        private static void _SocialSecurity(string input)
        {
            bool isMatch;
            string regex = @"^(\d{3}-\d{2}-\d{4})|(\d{9})$";
   
            isMatch = Regex.IsMatch(input, regex);

            report(input, isMatch, "SSN");
        }

        private static void _PhoneNumber(string input)
        {
            bool isMatch;
            string regex = @"^\(?(\d{3})\)?( |-)?(\d{3})( |-)?(\d{4})$";

            isMatch = Regex.IsMatch(input, regex);

            report(input, isMatch, "PHONE NUMBER");
        }

        // Does not check if the domain name exists, only that it is valid.
        //  found this regex on reglib.com
        private static void _EmailAddress(string input)
        {
            bool isMatch;
            string regex = @"^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$";

            isMatch = Regex.IsMatch(input, regex);

            report(input, isMatch, "EMAIL");
        }

        private static void _Name(string input)
        {
            bool isMatch;
            string regex = @"([A-Za-z.-]+\s*)+, [A-Za-z.-]*(, )?([A-Za-z.-])*";

            isMatch = Regex.IsMatch(input, regex);

            report(input, isMatch, "NAME");
        }

        private static void _Date(string input)
        {
            bool isMatch;
            string regex = @"^(\d{2})-(\d{2})-(\d{4})$";

            isMatch = Regex.IsMatch(input, regex);

            report(input, isMatch, "DATE");
        }

        private static void _StreetAddress(string input)
        {
            bool isMatch;
            string regex = @"\d{1,6}\s[a-zA-Z-]{1,30}\s[a-zA-Z]{2,15}";

            isMatch = Regex.IsMatch(input, regex);

            report(input, isMatch, "ADDRESS");
        }

        private static void _CityStateZip(string input)
        {
            bool isMatch;
            string regex = @"^[A-Za-z.-]*[, ]{0,2}[A-Za-z., ]{2,4}\d{5}(-\d{4})?$";

            isMatch = Regex.IsMatch(input, regex);

            report(input, isMatch, "CITY, STATE, ZIP");
        }

        private static void _MilitaryTime(string input)
        {
            bool isMatch;
            string regex = @"^([01]\d|2[0-3]):([0-5]\d):([0-5])\d$";

            isMatch = Regex.IsMatch(input, regex);

            report(input, isMatch, "MILITARY TIME");
        }

        // mostly proper commas ^\$\d{0,3},?(\d{3},?)*(\.\d{2})$
        //improper commas: ^\$-?(\d*,?\d{3})*(\.\d{2})$

        private static void _Currency(string input)
        {
            bool isMatch;
            string regex = @"^\$\d{0,3},?(\d{3},?)*(\.\d{2})$";

            isMatch = Regex.IsMatch(input, regex);

            report(input, isMatch, "CURRENCY");
        }

        private static void _URL(string input)
        {
            bool isMatch;
            string regex = @"^(?i)(http:\/\/| https:\/\/)?(www\.)[a-zA-Z0-9]*\.(\w){2,4}(\.(\w){2,4}){0,5}$";

            isMatch = Regex.IsMatch(input, regex);

            report(input, isMatch, "URL");
        }

        private static void _Password(string input) //TODO test this one a little more thoroughly
        {
            bool isMatch;
            bool has3LC = false;
            string regex = @"((?=.*\d)(?=.*[A-Z])(?=.*[a-z])(?=.*[+=*!^()|?<>,.{}`~_@#$%\-\\\/\[\]]).{10,})";

            isMatch = Regex.IsMatch(input, regex);

            if (isMatch)
            {
                has3LC = Regex.IsMatch(input, "[a-z]{3}");
                if (has3LC) { isMatch = false; }
            }

            report(input, isMatch, "PASSWORD");
        }

        private static void _IONWords(string input)
        {
            bool isMatch;
            string regex = @"[a-zA-Z]*(ion)";
            isMatch = Regex.IsMatch(input, regex);

            if (isMatch) { isMatch = (input.Length % 2 != 0); }

            report(input, isMatch, "\"ION\" WORD");
        } 

        private static void report(string input, bool valid, string type)
        {
            string result;

            result = buildReport(input, valid, type);
            WriteToLog(result);
            WriteToConsole(result);
        }

        private static void WriteToLog(string report)
        {
            using (StreamWriter w = File.AppendText("log.txt"))
            {
                w.WriteLine(report);
            }
        }

        private static void WriteToConsole(string report)
        {
            Console.WriteLine(report);
            Console.WriteLine();
        }

        private static string buildReport(string input, bool valid, string type)
        {
            string report = input;

            if (valid) { report += " IS VALID "; }
            else { report += " NOT VALID "; }

            report += type;

            return report;
        }

    }
}
