/* 
Silly Code by AMELIA ROTONDO 
Last Edited: 11/05/2022
*/

using System;
using System.Drawing;
using System.Windows.Forms;
using System.Timers;


public class RicochetBallInterface: Form
{

      // USER INTERFACE INITIALIZATION
      private Label title       = new Label();
      private Label coord_title = new Label();
      private Label speed_title = new Label();
      private Label dir_title   = new Label();
      private Label x_title     = new Label();
      private Label y_title     = new Label();
      private Label x_output    = new Label();
      private Label y_output    = new Label();
      private Panel header            = new Panel();
      private Panel controlz          = new Panel();
      private Graphicpanel ball_mover = new Graphicpanel();
      private Button init_button  = new Button();
      private Button start_button = new Button();
      private Button quit_button  = new Button();
      private TextBox speed_input = new TextBox();
      private TextBox dir_input   = new TextBox();

      //UI STYLE VARIABLES
      private Size program_size   = new Size(1000, 1200);
      private Size button_size    = new Size(150, 100);
      private Font control_font      = new Font("Comic Sans MS", 18, FontStyle.Regular);
      private Font title_font        = new Font("Impact", 25, FontStyle.Bold);

      //UX VARIABLES 
      private static double BALL_MOVEMENTS = 30;
      private static double delta_x   = 0;
      private static double delta_y   = 0; 
      private static double x_coord   = 490; 
      private static double y_coord   = 340; 
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
      public RicochetBallInterface() 
      {
            //DECLARE SIZES
            MaximumSize = program_size;
            MinimumSize = program_size;

            //INIT. TEXT
            title.Text        = "Ricochet Ball by Amelia Rotondo";
            coord_title.Text  = "Ball Coords:";
            speed_title.Text  = "SET THE SPEED:";
            dir_title.Text    = "SET THE ANGLE:";
            x_title.Text      = "X = ";
            y_title.Text      = "Y = ";
            init_button.Text  = "Init.";
            start_button.Text = "Start!";
            quit_button.Text  = "Quit...";
                                    
            //INIT. SIZES
            Size              = MaximumSize;
            title.Size        = new Size(800, 90);
            header.Size       = new Size(1000, 200);
            ball_mover.Size   = new Size(1000, 700);
            controlz.Size     = new Size(1000, 300);
            coord_title.Size  = new Size(200, 60);
            speed_title.Size  = new Size(200, 60);
            dir_title.Size    = new Size(200, 60);
            x_title.Size      = new Size(150, 70);
            y_title.Size      = new Size(150, 70);
            init_button.Size  = button_size;
            start_button.Size = button_size;
            quit_button.Size  = button_size;
            speed_input.Size  = new Size(120, 80);
            dir_input.Size    = new Size(120, 80);
            x_output.Size     = new Size(120, 90);
            y_output.Size     = new Size(120, 90);
            
            //INIT. COLORS
            header.BackColor       = Color.LightSeaGreen;
            ball_mover.BackColor   = Color.LightCyan;
            controlz.BackColor     = Color.LightGoldenrodYellow;
            coord_title.BackColor  = Color.LightCoral;
            speed_title.BackColor  = Color.LightCoral;
            dir_title.BackColor    = Color.LightCoral;
            x_title.BackColor      = Color.LightCoral;
            y_title.BackColor      = Color.LightCoral;
            speed_input.BackColor  = Color.LightPink;
            dir_input.BackColor    = Color.LightPink;
            x_output.BackColor     = Color.LightPink;
            y_output.BackColor     = Color.LightPink; 
            init_button.BackColor  = Color.LightPink;
            start_button.BackColor = Color.LightPink;
            quit_button.BackColor  = Color.LightPink;

            //INIT. FONTS
            title.Font        = title_font;
            coord_title.Font  = control_font;
            speed_title.Font  = control_font;
            dir_title.Font    = control_font; 
            x_title.Font      = control_font;
            y_title.Font      = control_font;
            init_button.Font  = control_font;
            start_button.Font = control_font;
            quit_button.Font  = control_font;
            speed_input.Font  = control_font;
            dir_input.Font    = control_font;
            x_output.Font     = control_font;
            y_output.Font     = control_font;

            //INIT. ALIGNMENTS
            title.TextAlign        = ContentAlignment.MiddleCenter;
            coord_title.TextAlign  = ContentAlignment.MiddleCenter;
            speed_title.TextAlign  = ContentAlignment.MiddleCenter;
            dir_title.TextAlign    = ContentAlignment.MiddleCenter;
            x_title.TextAlign      = ContentAlignment.MiddleCenter;
            y_title.TextAlign      = ContentAlignment.MiddleCenter;
            init_button.TextAlign  = ContentAlignment.MiddleCenter;
            start_button.TextAlign = ContentAlignment.MiddleCenter;
            quit_button.TextAlign  = ContentAlignment.MiddleCenter;

            //INIT. LOCATIONS
            header.Location     = new Point(0, 0);
            ball_mover.Location = new Point(0, 200);
            controlz.Location   = new Point(0, 900);

            title.Location       = new Point(125, 40);
            coord_title.Location = new Point(450, 130);
            speed_title.Location = new Point(200, 10);
            dir_title.Location   = new Point(550, 10);
            x_title.Location     = new Point(300, 200);
            y_title.Location     = new Point(600, 200);
            speed_input.Location = new Point(410, 10);
            dir_input.Location   = new Point(760, 10);
            x_output.Location    = new Point(460, 200);
            y_output.Location    = new Point(760, 200);

            init_button.Location  = new Point(30, 10);
            start_button.Location = new Point(40, 150);
            quit_button.Location = new Point(790, 75);

            //INIT. CONTROLS
            Controls.Add(header);
            Controls.Add(ball_mover);
            Controls.Add(controlz);
            header.Controls.Add(title);
                      
            //INIT. CONTROLZ
            controlz.Controls.Add(coord_title);
            controlz.Controls.Add(speed_title);
            controlz.Controls.Add(dir_title);
            controlz.Controls.Add(x_title);
            controlz.Controls.Add(y_title);
            controlz.Controls.Add(speed_input);
            controlz.Controls.Add(dir_input);
            controlz.Controls.Add(x_output);
            controlz.Controls.Add(y_output);
            controlz.Controls.Add(init_button);
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

      }//End of constructor RicochetBallInterface
      
      //Helper Method to Reset the Run
      protected void resetRun(Object sender, EventArgs events)
      {
            //Clear Text
            speed_input.Text = " ";
            dir_input.Text   = " ";
            x_output.Text    = " ";
            y_output.Text    = " ";

            start_button.Text = "Start!";
            quit_button.Text  = "Quit...";

            //Reset Variable
            BALL_MOVEMENTS = 0;
            x_coord        = 490;
            y_coord        = 340;
            delta_x        = 0;
            delta_y        = 0;
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
                              dir_angle      = Convert.ToDouble(dir_input.Text);
                              BALL_MOVEMENTS = 1000 * Convert.ToDouble(speed_input.Text);
                        }

                        start_button.Text= "Pause?";

                        //calculate the delta_x and delta_y
                        delta_x = (BALL_MOVEMENTS / ball_interval) * Math.Sin(dir_angle);
                        delta_y = (BALL_MOVEMENTS / ball_interval) * Math.Cos(dir_angle);
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
                  x_coord += delta_x;
                  y_coord += delta_y;
            }

            x_output.Text = Convert.ToString((Convert.ToInt32(x_coord) + 10));
            y_output.Text = Convert.ToString((Convert.ToInt32(y_coord) + 10));

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
            if(x_coord + delta_x < 0 || x_coord + delta_x > 980) 
                  {    
                        
                        if(x_coord < 500) 
                              {x_coord = 0; y_coord = y_coord + (delta_y * (x_coord / delta_x));}
                        else 
                              {x_coord = 980; y_coord = y_coord + (delta_y * ((980 - x_coord) / delta_x));}
                        delta_x = delta_x * -1;
                        return true;
                  }

            if(y_coord + delta_y < 0 || y_coord + delta_y > 680) 
                  {
                        
                        if(y_coord < 350) 
                              {y_coord = 0; x_coord = x_coord + (delta_x * (y_coord / delta_y));}
                        else 
                              {y_coord = 680; x_coord = x_coord + (delta_x * (680 - y_coord / delta_y));}
                        
                        delta_y = delta_y * -1;
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
                        Console.WriteLine("A Ball Is Moving - Currently At ( " + Convert.ToString(x_coord) + ", " + Convert.ToString(y_coord) + ")");
                        graph.FillEllipse(paint_brush ,Convert.ToInt32(x_coord), Convert.ToInt32(y_coord),20,20);
                        break;
            }
            

            base.OnPaint(ee);

      }//End of OnPaint

      }//End of class Graphicpanel

}//End of clas RicochetBallInterface