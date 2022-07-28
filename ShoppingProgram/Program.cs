/* BEFORE LOOKING AT THE CODE
   xxx - name of shoop which we choose
   name1--> calling a xxxProductsName
   name2--> calling a xxxProductsPrice
   name3--> calling a xxxProductsKcal
   name4--> calling a xxxProductsGram
   I have chosen this way of naming, because i have a lots of variables called xxkcal, xxgrams etc. I wanted these to stand out.
*/
using System.Text.RegularExpressions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Globalization;

public class AllProducts
{
    //Main lists used in the program
    public List<string> listName = new List<string>();
    public List<string> listShop = new List<string>();
    public List<double> listPrice = new List<double>();
    public List<double> listKcal = new List<double>();
    public List<double> listGrams = new List<double>();

    //List of products which we add to our basket
    private List<string> basketProduct = new List<string>();
    private List<double> basketPrice = new List<double>();
    private List<int> basketPieces = new List<int>();
    private List<double> basketKcal = new List<double>();
    private List<double> basketGrams = new List<double>();

    //Support list used in the program
    private List<string> listMid = new List<string>();
    private List<double> listPriceInteger = new List<double>();
    private List<double> listPriceFloat = new List<double>();
    private List<string> listGramsHelp = new List<string>();

    //Pathway to .csv file
    public string pathway_download = "Products.csv";

    public void Adding_Values() // Loading all datas from .csv
    {
        Random rnd = new Random();
        using (var reader = new StreamReader(pathway_download))
        {
            while (!reader.EndOfStream)
            {
                var line = reader.ReadLine();
                var values = line.Split(';');
                listName.Add(values[0]);
                listGramsHelp.Add(values[1]);
                listShop.Add(values[2]);
                listMid.Add(values[3]);
            }

        }

        //In my .csv file i have a lot of kcals whos for example looks like this: "~246k". I decide to remove them and add a random kcal from 50 to 500(first if statment).
        //If kcal number doesn not has any unnecessary char, and looks for example like this: "34", I decided to put it in to kcalList, after conversion from double.
        foreach (var values in listMid.ToList())
        {
            int lowerRandomValue = 50;
            int upperRandomValue = 500;
            int counter1 = values.Count(c => Char.IsNumber(c));
            int counter2 = values.Count();

            if (counter1 != counter2)
            {
                listKcal.Add(rnd.Next(lowerRandomValue, upperRandomValue));
            }

            else
            {
                listKcal.Add(Convert.ToDouble(values));
            }
        }

        listMid.Clear(); // I do not want to occupy computer memory, and add new variable

        //In my .csv file i have a lot of position looks for example like this: "100g". I have to delete this 'g'. First counter shows me position where first char is. Second 
        //counter a number where white space is. If position of white space = position of first char or if we dont have a white space we have to delete from position of first char.
        //This is the situation where we has for example '100g'. In other options we will remove items from counter2 -> this situation '168 g (3 x 56g)'. This first if just protects me 
        //from -1 from counter to error.
        for (int i = 0; i < listGramsHelp.Count; i++)
        {
            int counter1 = listGramsHelp[i].Count(c => Char.IsNumber(c));
            int counter2 = listGramsHelp[i].IndexOf(' ');
            if (counter1 == counter2 || counter2 == -1)
            {
                string var1 = listGramsHelp[i].Remove(counter1);
                listMid.Add(var1);
            }
            else
            {
                string var2 = listGramsHelp[i].Remove(counter2);
                listMid.Add(var2);
            }
        }

        listMid.Sort(); //sorting list because first 10 positions still does not want to remove. 
        int counter = 0;
        int delete_numbers = 10;

        //Because first 10 position does not removed i am adding to grams position from index 10. In addition I am changing the puncttuation mark so that i could put the variabels
        //to double list.
        for (int i = delete_numbers; i < listMid.Count; i++)
        {
            counter = listMid[i].IndexOf(',');
            if (counter >= 1)
            {
                string rep = listMid[i].Replace(',', '.');
                listGrams.Add(Convert.ToDouble(rep));
            }
            else
            {
                listGrams.Add(Convert.ToDouble(listMid[i]));
            }
        }

        //because I have add -10 numbers I just want to rand the rest of numbers.
        for (int i = 0; i < delete_numbers; i++)
        {
            listGrams.Add(rnd.Next(30, 800));
        }

        //still some position does not want to remove, so i decide to replace it.
        for (int i = 0; i < listGrams.Count; i++)
        {
            if (listGrams[i] == 1)
            {
                listGrams[i] = rnd.Next(300, 900);
            }
        }
    }

    public void Drawing_Values() // In my csv file i do not have a prices of product, so I dedidec to draw it. 
    {
        Random rnd = new Random();

        for (int i = 0; i < listName.Count; i++)
        {
            listPriceInteger.Add(rnd.Next(3, 12));
            listPriceFloat.Add((float)(rnd.Next(0, 99)) / 100);
        }
        for (int i = 0; i < listPriceInteger.Count; i++)
        {
            listPrice.Add(Math.Round(((float)listPriceInteger[i] + listPriceFloat[i]), 2, MidpointRounding.ToEven));
        }
    }

    //Printing products, prices, pieces, kcal and grams of products. I made 2 methods for printing, because i do not want to write "Your bucket" before calling all methods.
    private void PrintingProducts(List<string> name1, List<double> name2, List<double> name3, List<double> name4)
    {
        int absolute = Math.Abs(name1.Count - name2.Count);
        for (int i = 0; i < name1.Count + absolute; i++)
        {
            Console.WriteLine(i + 1 + "." + "Product name: " + name1[i] + ", PRICE: " + name2[i] + ", KCAL: " + name3[i] + ", GRAMS: " + name4[i]);
        }
    }

    // printing products, prices, pieces, kcal and grams of products from our own basket
    private void PrintingBasket()
    {
        Console.WriteLine("YOUR BUCKET");
        for (int i = 0; i < basketProduct.Count; i++)
        {
            Console.WriteLine(i + 1 + ".PRODUCT: " + basketProduct[i] + "PRICE: " + basketPrice[i] + " PIECES: " + basketPieces[i] + " KCAL: " + basketKcal[i] + " GRAMS PER ONE PRODUCT:" + basketGrams[i]);
        }
    }

    private void AddingAndMergingBasket(List<string> name1, List<double> name2, List<double> name3, List<double> name4)
    {
        int absolute = Math.Abs(name1.Count - name2.Count); // protect from some error list, if loading datas will be different
        int loop = 1; // variable only for while loop
        int iterator = 0;
        string letter1 = "F"; // finish option
        string letter2 = "X"; // deleting items from backet option

        Console.WriteLine("What do you want to add to your shopping basket? ///FINISH - PRESS F " + "//DELETE POSITION - PRESS X");
        while (loop > 0)
        {
            int product_choose_int = 0;
            string product_choose_str = Console.ReadLine();
            Regex digitsOnly = new Regex(@"[^\d]"); // I want to have protect in situation if user will enter another char, for example 'k'.
            MatchCollection matches = digitsOnly.Matches(product_choose_str);

            // If user will enter 'F' the loop will finish.
            if (product_choose_str == letter1 || product_choose_str == letter1.ToLower())
            {
                PrintingBasket();
                Console.WriteLine("Finally price:" + basketPrice.Sum());
                break;
            }

            // If user will enter 'X' we will be moved to deleting process.
            else if (product_choose_str == letter2 || product_choose_str == letter2.ToLower())
            {
                DeletingProductsFromBasket(name1, name2, name3, name4);

            }

            // It work if user will enter another char. Not 'F' or 'X'.
            else if (matches.Count == 1)
            {
                Console.WriteLine("You picked a wrong letter. Please write a command again.");
            }

            // in this situation user just want to enter a new product.
            else
            {
                product_choose_int = Convert.ToInt32(product_choose_str);
            }

            // I do not make else, because in all other situation the loop will not finish. ???????
            if (product_choose_int >= 1)
            {
                for (int i = 0; i <= name1.Count + absolute; i++) // this absolute value is protection, if we would have some differences beetwen lists.
                {
                    if (i == product_choose_int)
                    {
                        // Special option if our basket is empty. Then we have to add some item. index = i-1, because I am printing values from 1,2,3. 
                        // But the list index start at 0.
                        if (basketProduct.Count < 1)
                        {
                            Console.WriteLine("How much pieces of these product do you want?");
                            string pieces_choose_str_5 = Console.ReadLine();
                            int pieces_choose_int_5 = Convert.ToInt32(pieces_choose_str_5);

                            basketProduct.Add(name1[i - 1]);
                            basketPrice.Add(name2[i - 1] * pieces_choose_int_5);
                            basketPieces.Add(pieces_choose_int_5);
                            basketKcal.Add(name3[i - 1] * pieces_choose_int_5);
                            basketGrams.Add(name4[i - 1]);
                        }

                        // There we are adding product, if we have already 1 product in the backet.
                        else if (basketProduct.Count >= 1)
                        {
                            for (int j = 0; j < basketProduct.Count; j++)
                            {
                                //There we have special option if product that we choosing is already in backet. Then we just add pieces and price to position that is already existing in backet.
                                //In other option we jus add another product to backet.
                                if (name1[i - 1] == basketProduct[j])
                                {
                                    Console.WriteLine("How much pieces of these product do you want?");
                                    string pieces_choose_str_1 = Console.ReadLine();
                                    int pieces_choose_int_1 = Convert.ToInt32(pieces_choose_str_1);
                                    basketPrice[j] = basketPrice[j] + (name2[i - 1] * pieces_choose_int_1);
                                    basketPieces[j] = basketPieces[j] + pieces_choose_int_1;
                                    basketKcal[j] = basketKcal[j] + (name3[i - 1] * pieces_choose_int_1);
                                    iterator = 0;
                                    break;
                                }

                                //This if is simple. If first if does not match thats the signal, that entering product is not in the backet. After all for loops iterator
                                //finlly is equal to basketProduct.Count - 1. This is the signal, that our choosing product is out of the backet, so program has to add him.
                                //I have to do basketProduct.Count - 1 because program has already one existing product from line number 240 if. 
                                if (iterator == basketProduct.Count - 1)
                                {
                                    Console.WriteLine("How much pieces of these product do you want?");
                                    string pieces_choose_str = Console.ReadLine();
                                    int pieces_choose_int = Convert.ToInt32(pieces_choose_str);
                                    basketProduct.Add(name1[i - 1]);
                                    basketPrice.Add(name2[i - 1] * pieces_choose_int);
                                    basketPieces.Add(pieces_choose_int);
                                    basketKcal.Add(name3[i - 1] * pieces_choose_int);
                                    basketGrams.Add(name4[i - 1]);
                                    iterator = 0;
                                    break;
                                }
                                else
                                {
                                    iterator++;
                                }
                            }
                        }
                    }
                }
            }
            PrintingBasket();
        }
    }

    private void DeletingProductsFromBasket(List<string> name1, List<double> name2, List<double> name3, List<double> name4) // deleting products, prices and pieces of products from our own basket
    {
        /*  
        Here we are asking user which position he want to delete, and how much pieces does he wants to delete. If he want to delete all pieces of product, program are removing 
        Product name, product price, and product pices from the basket. 
        */

        Console.WriteLine("Which position do yow want to delete?");
        string product_delete_str = Console.ReadLine();
        int product_delete_int = Convert.ToInt32(product_delete_str);

        Console.WriteLine("How much pisition do you want to delete?");
        string pieces_delete_str = Console.ReadLine();
        int pieces_delete_int = Convert.ToInt32(pieces_delete_str);
        int rest_pieces = 0; // number of items that will remain after deletion

        for (int i = 0; i < name1.Count; i++)
        {
            if (basketProduct[product_delete_int - 1] == name1[i]) // product_delete_int - 1 because indexes are starting from the 0 position. Program is printig first position as a 1.xxx
            {
                rest_pieces = basketPieces[product_delete_int - 1] - pieces_delete_int;

                // In this option If we have 3pieces of some product in the backet, and we will enter a number grater than that we have pieces in the backet,
                // the program will remove all items. For example: We have 3 pieces in the backet, we enter number = 4, so program will delete 3 positions.
                if (rest_pieces <= 0)
                {
                    basketProduct.Remove(basketProduct[product_delete_int - 1]);
                    basketPieces.Remove(basketPieces[product_delete_int - 1]);
                    basketPrice.Remove(basketPrice[product_delete_int - 1]);
                    basketKcal.Remove(basketKcal[product_delete_int - 1]);
                    basketGrams.Remove(basketGrams[product_delete_int - 1]);
                    break;
                }

                else
                {
                    basketPrice[product_delete_int - 1] = rest_pieces * name2[i];
                    basketPieces[product_delete_int - 1] = rest_pieces;
                    basketKcal[product_delete_int - 1] = rest_pieces * name3[i];
                    break;
                }
            }
        }
        //If our backet is empty, the program will give us a choose, what we want to do.
        if (basketProduct.Count == 0)
        {
            Console.WriteLine("Basket is empty. If you want to finish - press F. If you want to buyy something - choose new product.");
        }

    }


    private void AutomaticallyBacket(List<string> name1, List<double> name2, List<double> name3, List<double> name4)
    {
        //This function I made after resach about healthy products, dietes, etc. If you want to understend why I use categories or selected variables, go to README.txt
        //Its to long to write about it there. This would unnecessarily clutter the place :)
        List<string> autoProduct = new List<string>();
        List<double> autoPrice = new List<double>();
        List<int> autoPieces = new List<int>();
        List<double> autoKcal = new List<double>();
        List<double> autoGrams = new List<double>();

        double WholePrice = 0; // finnaly price
        double WholeKcal = 0; // finally kcal
        double grams_help = 100; // variables which hel us to count product kcal for 100g.

        //This dates i took from my excel file, after research about healthy products
        double kcalFew = 100;
        double kcalAverage = 157;
        double kcalDeviation = 222;
        int firstCategorySwitch = 500;
        int thirdCategorySwitch = 1000;
        int upperLimit = 700;
        int lowerLimit = 500;

        Console.WriteLine("For how much days Should I propose a products for you?");
        string dayselect_str = Console.ReadLine();
        int dayselect = Convert.ToInt32(dayselect_str);

        Console.WriteLine("How much calories do you want to to eat per day?(If you are not sure, count you BMI)");
        string bmiselect_str = Console.ReadLine();
        int bmiselect = Convert.ToInt32(bmiselect_str);

        //There we have variables from excel file. If you want all documentation, why i choose this variables, go to README.txt
        int firstCategoryHelp = (dayselect * bmiselect) / firstCategorySwitch;
        int firstCategoryNumber = (firstCategoryHelp * 100) + 100;
        int thirdCategoryNumber = (dayselect * bmiselect) / thirdCategorySwitch;

        double firstCategoryIteration = 0;
        int thirdCategoryIteration = 0;

        for (int i = 0; i < name1.Count; i++)
        {
            if ((grams_help * name3[i]) / name4[i] < kcalFew) // first i want to remove unwanted product, which is propably unhelthy :)
            {

                if (name3[i] < kcalFew && name4[i] < grams_help && name3[i] > 10)// I add name[i] > 10 , because i dont want to add water, tee etc products to my autoBasket.
                {
                    if (firstCategoryNumber > firstCategoryIteration) //First category of products
                    {
                        autoProduct.Add(name1[i]);
                        autoPrice.Add(name2[i]);
                        autoKcal.Add(name3[i]);
                        autoGrams.Add(name4[i]);
                        firstCategoryIteration += name3[i];
                        WholePrice += name2[i];
                        WholeKcal += name3[i];
                    }
                }


                if (name3[i] > lowerLimit && name3[i] < upperLimit)
                {
                    if (thirdCategoryNumber > thirdCategoryIteration) //Third category of product
                    {
                        autoProduct.Add(name1[i]);
                        autoPrice.Add(name2[i]);
                        autoKcal.Add(name3[i]);
                        autoGrams.Add(name4[i]);
                        thirdCategoryIteration++;
                        WholePrice += name2[i];
                        WholeKcal += name3[i];
                    }
                }
            }

        }

        for (int i = 0; i < name1.Count; i++)
        {
            if ((grams_help * name3[i]) / name4[i] < kcalFew)
            {
                if (name3[i] < kcalAverage + kcalDeviation && name3[i] > kcalFew) //Second category of product
                {
                    if (bmiselect * dayselect > WholeKcal)
                    {
                        autoProduct.Add(name1[i]);
                        autoPrice.Add(name2[i]);
                        autoKcal.Add(name3[i]);
                        autoGrams.Add(name4[i]);
                        WholePrice += name2[i];
                        WholeKcal += name3[i];
                    }
                }
            }
        }
        PrintingProducts(autoProduct, autoPrice, autoKcal, autoGrams);
        Console.WriteLine("Finally Cost: " + WholePrice + "Finally kcal: " + WholeKcal);

    }

    public void SwitchMode(List<string> name1, List<double> name2, List<double> name3, List<double> name4)
    {
        int loop = 1;
        while (loop > 0)
        {
            Console.WriteLine("If you want to add products by your self - press M. If we have to propose you some products - press A");
            string optionSelect = Console.ReadLine();

            if (optionSelect == "M" || optionSelect == "m")
            {
                PrintingProducts(name1, name2, name3, name4);
                AddingAndMergingBasket(name1, name2, name3, name4);
            }
            if (optionSelect == "A" || optionSelect == "a")
            {
                AutomaticallyBacket(name1, name2, name3, name4);
                AddingAndMergingBasket(name1, name2, name3, name4);
            }
            if (optionSelect == "f" || optionSelect == "F")
            {
                Console.WriteLine("Thanks for using our aplication");
                break;
            }

        }
    }

    //This method I made only for classes inheriting from AllProducts. This metod add products to list, because of the shop names.
    public void ShopChoose(List<string> name1, List<double> name2, List<double> name3, List<double> name4, string shop1, string shop2, string shop3, string shop4)
    {
        for (int i = 0; i < listName.Count; i++)
        {
            if ((listShop[i] == shop1) || (listShop[i] != shop2 && listShop[i] != shop3 && listShop[i] != shop4))
            {
                name1.Add(listName[i]);
                name2.Add(listPrice[i]);
                name3.Add(listKcal[i]);
                name4.Add(listGrams[i]);
            }
        }
    }
}

class Biedronka : AllProducts
{
    public List<string> BiedronkaProductsName = new List<string>();
    public List<double> BiedronkaProductsPrice = new List<double>();
    public List<double> BiedronkaProductsKcal = new List<double>();
    public List<double> BiedronkaProductsGrams = new List<double>();

    public void Adding_Biedronka_Values()
    {
        Adding_Values();
        Drawing_Values();
        ShopChoose(BiedronkaProductsName, BiedronkaProductsPrice, BiedronkaProductsKcal, BiedronkaProductsGrams, "biedronka", "auchan", "lidl", "dino");
        SwitchMode(BiedronkaProductsName, BiedronkaProductsPrice, BiedronkaProductsKcal, BiedronkaProductsGrams);
    }

}



class Lidl : AllProducts
{
    public List<string> LidlProductsName = new List<string>();
    public List<double> LidlProductsPrice = new List<double>();
    public List<double> LidlProductsKcal = new List<double>();
    public List<double> LidlProductsGrams = new List<double>();

    public void Adding_Lidl_Values()
    {
        Adding_Values();
        Drawing_Values();
        ShopChoose(LidlProductsName, LidlProductsPrice, LidlProductsKcal, LidlProductsGrams, "lidl", "auchan", "biedronka", "dino");
        SwitchMode(LidlProductsName, LidlProductsPrice, LidlProductsKcal, LidlProductsGrams);

    }
}

class Auchan : AllProducts
{
    public List<string> AuchanProductsName = new List<string>();
    public List<double> AuchanProductsPrice = new List<double>();
    public List<double> AuchanProductsKcal = new List<double>();
    public List<double> AuchanProductsGrams = new List<double>();

    public void Adding_Auchan_Values()
    {
        Adding_Values();
        Drawing_Values();
        ShopChoose(AuchanProductsName, AuchanProductsPrice, AuchanProductsKcal, AuchanProductsGrams, "auchan", "lidl", "biedronka", "dino");
        SwitchMode(AuchanProductsName, AuchanProductsPrice, AuchanProductsKcal, AuchanProductsGrams);
    }
}
class Dino : AllProducts
{
    public List<string> DinoProductsName = new List<string>();
    public List<double> DinoProductsPrice = new List<double>();
    public List<double> DinoProductsKcal = new List<double>();
    public List<double> DinoProductsGrams = new List<double>();

    public void Adding_Dino_Values()
    {
        Adding_Values();
        Drawing_Values();
        ShopChoose(DinoProductsName, DinoProductsPrice, DinoProductsKcal, DinoProductsGrams, "dino", "lidl", "biedronka", "auchan");
        SwitchMode(DinoProductsName, DinoProductsPrice, DinoProductsKcal, DinoProductsGrams);
    }
}

namespace ShoppingAplication
{
    internal class Program
    {
        static void ShopChoosing()
        {
            Console.WriteLine("In which store do you want to shop?" + System.Environment.NewLine);
            Console.WriteLine("1.Biedronka" + System.Environment.NewLine);
            Console.WriteLine("2.Lidl" + System.Environment.NewLine);
            Console.WriteLine("3.Auchan" + System.Environment.NewLine);
            Console.WriteLine("4.Dino" + System.Environment.NewLine);
            Console.WriteLine("Your choose is: ");
        }

        static int Switches()
        {
            string choose = Console.ReadLine();
            int value = Convert.ToInt32(choose);
            return value;
        }

        static void Main(string[] args)
        {
            Biedronka biedronka = new Biedronka();
            Lidl lidl = new Lidl();
            Auchan auchan = new Auchan();
            Dino dino = new Dino();

            ShopChoosing();
            int value = Switches();
            switch (value)
            {
                case 1:
                    biedronka.Adding_Biedronka_Values();
                    break;

                case 2:
                    lidl.Adding_Lidl_Values();
                    break;

                case 3:
                    auchan.Adding_Auchan_Values();
                    break;

                case 4:
                    dino.Adding_Dino_Values();
                    break;
            }
        }
    }
}