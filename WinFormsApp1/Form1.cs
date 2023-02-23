using WinFormsApp1.Properties;

namespace WinFormsApp1
{
    public partial class Form1 : Form
    {
        private int count = 0;

        Symbol[,] mat = new Symbol[3, 3];
        Bitmap Ximage = WinFormsApp1.Properties.Resources.Ximage;
        Bitmap Oimage = WinFormsApp1.Properties.Resources.Oimage;
        public Form1()
        {
            InitializeComponent();
        }
        public void InitializeGame()
        {
            for(int i = 0; i < 3; i++)
            {
                for(int k = 0; k < 3; k++)
                {
                    mat[i,k] = Symbol.None; 
                }
            }
        }
        public enum Symbol
        {
            None,
            O,
            X
        }

        public enum GameState
        {
            Ongoing,
            Draw,
            Win
        }

        public Symbol TurnTracker(int count)
        {
            int turn = count % 2;
            if (turn == 0)
                return Symbol.X;
            else
                return Symbol.O;

        }
        
        private void ProcessClick(Button button, int buttonRowIndex, int buttonColumnIndex)
        {
            GameState gs;
            if (TurnTracker(count) == Symbol.X)
            {
                button.BackgroundImage = Ximage;
                mat[buttonRowIndex, buttonColumnIndex] = Symbol.X;
                gs = CheckWinner(Symbol.X,buttonRowIndex,buttonColumnIndex);
                if(gs == GameState.Win)
                {
                    DisableButtons();
                    label1.Text = "X HAS WON!";
                }
                if (gs == GameState.Draw)
                {
                    label1.Text = "GAME IS A DRAW";
                }
            }

            if (TurnTracker(count) == Symbol.O)
            {
                button.BackgroundImage = Oimage;
                mat[buttonRowIndex, buttonColumnIndex] = Symbol.O;
                gs = CheckWinner(Symbol.O, buttonRowIndex, buttonColumnIndex);
                if (gs == GameState.Win)
                {
                    DisableButtons();
                    label1.Text = "O HAS WON!";
                }
                if(gs == GameState.Draw)
                {
                    label1.Text = "GAME IS A DRAW";
                }
            }

            count++;
            button.Enabled = false;
        }
        private GameState CheckWinner(Symbol s,int x, int y)
        {
            //gameover conditions

            //check row:
            for(int i = 0; i < 3; i++)
            {
                if (mat[x,i] != s)
                    break;
                if(i == 2)
                    return GameState.Win;
            }
            //check column
            for(int i = 0; i < 3; i++)
            {
                if (mat[i, y] != s)
                    break;
                if (i == 2)
                    return GameState.Win;
            }
            //diagonal forward
            if (x == y)
            {
                for (int i = 0; i < 3; i++)
                {
                    if (mat[i,i] != s) 
                        break;
                    if(i==2)
                        return GameState.Win;
                }
            }
            //diagonal backward (0,2 to 2,0)
            if(x+y == 2)
            {
                for (int i = 0; i < 3; i++)
                {
                    if (mat[i,(2-i)]!=s)
                        break;
                    if(i==2)
                        return GameState.Win;
                }
            }
            //check if game is still ongoing
            if (count < 8)
                return GameState.Ongoing;
            else
                return GameState.Draw;

        }
        void DisableButtons()
        {
            foreach (Control c in Controls)
            {
                Button b = c as Button;
                if (b != null && b != button10)
                { 
                    b.Enabled = false;
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ProcessClick(button1, 0,0);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            ProcessClick(button2, 0,1);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            ProcessClick(button3, 0,2);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            ProcessClick(button4, 1,0);
        }

        private void button5_Click(object sender, EventArgs e)
        {
            ProcessClick(button5, 1,1);
        }

        private void button6_Click(object sender, EventArgs e)
        {
            ProcessClick(button6, 1,2);
        }

        private void button7_Click(object sender, EventArgs e)
        {
            ProcessClick(button7, 2,0);
        }

        private void button8_Click(object sender, EventArgs e)
        {
            ProcessClick(button8, 2,1);
        }

        private void button9_Click(object sender, EventArgs e)
        {
            ProcessClick(button9, 2,2);
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button10_Click(object sender, EventArgs e)
        {
            Application.Restart();
        }
    }
}