

using TRON.Properties;

namespace TRON

{
    public partial class Form1 : Form
    {
        private const int gridRowsSize = 38;
        private const int gridColsSize = 39; // Tamaño del grid NxN
         // Tamaño del grid NxN
        private const int pictureBoxSize = 25; // Tamaño de cada PictureBox
        private LinkedListGrid linkedListGrid;
        public Form1()
        {
            InitializeComponent();
            InitializeLinkedListGrid();
           
        }
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
