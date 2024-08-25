

using TRON.Properties;



namespace TRON {

    public enum Direction
    {
        Up,
        Down, 
        Left, 
        Right
    }
    


    public partial class Form1 : Form
    {
        private const int gridRowsSize = 38;
        private const int gridColsSize = 39; // Tamaño del grid NxN
         // Tamaño del grid NxN
        private const int pictureBoxSize = 25; // Tamaño de cada PictureBox
        private LinkedListGrid linkedListGrid;

        private Direction currentDirection;
        System.Windows.Forms.Timer moveTimer;
        public Form1()
        {
            InitializeComponent();
            InitializeLinkedListGrid();
            this.Controls.AddRange(linkedListGrid.Grid.Cast<Node>().Select(n => n.PictureBox).ToArray());

            currentDirection = Direction.Right; // Dirección inicial

            // Configurar el temporizador para mover la moto automáticamente
            moveTimer = new System.Windows.Forms.Timer();
            moveTimer.Interval = 200; // Intervalo de movimiento en milisegundos
            moveTimer.Tick += MoveTimer_Tick;
            moveTimer.Start();

        }

        private void MoveTimer_Tick(object sender, EventArgs e)
        {
            MoveMoto();
        }

        // Mover la moto en la dirección actual
        private void MoveMoto()
        {
            linkedListGrid.MoverMoto(currentDirection);
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            switch (keyData)
            {
                case Keys.Left:
                    if (currentDirection != Direction.Right)
                        currentDirection = Direction.Left;
                    break;
                case Keys.Right:
                    if (currentDirection != Direction.Left)
                        currentDirection = Direction.Right;
                    break;
                case Keys.Up:
                    if (currentDirection != Direction.Down)
                        currentDirection = Direction.Up;
                    break;
                case Keys.Down:
                    if (currentDirection != Direction.Up)
                        currentDirection = Direction.Down;
                    break;
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }

        // Cambiar la dirección a la izquierda
        
        private void InitializeLinkedListGrid()
        {
            
            linkedListGrid = new LinkedListGrid(gridRowsSize, gridColsSize, pictureBoxSize);

            // Agregar cada PictureBox al formulario
            foreach (var node in linkedListGrid.Grid)
            {
                this.Controls.Add(node.PictureBox);
            }
        }
        



    }
}
