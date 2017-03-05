/*******************************************
 *                                         *
 * NPower Software Development and Testing *
 *            Week 1 Project               *
 *        The Loopy Planet Challenge       *
 *            Language: C#                 *
 *            February 7, 2017             *
 *            Dillon Dommer                *
 *                                         *
 *******************************************/

/* This program prints a list of the planets in the solar system,
 * (excluding Earth), and prompts the user to select a number
 *  that corresponds to a planet via a console-based menu.
 * 
 * It then asks a user to input their weight in pounds, and prints out their
 * weight on the planet that they have chosen.
 * 
 * The program will then repeat the process until 
 * the user enters a number which corresponds to <quit>, which will
 * cause the process to terminate. 
 */

using System;

namespace DillonDommerLoopyPlanet
{
    class LoopyPlanet
    {
        public static int menu_num;      // Menu number to be input
        public static double weight;     // Weight (lbs) to be input

        public static bool exit_cond;    // used to exit prompt loops on valid input, this is used for both loops
        public static bool bigexit_cond; // used to determine when to exit the "game loop"

        public static string[] planets = {"Jupiter", "Mars", "Mercury", "Neptune",
                "Pluto", "Saturn", "Uranus", "Venus"};

        public static double[] weights = { 2.64, 0.38, 0.37, 1.12, 0.04, 1.15, 1.15, 0.88 };

        /* Contract: printGravity : void -> void
         * 
         * Purpose: to calculate and print the gravity (relative to Earth) of the array of planets
         * 
         * Example: printGravity() should produce "The gravity on Mars is 38% of the gravity on Earth."
         * for each planet                          
         */
        public static void printGravity()
        {
            for (int i = 0; i < planets.Length; i++)
            {
                Console.WriteLine("\nThe gravity on {0} is {1}% of the gravity on Earth.", planets[i], 100 * weights[i]);
            }
        }

        /* Contract: printMenu : void -> void
         * 
         * Purpose: to print a text menu of the possible menu selections available to the user
         * 
         * Example: printMenu() should produce "Menu of Planets" \n "1. Jupiter 2. Mars 3. Mercury"
         * et cetera                                                                                    
         */
        public static void printMenu()
        {
            Console.WriteLine("           Menu of Planets          ");
            Console.WriteLine("           ==== == =======          ");
            Console.WriteLine("1. Jupiter    2. Mars     3. Mercury");
            Console.WriteLine("4. Neptune    5. Pluto    6. Saturn ");
            Console.WriteLine("7. Uranus     8. Venus    9. <Quit> ");
            Console.WriteLine("10. List the planet gravity factors ");
        }

        /* Contract: compute_weight : double double -> double
         * 
         * Purpose: to calculate an extraterrestrial weight using both 
         * an Earth weight which is 'lbs', and an extraterrestrial 'factor'
         *  
         * Example: compute_weight(100.6, 0.45) should produce 45.2                                                                                          
         */
        public static double compute_weight(double lbs, double factor)
        {
            double lbs_format = lbs * factor;
            return double.Parse(lbs_format.ToString("0.0"));
        }

        /* Contract: handleMenu : void -> void
         * 
         * Purpose: to run the menu loop, which prompts the user for input
         *  
         * Example: handleMenu() should produce "Enter your menu choice: "                                                                                          
         */
        public static void handleMenu()
        {
            do
            {
                try
                {
                    Console.Write("\nEnter your menu choice: ");
                    menu_num = int.Parse(Console.ReadLine());

                    if (menu_num == 10)         // bonus selection: print the list of gravity
                    {                           // of extraterrestrial planets
                        printGravity();
                        Console.WriteLine();
                        printMenu();
                        continue;
                    }
                    // if the input doesn't correspond to a menu selection, we deal with it via an exception
                    else if (menu_num < 1 || menu_num > 9) throw new ArgumentOutOfRangeException { };

                    else if (menu_num == 9)
                    {
                        Console.WriteLine("\nHave a great day! Goodbye!");
                        exit_cond = true;
                        bigexit_cond = true;
                    }

                    else
                    {
                        exit_cond = true;              // we have a valid input, don't ask for another
                    }
                }

                catch (FormatException)                // input is NaN
                {
                    Console.WriteLine("\nError! Please enter a a valid number.");
                }

                catch (ArgumentOutOfRangeException)    // input is not on the menu
                {
                    Console.WriteLine("\nError! Please enter a number between 1 and 9, inclusive.");
                }

                catch (OverflowException)              // number is too big or small!
                {
                    Console.WriteLine("\nError! Number is out of int value range."); 
                }

            } while (!exit_cond);
        }

        /* Contract: handleWeight : void -> void
         * 
         * Purpose: to run the loop to get a weight, which prompts the user for input
         *  
         * Example: handleWeight() should produce "Enter your weight on Earth: "                                                                                          
         */
        public static void handleWeight()
        {
            exit_cond = false;

            if (!bigexit_cond)             // if the user wants to exit, this loop will not run
            {
                do
                {
                    try
                    {
                        Console.Write("\nEnter your weight on Earth: ");
                        weight = double.Parse(Console.ReadLine());

                        if (weight < 0) throw new ArgumentOutOfRangeException { }; // handles a negative weight

                        else
                        {
                            exit_cond = true;                 // we have a valid input, don't ask for another
                        }
                    }

                    catch (FormatException)              // input is NaN
                    {
                        Console.WriteLine("\nError! Please enter a a valid number.");
                    }

                    catch (ArgumentOutOfRangeException)  // input is negative
                    {
                        Console.WriteLine("\nError! Please enter a non-negative weight.");
                    }

                    catch (OverflowException)            // input is too big (unlikely to ever be thrown)
                    {
                        Console.WriteLine("\nError! Number is out of double value range.");
                    }

                } while (!exit_cond);

                exit_cond = false;                     // this allows handleMenu to run again

                Console.WriteLine();
                Console.WriteLine("\nYour weight of {0} pounds on Earth would be {1} pounds on {2}.",
                    double.Parse(weight.ToString("0.0")),
                    compute_weight(weight, weights[menu_num - 1]), planets[menu_num - 1]);         // print the result
                Console.WriteLine();
            }
        }
        static void Main(string[] args)
        {
            while (!bigexit_cond)         // "game loop", can be terminated in the printMenu() method
            {
                printMenu();              // print the menu 
                handleMenu();             // handle first input
                handleWeight();           // handle second input
            }
            Console.ReadLine();           // keeps the console open after the program terminates in order to read goodbye message
        }
    }
}