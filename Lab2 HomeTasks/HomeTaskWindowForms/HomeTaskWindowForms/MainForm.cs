using CRUD_Operations;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HomeTaskWindowForms
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void label7_Click(object sender, EventArgs e)
        {
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            StudentCRUD studentCRUD = new StudentCRUD();
            studentCRUD.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            CourseCRUD courseCRUD = new CourseCRUD();
            courseCRUD.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            ResultForm resultForm = new ResultForm();
            resultForm.Show();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Close();
            
        }
    }
}
