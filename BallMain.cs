/* 
Silly Code by AMELIA ROTONDO 
Last Edited: 11/05/2022
*/

using System;
//using System.Drawing;
using System.Windows.Forms;  //Needed for "Application" on next to last line of Main
public class BallMain
{  static void Main(string[] args)
   {
      //startup console message
      System.Console.WriteLine("Welcome to the Main method of the Amelia Rotondo Assignment 4 Ball-In-Motion UI!");

      //register the class as an object-
      RicochetBallInterface ball = new RicochetBallInterface();

      //run application
      Application.Run(ball);

      //shutdown console message
      System.Console.WriteLine("Main method will now shutdown.");

   }//End of Main

}//End of BallMain