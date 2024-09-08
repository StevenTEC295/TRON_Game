

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
        public List<Bot> bots;
        public System.Windows.Forms.Timer botTimer;

        private Direction currentDirection;
        public System.Windows.Forms.Timer moveTimer;
        private System.Windows.Forms.Timer itemGenerationTimer;
        
        public Form1()
        {
            InitializeComponent();
            InitializeLinkedListGrid();
            InitializeBots();
            this.Controls.AddRange(linkedListGrid.Grid.Cast<Node>().Select(n => n.PictureBox).ToArray());

            currentDirection = Direction.Right; // Dirección inicial

            

            

           

            // Timer para mover los bots
            botTimer = new System.Windows.Forms.Timer();
            botTimer.Interval = 500; // Intervalo en milisegundos
            botTimer.Tick += BotTimer_Tick;
            botTimer.Start();
            // Configurar el temporizador para mover la moto automáticamente
            moveTimer = new System.Windows.Forms.Timer();
            moveTimer.Interval = 200; // Intervalo de movimiento en milisegundos
            moveTimer.Tick += MoveTimer_Tick;
            moveTimer.Start();

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
            
            var botsToMove = new List<Bot>(bots);

            foreach (var bot in botsToMove)
            {
                MoveBot(bot);
            }
        }



        public static bool CheckCollision(Node nextNode)
        {
            // Si el nodo es parte de una estela o una cabeza, hay colisión
            // nextNode.IsTrail || nextNode.IsHead;
            return nextNode.IsTrail || nextNode.IsHead;
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


        private void MoveBot(Bot bot)
        {
            if (bot.Estela == null || bot.Estela.Head == null)
            {
                return; // No mover si no hay estela o cabeza.
            }

            Node botNode = bot.Estela.Head.GridNode;
            Direction followDirection = GetDirectionToFollow(botNode, linkedListGrid.Moto.Head.GridNode);
            Node nextNode = GetNextNode(botNode, followDirection);

            if (nextNode != null && !CheckCollision(nextNode))
            {
                bot.Move(nextNode);
                bot.currentDirection = followDirection; // Actualiza la última dirección del bot
            }
            else
            {
                RemoveBot(bot); // El bot muere si colisiona con su estela o con otro objeto
            }
        }







        // Método que obtiene una dirección válida para el bot
        private Direction GetDirectionToFollow(Node botNode, Node motoNode)
        {
            if (botNode == null || motoNode == null)
                return Direction.Right; // Valor por defecto si uno de los nodos es nulo

            int botRow = botNode.GridRow; 
            int botCol = botNode.GridColumn; 

            int motoRow = motoNode.GridRow;
            int motoCol = motoNode.GridColumn;

            if (Math.Abs(botRow - motoRow) > Math.Abs(botCol - motoCol))
            {
                
                return botRow < motoRow ? Direction.Down : Direction.Up;
            }
            else
            {
                
                return botCol < motoCol ? Direction.Right : Direction.Left;
            }
        }

        public void RemoveBot(Bot bot)
        {
            if (bot.Estela == null || bot.Estela.Head == null)
                return;

            Node currentNode = bot.Estela.Head.GridNode;
            while (currentNode != null)
            {
                // Marca el nodo como no parte de una estela
                currentNode.IsTrail = false;

                // Mueve al siguiente nodo en la estela
                currentNode = GetNextNode(currentNode, bot.currentDirection);
            }

            // Elimina el bot de la lista de bots
            bots.Remove(bot);

            // Opcional: hacer algo especial si solo queda un bot, etc.
            if (bots.Count == 0)
            {
                // Aquí podrías mostrar un mensaje o terminar el juego si es necesario
                Win();
            }
        }



        public void Win()
        {
            // Detener los temporizadores
            moveTimer.Stop();
            botTimer.Stop();

            // Crear y mostrar el formulario de Victoria
            GameOver winForm = new GameOver("You Win");
            winForm.Show();

            // Cerrar el formulario principal
            //this.Close();
        }


        public void GameOver()
        {
            // Detener los temporizadores
            moveTimer.Stop();
            botTimer.Stop();
            

            // Crear y mostrar el formulario de Game Over
            GameOver gameOverForm = new GameOver("Game Over");
            gameOverForm.Show();

            // Cerrar el formulario principal
            //this.Close();
        }

    }
}

