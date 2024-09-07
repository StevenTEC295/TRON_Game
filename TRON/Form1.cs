

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
        private const int pictureBoxSize = 23; // Tamaño de cada PictureBox
        private LinkedListGrid linkedListGrid;
        private List<Bot> bots;
        public System.Windows.Forms.Timer botTimer;

        private Direction currentDirection;
        public System.Windows.Forms.Timer moveTimer;
        private System.Windows.Forms.Timer itemGenerationTimer;
        /*private void GeneratePower()
        {
            Power hyperSpeedPower = new Power("HyperSpeed", moto =>
            {
                moto.Velocidad += 5;
            });

            linkedListGrid.AddPowerToRandomPosition(hyperSpeedPower);
        }*/
        public Form1()
        {
            InitializeComponent();
            InitializeLinkedListGrid();
            this.Controls.AddRange(linkedListGrid.Grid.Cast<Node>().Select(n => n.PictureBox).ToArray());

            currentDirection = Direction.Right; // Dirección inicial

            InitializeBots();

            // Configurar el temporizador para mover la moto automáticamente
            moveTimer = new System.Windows.Forms.Timer();
            moveTimer.Interval = 200; // Intervalo de movimiento en milisegundos
            moveTimer.Tick += MoveTimer_Tick;
            moveTimer.Start();

            /*// Configurar el temporizador para la generación de ítems
            itemGenerationTimer = new System.Windows.Forms.Timer();
            itemGenerationTimer.Interval = 5000; // Intervalo de generación de ítems en milisegundos (por ejemplo, cada 5 segundos)
            itemGenerationTimer.Tick += ItemGenerationTimer_Tick;
            itemGenerationTimer.Start();
            GenerateItem();*/

            // Timer para mover los bots
            botTimer = new System.Windows.Forms.Timer();
            botTimer.Interval = 500; // Intervalo en milisegundos
            botTimer.Tick += BotTimer_Tick;
            botTimer.Start();

        }
        private void InitializeBots()
        {
            bots = new List<Bot>();

            for (int i = 0; i < 3; i++)  // Crear 3 bots
            {
                Bot bot = new Bot(linkedListGrid);
                bots.Add(bot);
            }

            botTimer = new System.Windows.Forms.Timer();
            botTimer.Interval = 500; // Intervalo en milisegundos
            botTimer.Tick += BotTimer_Tick;
            botTimer.Start();
        }


        private void BotTimer_Tick(object sender, EventArgs e)
        {
            foreach (var bot in bots)
            {
                MoveBot(bot);
            }
        }


        public static bool CheckCollision(Node nextNode)
        {
            // Si el nodo es parte de una estela o una cabeza, hay colisión
            // nextNode.IsTrail || nextNode.IsHead;
            return false;
        }



        private void MoveTimer_Tick(object sender, EventArgs e)
        {
            MoveMoto();
        }

        // Mover la moto en la dirección actual
        public void MoveMoto()
        {
            Node currentNode = linkedListGrid.Moto.Head.GridNode;
            Node nextNode = GetNextNode(currentNode, currentDirection);

            if (CheckCollision(nextNode))
            {
                // Terminar el juego si hay colisión
                GameOver();
            }
            else
            {
                linkedListGrid.Moto.Move(nextNode);
            }
        }

        private Node GetNextNode(Node currentNode, Direction direction)
        {
            if (currentNode == null)
                return null;

            switch (direction)
            {
                case Direction.Up:
                    return currentNode.Up;
                case Direction.Down:
                    return currentNode.Down;
                case Direction.Left:
                    return currentNode.Left;
                case Direction.Right:
                    return currentNode.Right;
                default:
                    return null;
            }
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
            //linkedListGrid.AddItemToRandomPosition(new Item("Fuel", (moto) => moto.Combustible += 10));
            //linkedListGrid.AddPowerToRandomPosition(new Power("HyperSpeed", (moto) => moto.Velocidad += 2));
        }
        public void MoveBot(Bot bot)
        {
            Node currentNode = bot.Estela.Head.GridNode;
            Direction randomDirection = GetRandomDirection();
            Node nextNode = GetNextNode(currentNode, randomDirection);

            if (CheckCollision(nextNode))
            {
                // Si hay colisión, el bot muere
                bot.Die();
            }
            else
            {
                bot.Move(nextNode);
            }
        }

        private Direction GetRandomDirection()
        {
            Random random = new Random();
            int randomValue = random.Next(4); // Hay 4 direcciones posibles

            switch (randomValue)
            {
                case 0:
                    return Direction.Up;
                case 1:
                    return Direction.Down;
                case 2:
                    return Direction.Left;
                case 3:
                    return Direction.Right;
                default:
                    return Direction.Right;
            }
        }

        public void GameOver()
        {
            // Detener los temporizadores y mostrar mensaje de game over
            moveTimer.Stop();
            botTimer.Stop();
            MessageBox.Show("¡Juego terminado!");
            Application.Exit();
        }






    }
}

