using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using CompiladorDFD.Datos_Externos;
namespace CompiladorDFD
{
    public partial class FrmTablaDeErrores : Form
    {
        public FrmTablaDeErrores()
        {
            InitializeComponent();
        }
        Error[] error;
        private void FrmTablaDeErrores_Load(object sender, EventArgs e)
        {
    
        }

        private void FrmTablaDeErrores_Load_1(object sender, EventArgs e)
        {
            error = ValoresGlobales.valores().tablaDeErrores.errores.ToArray();
            int i = 0;
            foreach (Error tempError in error)
            {
                DGrid.Rows.Add();
                DGrid.Rows[i].Cells[0].Value = i.ToString();
                DGrid.Rows[i].Cells[1].Value = tempError.faseAnalisis;
                DGrid.Rows[i].Cells[2].Value = tempError.detalle;
                i++;
            }
        }

        private void DGrid_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
        }

        private void DGrid_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            error[int.Parse(DGrid.Rows[e.RowIndex].Cells[0].Value.ToString())].ElementoError.LlamarFormulario();
     
        }

       
    }
}
