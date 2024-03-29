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

namespace WindowForm1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            var con = Configuration.getInstance().getConnection();
            SqlCommand cmd = new SqlCommand("Insert into student values (@Id, @Name, @Section, @Contact, @Address, @CGPA)", con);
            if (textBox1.Text == "" || textBox6.Text == "" || textBox5.Text == "" || textBox2.Text == "" || textBox3.Text == "" || textBox4.Text == "")
            {
                MessageBox.Show("Some Fields are missing!!!");
            }
            else
            {
                cmd.Parameters.AddWithValue("@Id", int.Parse(textBox1.Text));
                cmd.Parameters.AddWithValue("@Name", textBox6.Text);
                cmd.Parameters.AddWithValue("@Section", textBox5.Text);
                cmd.Parameters.AddWithValue("@Contact", textBox4.Text);
                cmd.Parameters.AddWithValue("@Address", textBox3.Text);
                cmd.Parameters.AddWithValue("@CGPA", textBox2.Text);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Successfully saved");
                LoadData();
            }



        }

        private void button1_Click(object sender, EventArgs e)
        {
            LoadData();
        }
        void LoadData()
        {
            var con = Configuration.getInstance().getConnection();
            SqlCommand cmd = new SqlCommand("Select * from student", con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dataGridView1.DataSource = dt;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            var con = Configuration.getInstance().getConnection();
            SqlCommand cmd = new SqlCommand("UPDATE student SET Id = @Id, Name = @Name, Section=@Section, Contact=@Contact, Address=@Address, CGPA=@CGPA WHERE Id=@Id", con);
            cmd.Parameters.AddWithValue("@Id", int.Parse(textBox1.Text));
            cmd.Parameters.AddWithValue("@Name", textBox6.Text);
            cmd.Parameters.AddWithValue("@Section", textBox5.Text);
            cmd.Parameters.AddWithValue("@Contact", textBox4.Text);
            cmd.Parameters.AddWithValue("@Address", textBox3.Text);
            cmd.Parameters.AddWithValue("@CGPA", textBox2.Text);
            cmd.ExecuteNonQuery();
            MessageBox.Show("Successfully Updated");
        }

        private void button4_Click(object sender, EventArgs e)
        {
            var con = Configuration.getInstance().getConnection();
            SqlCommand cmd = new SqlCommand("DELETE FROM student WHERE Id=@Id", con);
            if (textBox1.Text == "")
            {
                MessageBox.Show("Id is missing!!!");
            }
            else
            {
                cmd.Parameters.AddWithValue("@Id", int.Parse(textBox1.Text));
                cmd.ExecuteNonQuery();
                MessageBox.Show("Successfully Deleted");
            }
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
            {
                return;
            }
            DataGridViewRow selectedRow = dataGridView1.Rows[e.RowIndex];

            textBox1.Text = selectedRow.Cells["Id"].Value.ToString();
            textBox6.Text = selectedRow.Cells["Name"].Value.ToString();
            textBox5.Text = selectedRow.Cells["Section"].Value.ToString();
            textBox4.Text = selectedRow.Cells["Contact"].Value.ToString();
            textBox3.Text = selectedRow.Cells["Address"].Value.ToString();
            textBox2.Text = selectedRow.Cells["CGPA"].Value.ToString();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if(comboBox1.SelectedItem == null)
            {
                MessageBox.Show("Please select a valid column from the ComboBox. to perform search on.");
                return;
            }
            string selectedColumn = comboBox1.SelectedItem.ToString(); 

            if (!string.IsNullOrEmpty(selectedColumn))
            {
                var con = Configuration.getInstance().getConnection();
                SqlCommand cmd = new SqlCommand($"SELECT * FROM student WHERE {selectedColumn} = @{selectedColumn}", con);
                string value = GetTextBoxValue(selectedColumn);
                if(value == "" || value == null)
                {
                    MessageBox.Show("The TextBox on whose basis you want to search cannot be empty");
                    return;
                }
                cmd.Parameters.AddWithValue($"@{selectedColumn}", value); 

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);

                dataGridView1.DataSource = dt;
            }
            else
            {
                MessageBox.Show("Please select a valid column from the ComboBox. to perform search on.");
            }
        }

        private string GetTextBoxValue(string columnName)
        {
            switch (columnName)
            {
                case "Id":
                    return textBox1.Text;
                case "Name":
                    return textBox6.Text;
                case "Section":
                    return textBox5.Text;
                case "Contact":
                    return textBox4.Text;
                case "Address":
                    return textBox3.Text;
                case "CGPA":
                    return textBox2.Text;
                default:
                    return string.Empty;
            }
        }
    }
}
