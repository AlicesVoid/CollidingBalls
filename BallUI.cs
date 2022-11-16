/* 
Silly Code by AMELIA ROTONDO 
Last Edited: 11/15/2022
*/

using System;
using System.Drawing;
using System.Windows.Forms;
using System.Timers;


public class CollidingBallsInterface: Form
{

      /* UPDATES A4 -> A5: 
            SET THE SPEED -> BALL 1 SPEED
                  SPEED INPUT -> BALL 1 SPEED INPUT
            SET THE ANGLE -> BALL 2 SPEED
                  ANGLE INPUT -> BALL 2 SPEED INPUT
            X =           -> 1st Ball C.Coords
                  X_OUT   -> 1st Ball C.Coords Out
            Y =           -> 2nd Ball C.Coords
                  Y_OUT   -> 2nd Ball C.Coords Out
            COMBINE START && INIT 
            BALL COORDS -> DELETE
            BALLS START OUT ON THE SCREEN IN 2 SET LOCATIONS

            UX UPDATES:
            Add Another Delta-X and Another Delta-Y
            Add Another Set of C.Cord Variables
            Add Another BALL MOVEMENTS for a Second Speed Variation
            ADD COLLISION DETECTION
                  SEE IF YOU CAN ADD DIRECTION CHANGE? 
      */
      
      // USER INTERFACE INITIALIZATION
      private Label title       = new Label();
      private Label first_speed_title = new Label();
      private Label second_speed_title   = new Label();
      private Label first_location_title     = new Label();
      private Label second_location_title     = new Label();
      private Label first_location_output    = new Label();
      private Label second_location_output    = new Label();
      private Panel header            = new Panel();
      private Panel controlz          = new Panel();
      private Graphicpanel ball_mover = new Graphicpanel();
      private Button init_button  = new Button();
      private Button start_button = new Button();
      private Button quit_button  = new Button();
      private TextBox first_speed_input = new TextBox();
      private TextBox second_speed_input   = new TextBox();

      //UI STYLE VARIABLES
      private Size program_size   = new Size(1000, 1200);
      private Size button_size    = new Size(150, 100);
      private Font control_font      = new Font("Comic Sans MS", 18, FontStyle.Regular);
      private Font title_font        = new Font("Impact", 25, FontStyle.Bold);

      //UX VARIABLES 
      private static double FIRST_MOVEMENTS = 30;
      private static double delta_x1   = 0;
      private static double delta_y1   = 0; 
      private static double x1_coord   = 290; 
      private static double y1_coord   = 390; 

      /* New Variables: NOT YET IMPLEMENTED:

      private static double SECOND_MOVEMENTS = 30;
      private static double delta_x2   = 0;
      private static double delta_y2   = 0; 
      private static double x2_coord   = 590; 
      private static double y2_coord   = 30; 
      private static double dir_angle = 0;

      */

      private static double dir_angle = 0;

      //Runtime State 
      private enum State {Init, Line, Pause};
      private static State runtime = State.Init;
      
      //Execution State
      private enum Execution_state {Executing, Waiting_to_terminate};           
      private Execution_state current_state = Execution_state.Executing;

      //Clock Systems
      private static System.Timers.Timer exit_clock = new System.Timers.Timer();  
      private static System.Timers.Timer ball_clock = new System.Timers.Timer();  
      private const double clock_time = 3.0; //Hz
      private const double one_second = 1000.0; //ms
      private const double ball_interval = one_second / clock_time;


      //CONSTRUCTOR
      public CollidingBallsInterface() 
      {
            //DECLARE SIZES
            MaximumSize = program_size;
            MinimumSize = program_size;

            //INIT. TEXT
            title.Text        = "Colliding Balls by Amelia Rotondo";
            first_speed_title.Text  = "RED Ball Speed Input:";
            second_speed_title.Text    = "BLUE Ball Speed Input:";
            first_location_title.Text      = "RED Ball Location:";
            second_location_title.Text      = "BLUE Ball Location:";
            init_button.Text  = "Init.";
            start_button.Text = "Start!";
            quit_button.Text  = "Quit...";
                                    
            //INIT. SIZES
            Size              = MaximumSize;
            title.Size        = new Size(800, 90);
            header.Size       = new Size(1000, 200);
            ball_mover.Size   = new Size(1000, 700);
            controlz.Size     = new Size(1000, 300);
            first_speed_title.Size  = new Size(200, 60);
            second_speed_title.Size    = new Size(200, 60);
            first_location_title.Size      = new Size(150, 70);
            second_location_title.Size      = new Size(150, 70);
            init_button.Size  = button_size;
            start_button.Size = button_size;
            quit_button.Size  = button_size;
            first_speed_input.Size  = new Size(120, 80);
            second_speed_input.Size    = new Size(120, 80);
            first_location_output.Size     = new Size(120, 90);
            second_location_output.Size     = new Size(120, 90);
            
            //INIT. COLORS
            header.BackColor       = Color.LightSeaGreen;
            ball_mover.BackColor   = Color.LightCyan;
            controlz.BackColor     = Color.LightGoldenrodYellow;
            first_speed_title.BackColor  = Color.Pink;
            second_speed_title.BackColor    = Color.Pink;
            first_location_title.BackColor      = Color.Lime;
            second_location_title.BackColor      = Color.Lime;
            first_speed_input.BackColor  = Color.LightPink;
            second_speed_input.BackColor    = Color.LightPink;
            first_location_output.BackColor     = Color.LightGreen;
            second_location_output.BackColor     = Color.LightGreen; 
            init_button.BackColor  = Color.LightPink;
            start_button.BackColor = Color.LightPink;
            quit_button.BackColor  = Color.LightPink;

            //INIT. FONTS
            title.Font        = title_font;
            first_speed_title.Font  = control_font;
            second_speed_title.Font    = control_font; 
            first_location_title.Font      = control_font;
            second_location_title.Font      = control_font;
            init_button.Font  = control_font;
            start_button.Font = control_font;
            quit_button.Font  = control_font;
            first_speed_input.Font  = control_font;
            second_speed_input.Font    = control_font;
            first_location_output.Font     = control_font;
            second_location_output.Font     = control_font;

            //INIT. ALIGNMENTS
            title.TextAlign        = ContentAlignment.MiddleCenter;
            first_speed_title.TextAlign  = ContentAlignment.MiddleCenter;
            second_speed_title.TextAlign    = ContentAlignment.MiddleCenter;
            first_location_title.TextAlign      = ContentAlignment.MiddleCenter;
            second_location_title.TextAlign      = ContentAlignment.MiddleCenter;
            init_button.TextAlign  = ContentAlignment.MiddleCenter;
            start_button.TextAlign = ContentAlignment.MiddleCenter;
            quit_button.TextAlign  = ContentAlignment.MiddleCenter;

            //INIT. LOCATIONS
            header.Location     = new Point(0, 0);
            ball_mover.Location = new Point(0, 200);
            controlz.Location   = new Point(0, 900);

            title.Location       = new Point(125, 40);
            first_speed_title.Location = new Point(200, 50);
            second_speed_title.Location   = new Point(550, 50);
            first_location_title.Location     = new Point(250, 150);
            second_location_title.Location     = new Point(600, 150);
            first_speed_input.Location = new Point(410, 50);
            second_speed_input.Location   = new Point(760, 50);
            first_location_output.Location    = new Point(410, 150);
            second_location_output.Location    = new Point(760, 150);
            start_button.Location = new Point(30, 10);
            quit_button.Location = new Point(40, 150);

            //INIT. CONTROLS
            Controls.Add(header);
            Controls.Add(ball_mover);
            Controls.Add(controlz);
            header.Controls.Add(title);
                      
            //INIT. CONTROLZ
            controlz.Controls.Add(first_speed_title);
            controlz.Controls.Add(second_speed_title);
            controlz.Controls.Add(first_location_title);
            controlz.Controls.Add(second_location_title);
            controlz.Controls.Add(first_speed_input);
            controlz.Controls.Add(second_speed_input);
            controlz.Controls.Add(first_location_output);
            controlz.Controls.Add(second_location_output);
            controlz.Controls.Add(start_button);
            controlz.Controls.Add(quit_button);  

            //INIT. EVENT HANDLERS
            init_button.Click  += new EventHandler(resetRun);
            start_button.Click += new EventHandler(startRun);
            quit_button.Click  += new EventHandler(stopRun);
            
            //INIT. CLOCK CONFIG
            exit_clock.Enabled = false;     //Clock is turned off at start program execution.
            exit_clock.Interval = 2500;               //2.5 Seconds
            exit_clock.Elapsed += new ElapsedEventHandler(shutdown);   //Attach a method to the clock.

            ball_clock.Enabled = false;     //Clock is turned off at start program execution.
            ball_clock.Interval = ball_interval;      // 1/3rd Second 
            ball_clock.Elapsed += new ElapsedEventHandler(ballHelper);   //Attach a method to the clock.

            //Open this user interface window in the center of the display.
            CenterToScreen();

      }//End of constructor CollidingBallsInterface
      
      //Helper Method to Reset the Run
      protected void resetRun(Object sender, EventArgs events)
      {
            //Clear Text
            first_speed_input.Text = " ";
            second_speed_input.Text   = " ";
            first_location_output.Text    = " ";
            second_location_output.Text    = " ";

            start_button.Text = "Start!";
            quit_button.Text  = "Quit...";

            //Reset Variable
            FIRST_MOVEMENTS = 0;
            x1_coord        = 490;
            y1_coord        = 340;
            delta_x1        = 0;
            delta_y1        = 0;
            dir_angle      = 0;

            ball_clock.Enabled = false;
            runtime = State.Init;
            
            //Reset Graphic Panel
            ball_mover.Invalidate();
            ball_mover.Refresh();
      }

      //Helper Method to Exit and LEAVE the Program (waits 2.5 seconds before closing)
      protected void stopRun(Object sender, EventArgs events)
      {
            switch(current_state)
            {
                  case Execution_state.Executing:
                              exit_clock.Interval = 2500;     //2500ms = 2.5 seconds
                              exit_clock.Enabled  = true;
                              quit_button.Text = "Cancel!?";
                              current_state = Execution_state.Waiting_to_terminate;
                              break;
                  case Execution_state.Waiting_to_terminate:
                              exit_clock.Enabled = false;
                              quit_button.Text   = "Quit...";
                              current_state = Execution_state.Executing;
                              break;
            }//End of switch statement

      }//End of method stopRun.  
      
      //Helper Method to Initialize the Program
      protected void startRun(Object sender, EventArgs events)
      {
            switch(runtime)
            {
                  case State.Line:
                        //change text
                        start_button.Text = "Resume!";
                        runtime = State.Pause; 

                        ball_clock.Enabled = false;
                        break;

                  default:
                        //change text
                        if(runtime == State.Init && assertValues()) 
                        {
                              dir_angle      = Convert.ToDouble(second_speed_input.Text);
                              FIRST_MOVEMENTS = 1000 * Convert.ToDouble(first_speed_input.Text);
                        }

                        start_button.Text= "Pause?";

                        //calculate the delta_x1 and delta_y1
                        delta_x1 = (FIRST_MOVEMENTS / ball_interval) * Math.Sin(dir_angle);
                        delta_y1 = (FIRST_MOVEMENTS / ball_interval) * Math.Cos(dir_angle);
                        runtime = State.Line;

                        ball_clock.Enabled = true;

                        if(runtime == State.Init) {ball_mover.Refresh(); }

                        break;

            }
      }//End of startRun
        
      //Method to Move the Ball Around
      protected void ballHelper(Object sender, EventArgs events)
      {   
            if(collisionCheck())
            {

            }
            else  
            {
                  x1_coord += delta_x1;
                  y1_coord += delta_y1;
            }

            first_location_output.Text = Convert.ToString((Convert.ToInt32(x1_coord) + 10));
            second_location_output.Text = Convert.ToString((Convert.ToInt32(y1_coord) + 10));

            ball_mover.Refresh(); 
      }

       //Method to Shut Down the System
      protected void shutdown(System.Object sender, EventArgs even)                   //<== Revised for version 2.2
      {
            Close();       //That means close the main user interface window.
      }//End of method shutdown

      //Helper Method to Process Collisions
      protected bool collisionCheck()
      {
            if(x1_coord + delta_x1 < 0 || x1_coord + delta_x1 > 980) 
                  {    
                        
                        if(x1_coord < 500) 
                              {x1_coord = 0; y1_coord = y1_coord + (delta_y1 * (x1_coord / delta_x1));}
                        else 
                              {x1_coord = 980; y1_coord = y1_coord + (delta_y1 * ((980 - x1_coord) / delta_x1));}
                        delta_x1 = delta_x1 * -1;
                        return true;
                  }

            if(y1_coord + delta_y1 < 0 || y1_coord + delta_y1 > 680) 
                  {
                        
                        if(y1_coord < 350) 
                              {y1_coord = 0; x1_coord = x1_coord + (delta_x1 * (y1_coord / delta_y1));}
                        else 
                              {y1_coord = 680; x1_coord = x1_coord + (delta_x1 * (680 - y1_coord / delta_y1));}
                        
                        delta_y1 = delta_y1 * -1;
                        return true;
                  }
            return false;
      }
      
      //Helper Method to Assert Inputs
      protected bool assertValues()
      {
            return true;
      }

      // Method to show a whole bunch of tiny funny red dots in the shape of an Exit Sign
      public class Graphicpanel: Panel
      {private Brush paint_brush = new SolidBrush(System.Drawing.Color.Red);
      public Graphicpanel() 
            {Console.WriteLine("A graphic enabled panel was created");}  //Constructor writes to terminal

      //Draws the Arrow
      protected override void OnPaint(PaintEventArgs ee)
      {  
            Graphics graph = ee.Graphics;

            switch(runtime)
            {
                  case State.Init: 
                        Console.WriteLine("Nothing Is Being Drawn");
                        break;

                  default:
                        Console.WriteLine("A Ball Is Moving - Currently At ( " + Convert.ToString(x1_coord) + ", " + Convert.ToString(y1_coord) + ")");
                        graph.FillEllipse(paint_brush ,Convert.ToInt32(x1_coord), Convert.ToInt32(y1_coord),20,20);
                        break;
            }
            

            base.OnPaint(ee);

      }//End of OnPaint

      }//End of class Graphicpanel

}//End of clas CollidingBallsInterface