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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace HomeTaskWindowForms
{
    public partial class StudentCRUD : Form
    {
        public StudentCRUD()
        {
            InitializeComponent();
        }
        private void LoadData()
        {
            var con = Configuration.getInstance().getConnection();
            SqlCommand cmd = new SqlCommand("Select * from student", con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dataGridView1.DataSource = dt;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            LoadData();
        }


        private void button2_Click(object sender, EventArgs e)
        {

            var con = Configuration.getInstance().getConnection();
            SqlCommand cmd = new SqlCommand("Insert into student values (@RegistrationNumber, @Name, @Department, @Session, @Address)", con);
            if (textBox1.Text == "" || textBox6.Text == "" || textBox5.Text == "" || textBox2.Text == "" || textBox4.Text == "")
            {
                MessageBox.Show("No Input Field can be empty!!!");
            }
            else
            {
                if (!textBox4.Text.All(char.IsDigit))
                {
                    MessageBox.Show("Session can only contain numbers");
                    return;
                }
                cmd.Parameters.AddWithValue("@RegistrationNumber", textBox1.Text);
                cmd.Parameters.AddWithValue("@Name", textBox6.Text);
                cmd.Parameters.AddWithValue("@Department", textBox5.Text);
                cmd.Parameters.AddWithValue("@Session", int.Parse(textBox4.Text));
                cmd.Parameters.AddWithValue("@Address", textBox2.Text);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Successfully saved");
                LoadData();
            }

        }

        private void button3_Click(object sender, EventArgs e)
        {
            var con = Configuration.getInstance().getConnection();
            SqlCommand cmd = new SqlCommand("UPDATE student SET RegistrationNumber = @RegistrationNumber, Name = @Name,Department = @Department,Session = @Session,Address = @Address WHERE RegistrationNumber=@RegistrationNumber", con);
            if (textBox1.Text == "" || textBox6.Text == "" || textBox5.Text == "" || textBox2.Text == "" || textBox4.Text == "")
            {
                MessageBox.Show("No Input Field can be empty!!!");
            }
            else
            {
                if (!textBox4.Text.All(char.IsDigit))
                {
                    MessageBox.Show("Session can only contain numbers");
                    return;
                }
                cmd.Parameters.AddWithValue("@RegistrationNumber", textBox1.Text);
                cmd.Parameters.AddWithValue("@Name", textBox6.Text);
                cmd.Parameters.AddWithValue("@Department", textBox5.Text);
                cmd.Parameters.AddWithValue("@Session", int.Parse(textBox4.Text));
                cmd.Parameters.AddWithValue("@Address", textBox2.Text);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Successfully Updated");
                LoadData();
                UpdateinResultTable();
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            var con = Configuration.getInstance().getConnection();
            SqlCommand cmd = new SqlCommand("DELETE FROM student WHERE RegistrationNumber=@RegistrationNumber", con);
            if (textBox1.Text == "")
            {
                MessageBox.Show("RegistrationNumber is missing!!!");
            }
            else
            {
                cmd.Parameters.AddWithValue("@RegistrationNumber", int.Parse(textBox1.Text));
                cmd.ExecuteNonQuery();
                MessageBox.Show("Successfully Deleted");
                LoadData();
            }
        }
        private void UpdateinResultTable()
        {
            var con = Configuration.getInstance().getConnection();
            SqlCommand cmd = new SqlCommand("UPDATE Result SET Student_Id = @Student_Id, Student_Name = @Student_Name,Session = @Session WHERE Student_Id=@Student_Id", con);

            cmd.Parameters.AddWithValue("@Student_Id", textBox1.Text);
            cmd.Parameters.AddWithValue("@Student_Name", textBox6.Text);
            cmd.Parameters.AddWithValue("@Session", int.Parse(textBox4.Text));
            cmd.ExecuteNonQuery();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (comboBox1.SelectedItem == null)
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
                if (value == "" || value == null)
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
                case "RegistrationNumber":
                    return textBox1.Text;
                case "Name":
                    return textBox6.Text;
                case "Department":
                    return textBox5.Text;
                case "Session":
                    return textBox4.Text;
                case "Address":
                    return textBox2.Text;
                default:
                    return string.Empty;
            }
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
            {
                return;
            }
            DataGridViewRow selectedRow = dataGridView1.Rows[e.RowIndex];

            textBox1.Text = selectedRow.Cells["RegistrationNumber"].Value.ToString();
            textBox6.Text = selectedRow.Cells["Name"].Value.ToString();
            textBox5.Text = selectedRow.Cells["Department"].Value.ToString();
            textBox4.Text = selectedRow.Cells["Session"].Value.ToString();
            textBox2.Text = selectedRow.Cells["Address"].Value.ToString();
        }
    }
}
