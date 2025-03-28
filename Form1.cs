namespace Movement
{
    public partial class Form1 : Form
    {
        private int _xSpeed = 2;
        private int _ySpeed = 2;
        private List<Button> _targets = new List<Button>();
        private int _score = 0;
        private Label _scoreLabel;
        private Random _random = new Random();

        public Form1()
        {
            InitializeComponent();
            SetupGame();
            timer1.Start();
            timer1.Interval = 10;
            MakeTargets();
        }

        private void SetupGame()
        {
            _scoreLabel = new Label
            {
                Location = new Point(10, 10),
                Text = "Score: 0",
                AutoSize = true
            };
            Controls.Add(_scoreLabel);
        }

        private void MakeTargets()
        {
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 6; j++)
                {
                    var target = new Button
                    {
                        BackColor = Color.FromArgb(_random.Next(256), _random.Next(256), _random.Next(256)),
                        Left = 50 + 100 * j,
                        Width = 95,
                        Top = 50 + 45 * i,
                        Height = 40,
                        Enabled = false
                    };

                    _targets.Add(target);
                    Controls.Add(target);
                }
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            
            if (button1.Right >= panel1.Left &&
                button1.Left <= panel1.Right &&
                button1.Bottom >= panel1.Top &&
                button1.Top <= panel1.Bottom)
            {
                _ySpeed = -Math.Abs(_ySpeed);
                _xSpeed = (_xSpeed > 0) ? 2 : -2; 
            }

            for (int i = _targets.Count - 1; i >= 0; i--)
            {
                if (button1.Bounds.IntersectsWith(_targets[i].Bounds) && _targets[i].Visible)
                {
                    _targets[i].Visible = false;
                    _score += 10;
                    _scoreLabel.Text = $"Score: {_score}";
                    _ySpeed = Math.Abs(_ySpeed); 
                }
            }

        
            if (button1.Right >= ClientSize.Width) _xSpeed = -Math.Abs(_xSpeed);
            if (button1.Left <= 0) _xSpeed = Math.Abs(_xSpeed);
            if (button1.Bottom >= ClientSize.Height)
            {
               
                timer1.Stop();
                MessageBox.Show($"Game Over! Score: {_score}");
                ResetGame();
                return;
            }
            if (button1.Top <= 0) _ySpeed = Math.Abs(_ySpeed);

           
            button1.Left += _xSpeed;
            button1.Top += _ySpeed;

            if (_targets.All(t => !t.Visible))
            {
                timer1.Stop();
                MessageBox.Show($"You Win! Score: {_score}");
                ResetGame();
            }
        }

        private void ResetGame()
        {
            button1.Location = new Point(334, 334);
           
            _xSpeed = 2;
            _ySpeed = 2;
            _score = 0;
            _scoreLabel.Text = "Score: 0";
            foreach(var target in _targets)
            {
                target.Visible = true;
            }
            timer1.Start();

        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.Left)
            {
                if (panel1.Left >= 10)
                    panel1.Left -= 10;
            }
            else if (keyData == Keys.Right)
            {
                if (panel1.Right <= ClientSize.Width)
                    panel1.Left += 10;
            }
            
            return base.ProcessCmdKey(ref msg, keyData);
        }
    }
}
