using CRUD_Operations;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HomeTaskWindowForms
{
    public partial class CourseCRUD : Form
    {
        public CourseCRUD()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            LoadData();
        }
        private void LoadData()
        {
            var con = Configuration.getInstance().getConnection();
            SqlCommand cmd = new SqlCommand("Select * from Course", con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dataGridView1.DataSource = dt;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            var con = Configuration.getInstance().getConnection();
            SqlCommand cmd = new SqlCommand("Insert into Course values (@Course_Id, @Course_Name, @Student_Name, @Teacher_Name, @Semester)", con);
            if (textBox1.Text == "" || textBox6.Text == "" || textBox5.Text == "" || textBox2.Text == "" || textBox4.Text == "")
            {
                MessageBox.Show("No Input Field can be empty!!!");
            }
            else
            {
                cmd.Parameters.AddWithValue("@Course_Id", textBox1.Text);
                cmd.Parameters.AddWithValue("@Course_Name", textBox6.Text);
                cmd.Parameters.AddWithValue("@Student_Name", textBox5.Text);
                cmd.Parameters.AddWithValue("@Teacher_Name", textBox4.Text);
                cmd.Parameters.AddWithValue("@Semester", textBox2.Text);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Successfully saved");
                LoadData();
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            var con = Configuration.getInstance().getConnection();
            SqlCommand cmd = new SqlCommand("UPDATE Course SET Course_Id=@Course_Id, Course_Name=@Course_Name, Student_Name=@Student_Name, Teacher_Name=@Teacher_Name, Semester=@Semester WHERE Course_Id=@Course_Id", con);
            if (textBox1.Text == "" || textBox6.Text == "" || textBox5.Text == "" || textBox2.Text == "" || textBox4.Text == "")
            {
                MessageBox.Show("No Input Field can be empty!!!");
            }
            else
            {
                cmd.Parameters.AddWithValue("@Course_Id", textBox1.Text);
                cmd.Parameters.AddWithValue("@Course_Name", textBox6.Text);
                cmd.Parameters.AddWithValue("@Student_Name", textBox5.Text);
                cmd.Parameters.AddWithValue("@Teacher_Name", textBox4.Text);
                cmd.Parameters.AddWithValue("@Semester", textBox2.Text);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Successfully Updated");
                LoadData();
                UpdateinResultTable();

            }
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
            {
                return;
            }
            DataGridViewRow selectedRow = dataGridView1.Rows[e.RowIndex];

            textBox1.Text = selectedRow.Cells["Course_Id"].Value.ToString();
            textBox6.Text = selectedRow.Cells["Course_Name"].Value.ToString();
            textBox5.Text = selectedRow.Cells["Student_Name"].Value.ToString();
            textBox4.Text = selectedRow.Cells["Teacher_Name"].Value.ToString();
            textBox2.Text = selectedRow.Cells["Semester"].Value.ToString();
        }
        private void UpdateinResultTable()
        {
            var con = Configuration.getInstance().getConnection();
            SqlCommand cmd = new SqlCommand("UPDATE Result SET Course_Name = @Course_Name, Semester = @Semester WHERE Course_Name=@Course_Name", con);

            cmd.Parameters.AddWithValue("@Course_Name", textBox6.Text);
            cmd.Parameters.AddWithValue("@Semester", ExtractIntegers(textBox2.Text));
            cmd.ExecuteNonQuery();
        }
        private void button4_Click(object sender, EventArgs e)
        {
            var con = Configuration.getInstance().getConnection();
            SqlCommand cmd = new SqlCommand("DELETE FROM Course WHERE Course_Id=@Course_Id", con);
            if (textBox1.Text == "")
            {
                MessageBox.Show("Course_Id is missing!!!");
            }
            else
            {
                cmd.Parameters.AddWithValue("@Course_Id", textBox1.Text);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Successfully Deleted");
                LoadData();
            }

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
                SqlCommand cmd = new SqlCommand($"SELECT * FROM Course WHERE {selectedColumn} = @{selectedColumn}", con);
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
                case "Course_Id":
                    return textBox1.Text;
                case "Course_Name":
                    return textBox6.Text;
                case "Student_Name":
                    return textBox5.Text;
                case "Teacher_Name":
                    return textBox4.Text;
                case "Semester":
                    return textBox2.Text;
                default:
                    return string.Empty;
            }
        }
        private string ExtractIntegers(string input)
        {
            // Define a regular expression pattern to match integers
            Regex regex = new Regex(@"\d+");

            // Find all matches in the input string
            MatchCollection matches = regex.Matches(input);

            // Concatenate all matched integers
            string result = "";
            foreach (Match match in matches)
            {
                result += match.Value;
            }

            return result;
        }
    }
}
