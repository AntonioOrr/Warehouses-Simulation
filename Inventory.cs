using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Warehouses
{
    static class Inventory
    {
    

        //Author: Antonio Orr
        [STAThread]
        static void Main()
        {
            const string Path = "Inventory.txt";
            //creates array of strings of text file separated by line
            string[] lines = System.IO.File.ReadAllLines(Path);
            //creates arrays of elements of every line separated by commas and spaces
            string[] string1 = lines[0].Split(new string[] { ", " }, StringSplitOptions.None); string[] string2 = lines[1].Split(new string[] { ", " }, StringSplitOptions.None);
            string[] string3 = lines[2].Split(new string[] { ", " }, StringSplitOptions.None); string[] string4 = lines[3].Split(new string[] { ", " }, StringSplitOptions.None);
            string[] string5 = lines[4].Split(new string[] { ", " }, StringSplitOptions.None); string[] string6 = lines[5].Split(new string[] { ", " }, StringSplitOptions.None);
            //creates six int arrays
            int[] Atlanta = convert(string1), Baltimore = convert(string2), Chicago = convert(string3),
                Denver = convert(string4), Ely = convert(string5), Fargo = convert(string6);
            //transactions is accessed
            const string Path2 = "Transactions.txt";
            //creates string array of transactions separated by line
            string[] tLines = System.IO.File.ReadAllLines(Path2);
            string[] day;
            int amount;
            int choice;
            int itemNum;
            int index;

            //beginning-of-day status of all warehouses
            Console.WriteLine("Atlanta: " + Atlanta[0] +" "+ Atlanta[1] + " " + Atlanta[2] + " " + Atlanta[3] + " " + Atlanta[4]);
            Console.WriteLine("Baltimore: " + Baltimore[0] + " " + Baltimore[1] + " " + Baltimore[2] + " " + Baltimore[3] + " " + Baltimore[4]);
            Console.WriteLine("Chicago: " + Chicago[0] + " " + Chicago[1] + " " + Chicago[2] + " " + Chicago[3] + " " + Chicago[4]);
            Console.WriteLine("Denver: " + Denver[0] + " " + Denver[1] + " " + Denver[2] + " " + Denver[3] + " " + Denver[4]);
            Console.WriteLine("Ely: " + Ely[0] + " " + Ely[1] + " " + Ely[2] + " " + Ely[3] + " " + Ely[4]);
            Console.WriteLine("Fargo: " + Fargo[0] + " " + Fargo[1] + " " + Fargo[2] + " " + Fargo[3] + " " + Fargo[4]);
            Console.WriteLine();

            //loops twenty times, equal to number of transactions from text file
            for (int i = 0; i < 20; i++)
            {
                //string array gets initialized and changes transactions for every loop
                day = transaction(tLines, i);
                Console.WriteLine("Transaction " + (i + 1) + " processing: " + day[0] + " " + day[1] + " " + day[2]);
                Console.WriteLine();
                //returns negative or positive value whether items are being taken out or added in
                amount = SellOrPurchase(day);
                //returns item number within transactions array
                itemNum = getItemNum(day, 1);
                //returns index depending on itemNum
                index = itemNumScanner(day, itemNum);
                //unique statements depending on selling or purchasing
                //note: code only updates one warehouse per transaction (alphabetical order)
                if (amount < 0)
                {
                    choice = sellPick(itemNum, Atlanta, Baltimore, Chicago, Denver, Ely, Fargo);
                    //choice determined by warehouse with largest supply
                    if (choice == 1)
                    {
                        Console.WriteLine("Subtracting inventory from Atlanta... Updated inventory: ");
                        Atlanta[index] = Atlanta[index] + amount;
                        Console.WriteLine("Atlanta: " + Atlanta[0] + " " + Atlanta[1] + " " + Atlanta[2] + " " + Atlanta[3] + " " + Atlanta[4]);
                    }
                    else if (choice == 2)
                    {
                        Console.WriteLine("Subtracting inventory from Baltimore... Updated inventory: ");
                        Baltimore[index] = Baltimore[index] + amount;
                        Console.WriteLine("Baltimore: " + Baltimore[0] + " " + Baltimore[1] + " " + Baltimore[2] + " " + Baltimore[3] + " " + Baltimore[4]);
                    }
                    else if (choice == 3)
                    {
                        Console.WriteLine("Subtracting inventory from Chicago... Updated inventory: ");
                        Chicago[index] = Chicago[index] + amount;
                        Console.WriteLine("Chicago: " + Chicago[0] + " " + Chicago[1] + " " + Chicago[2] + " " + Chicago[3] + " " + Chicago[4]);
                    }
                    else if (choice == 4)
                    {
                        Console.WriteLine("Subtracting inventory from Denver... Updated inventory: ");
                        Denver[index] = Denver[index] + amount;
                        Console.WriteLine("Denver: " + Denver[0] + " " + Denver[1] + " " + Denver[2] + " " + Denver[3] + " " + Denver[4]);
                    }
                    else if (choice == 5)
                    {
                        Console.WriteLine("Subtracting inventory from Ely... Updated inventory: ");
                        Ely[index] = Ely[index] + amount;
                        Console.WriteLine("Ely: " + Ely[0] + " " + Ely[1] + " " + Ely[2] + " " + Ely[3] + " " + Ely[4]);
                    }
                    else if (choice == 6)
                    {
                        Console.WriteLine("Subtracting inventory from Fargo... Updated inventory: ");
                        Fargo[index] = Fargo[index] + amount;
                        Console.WriteLine("Ely: " + Ely[0] + " " + Ely[1] + " " + Ely[2] + " " + Ely[3] + " " + Ely[4]);
                    }
                }
                else
                {
                    choice = purchasePick(itemNum, Atlanta, Baltimore, Chicago, Denver, Ely, Fargo);
                    //choice determined by warehouse with smallest supply
                    if (choice == 1)
                    {
                        Console.WriteLine("Adding inventory to Atlanta... Updated inventory: ");
                        Atlanta[index] = Atlanta[index] + amount;
                        Console.WriteLine("Atlanta: " + Atlanta[0] + " " + Atlanta[1] + " " + Atlanta[2] + " " + Atlanta[3] + " " + Atlanta[4]);
                    }
                    else if (choice == 2)
                    {
                        Console.WriteLine("Added inventory to Baltimore... Updated inventory: ");
                        Baltimore[index] = Baltimore[index] + amount;
                        Console.WriteLine("Baltimore: " + Baltimore[0] + " " + Baltimore[1] + " " + Baltimore[2] + " " + Baltimore[3] + " " + Baltimore[4]);
                    }
                    else if (choice == 3)
                    {
                        Console.WriteLine("Added inventory to Chicago... Updated inventory: ");
                        Chicago[index] = Chicago[index] + amount;
                        Console.WriteLine("Chicago: " + Chicago[0] + " " + Chicago[1] + " " + Chicago[2] + " " + Chicago[3] + " " + Chicago[4]);
                    }
                    else if (choice == 4)
                    {
                        Console.WriteLine("Adding inventory to Denver... Updated inventory: ");
                        Denver[index] = Denver[index] + amount;
                        Console.WriteLine("Denver: " + Denver[0] + " " + Denver[1] + " " + Denver[2] + " " + Denver[3] + " " + Denver[4]);
                    }
                    else if (choice == 5)
                    {
                        Console.WriteLine("Adding inventory to Ely... Updated inventory: ");
                        Ely[index] = Ely[index] + amount;
                        Console.WriteLine("Ely: " + Ely[0] + " " + Ely[1] + " " + Ely[2] + " " + Ely[3] + " " + Ely[4]);
                    }
                    else if (choice == 6)
                    {
                        Console.WriteLine("Adding inventory to Fargo... Updated inventory: ");
                        Fargo[index] = Fargo[index] + amount;
                        Console.WriteLine("Ely: " + Ely[0] + " " + Ely[1] + " " + Ely[2] + " " + Ely[3] + " " + Ely[4]);
                    }
                  
                }
                Console.WriteLine();
            }
            //end-of-day status of all warehouses
            Console.WriteLine("The day has ended! End-of-day status of all warehouses: ");
            Console.WriteLine("Atlanta: " + Atlanta[0] + " " + Atlanta[1] + " " + Atlanta[2] + " " + Atlanta[3] + " " + Atlanta[4]);
            Console.WriteLine("Baltimore: " + Baltimore[0] + " " + Baltimore[1] + " " + Baltimore[2] + " " + Baltimore[3] + " " + Baltimore[4]);
            Console.WriteLine("Chicago: " + Chicago[0] + " " + Chicago[1] + " " + Chicago[2] + " " + Chicago[3] + " " + Chicago[4]);
            Console.WriteLine("Denver: " + Denver[0] + " " + Denver[1] + " " + Denver[2] + " " + Denver[3] + " " + Denver[4]);
            Console.WriteLine("Ely: " + Ely[0] + " " + Ely[1] + " " + Ely[2] + " " + Ely[3] + " " + Ely[4]);
            Console.WriteLine("Fargo: " + Fargo[0] + " " + Fargo[1] + " " + Fargo[2] + " " + Fargo[3] + " " + Fargo[4]);
            

        }

        //converts string arrays to int arrays
        private static int[] convert(string[] fields)
        {
            int[] conversion = fields.Select(n => Convert.ToInt32(n)).ToArray();
            return conversion;
        }
        //returns transaction string array based on index (line)
        private static string[] transaction(string[] file, int index)
        {
            string[] transaction = file[index].Split(new string[] { ", " }, StringSplitOptions.None);
            return transaction;
        }
        //returns item number in a transaction array
        private static int getItemNum(string[] day, int numLoc)
        {
            int num = Int32.Parse(day[numLoc]);
            return num;
        }
        //returns integer depending on item number
        private static int itemNumScanner(string[] day, int itemNum)
        {
            if (itemNum == 102)
                return 0;
            else if (itemNum == 215)
                return 1;
            else if (itemNum == 410)
                return 2;
            else if (itemNum == 525)
                return 3;
            else if (itemNum == 711)
                return 4;
            else
                return 5;

        }
        //returns positive or negative int amount
        private static int SellOrPurchase(string[] day)
        {
            int amount;
            if (day[0] == "S")
            {
                amount = 0 - Int32.Parse(day[2]);
            }
            else if (day[0] == "P")
            {
                amount = Int32.Parse(day[2]);
            }
            else
                amount = 0;
            return amount;
        }
        //returns an int declaring which capital gets inventory subtracted
        private static int sellPick(int itemNum,int[] cap1, int[] cap2, int[] cap3, int[] cap4, int[] cap5, int[] cap6)
        {
            int choice;
            if (itemNum == 102)
            {
                int q1 = cap1[0], q2 = cap2[0], q3 = cap3[0], q4 = cap4[0], q5 = cap5[0], q6 = cap6[0];
                var a = new List<int>() { q1, q2, q3, q4, q5, q6 };
                int max = a.Max();
                if (max == q1)
                    choice = 1;
                else if (max == q2)
                    choice = 2;
                else if (max == q3)
                    choice = 3;
                else if (max == q4)
                    choice = 4;
                else if (max == q5)
                    choice = 5;
                else
                    choice = 6;
            }
            else if (itemNum == 215)
            {
                int q1 = cap1[1], q2 = cap2[1], q3 = cap3[1], q4 = cap4[1], q5 = cap5[1], q6 = cap6[1];
                var a = new List<int>() { q1, q2, q3, q4, q5, q6 };
                int max = a.Max();
                if (max == q1)
                    choice = 1;
                else if (max == q2)
                    choice = 2;
                else if (max == q3)
                    choice = 3;
                else if (max == q4)
                    choice = 4;
                else if (max == q5)
                    choice = 5;
                else
                    choice = 6;
            }
            else if (itemNum == 410)
            {
                int q1 = cap1[2], q2 = cap2[2], q3 = cap3[2], q4 = cap4[2], q5 = cap5[2], q6 = cap6[2];
                var a = new List<int>() { q1, q2, q3, q4, q5, q6 };
                int max = a.Max();
                if (max == q1)
                    choice = 1;
                else if (max == q2)
                    choice = 2;
                else if (max == q3)
                    choice = 3;
                else if (max == q4)
                    choice = 4;
                else if (max == q5)
                    choice = 5;
                else
                    choice = 6;
            }
            else if (itemNum == 525)
            {
                int q1 = cap1[3], q2 = cap2[3], q3 = cap3[3], q4 = cap4[3], q5 = cap5[3], q6 = cap6[3];
                var a = new List<int>() { q1, q2, q3, q4, q5, q6 };
                int max = a.Max();
                if (max == q1)
                    choice = 1;
                else if (max == q2)
                    choice = 2;
                else if (max == q3)
                    choice = 3;
                else if (max == q4)
                    choice = 4;
                else if (max == q5)
                    choice = 5;
                else
                    choice = 6;
            }
            else if (itemNum == 711)
            {
                int q1 = cap1[4], q2 = cap2[4], q3 = cap3[4], q4 = cap4[4], q5 = cap5[4], q6 = cap6[4];
                var a = new List<int>() { q1, q2, q3, q4, q5, q6 };
                int max = a.Max();
                if (max == q1)
                    choice = 1;
                else if (max == q2)
                    choice = 2;
                else if (max == q3)
                    choice = 3;
                else if (max == q4)
                    choice = 4;
                else if (max == q5)
                    choice = 5;
                else
                    choice = 6;
            }
            else
                choice = 0;
            return choice;
        }
        //returns an int declaring which capital gets inventory added
        private static int purchasePick(int itemNum, int[] cap1, int[] cap2, int[] cap3, int[] cap4, int[] cap5, int[] cap6)
        {
            int choice;
            if (itemNum == 102)
            {
                int q1 = cap1[0], q2 = cap2[0], q3 = cap3[0], q4 = cap4[0], q5 = cap5[0], q6 = cap6[0];
                var a = new List<int>() { q1, q2, q3, q4, q5, q6 };
                int min = a.Min();
                if (min == q1)
                    choice = 1;
                else if (min == q2)
                    choice = 2;
                else if (min == q3)
                    choice = 3;
                else if (min == q4)
                    choice = 4;
                else if (min == q5)
                    choice = 5;
                else
                    choice = 6;
            }
            else if (itemNum == 215)
            {
                int q1 = cap1[1], q2 = cap2[1], q3 = cap3[1], q4 = cap4[1], q5 = cap5[1], q6 = cap6[1];
                var a = new List<int>() { q1, q2, q3, q4, q5, q6 };
                int min = a.Min();
                if (min == q1)
                    choice = 1;
                else if (min == q2)
                    choice = 2;
                else if (min == q3)
                    choice = 3;
                else if (min == q4)
                    choice = 4;
                else if (min == q5)
                    choice = 5;
                else
                    choice = 6;
            }
            else if (itemNum == 410)
            {
                int q1 = cap1[2], q2 = cap2[2], q3 = cap3[2], q4 = cap4[2], q5 = cap5[2], q6 = cap6[2];
                var a = new List<int>() { q1, q2, q3, q4, q5, q6 };
                int min = a.Min();
                if (min == q1)
                    choice = 1;
                else if (min == q2)
                    choice = 2;
                else if (min == q3)
                    choice = 3;
                else if (min == q4)
                    choice = 4;
                else if (min == q5)
                    choice = 5;
                else
                    choice = 6;
            }
            else if (itemNum == 525)
            {
                int q1 = cap1[3], q2 = cap2[3], q3 = cap3[3], q4 = cap4[3], q5 = cap5[3], q6 = cap6[3];
                var a = new List<int>() { q1, q2, q3, q4, q5, q6 };
                int min = a.Min();
                if (min == q1)
                    choice = 1;
                else if (min == q2)
                    choice = 2;
                else if (min == q3)
                    choice = 3;
                else if (min == q4)
                    choice = 4;
                else if (min == q5)
                    choice = 5;
                else
                    choice = 6;
            }
            else if (itemNum == 711)
            {
                int q1 = cap1[4], q2 = cap2[4], q3 = cap3[4], q4 = cap4[4], q5 = cap5[4], q6 = cap6[4];
                var a = new List<int>() { q1, q2, q3, q4, q5, q6 };
                int min = a.Min();
                if (min == q1)
                    choice = 1;
                else if (min == q2)
                    choice = 2;
                else if (min == q3)
                    choice = 3;
                else if (min == q4)
                    choice = 4;
                else if (min == q5)
                    choice = 5;
                else
                    choice = 6;
            }
            else
                choice = 0;
            return choice;
        }
    }
}

