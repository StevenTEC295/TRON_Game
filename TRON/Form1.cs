

namespace TRON

{
    public partial class Form1 : Form
    {
        private Grid grid;
        private Moto moto;
        private int cellSize = 20; // Tamaño de cada celda en píxeles
        public Form1()
        {
            InitializeComponent();
            InicializarJuego();
            this.Paint += new PaintEventHandler(Form1_Paint);
            this.KeyDown += new KeyEventHandler(Form1_KeyDown);
        }
        private void InicializarJuego()
        {
            // Crear un grid de 20x20 celdas
            grid = new Grid(20, 20);

            // Inicializar la moto en el nodo superior izquierdo
            moto = new Moto(grid.PrimerNodo);
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;

            // Dibujar el grid
            Nodo nodoActualFila = grid.PrimerNodo;
            Nodo nodoActual = nodoActualFila;

            for (int i = 0; nodoActualFila != null; i++)
            {
                nodoActual = nodoActualFila;
                for (int j = 0; nodoActual != null; j++)
                {
                    Rectangle rect = new Rectangle(j * cellSize, i * cellSize, cellSize, cellSize);
                    g.DrawRectangle(Pens.Black, rect);

                    // Si el nodo está ocupado por la moto o la estela, lo coloreamos
                    if (nodoActual.Ocupado)
                    {
                        g.FillRectangle(Brushes.Blue, rect);
                    }

                    nodoActual = nodoActual.Derecha;
                }
                nodoActualFila = nodoActualFila.Abajo;
            }


        }
        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Up:
                    MoverMoto(moto,"Arriba");
                    break;
                case Keys.Down:
                    MoverMoto(moto, "Abajo");
                    break;
                case Keys.Left:
                    MoverMoto(moto,"Izquierda");
                    break;
                case Keys.Right:
                    MoverMoto(moto, "Derecha");
                    break;
            }

            // Redibujar el formulario
            this.Invalidate();
        }

        private void MoverMoto(Moto moto, string direccion)
        {
            Nodo nuevoNodo = null;

            switch (direccion)
            {
                case "Arriba":
                    nuevoNodo = moto.Cabeza.Arriba;
                    break;
                case "Abajo":
                    nuevoNodo = moto.Cabeza.Abajo;
                    break;
                case "Izquierda":
                    nuevoNodo = moto.Cabeza.Izquierda;
                    break;
                case "Derecha":
                    nuevoNodo = moto.Cabeza.Derecha;
                    break;
            }

            if (nuevoNodo != null && !nuevoNodo.Ocupado)
            {
                moto.Mover(nuevoNodo);
            }
            else
            {
                //implementar la lógica de colisión o fin de juego.
            }
        }


    }
}
