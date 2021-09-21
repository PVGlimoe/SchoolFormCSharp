using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SchoolForm
{
    public partial class Form1 : Form
    {
        List<School> listOfSchools;
        public Form1()
        {
            InitializeComponent();
            listOfSchools = GetSchools();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            textBox1.AutoCompleteMode = AutoCompleteMode.Suggest;
            textBox1.AutoCompleteSource = AutoCompleteSource.CustomSource;
            AutoCompleteStringCollection autoComplete = new AutoCompleteStringCollection();
            foreach (var school in listOfSchools)
            {
                autoComplete.Add(school.Name);
            }
            
            textBox1.AutoCompleteCustomSource = autoComplete;
        }

        protected List<School> GetSchools()
        {
            List<School> listOfSchools = new List<School>();

            var schoolData = File.ReadAllLines(@"C:\csvFiles\folkeSkole\FolkeskoleDK13-09-2021UTF-8.csv")
               .Skip(2)
               .Where(row => row != "\0");

            foreach (var school in schoolData)
            {
                var data = school.Replace("\0", "").Split(';');

                var schoolName = data[2];
                var address = data[4];
                var zipCode = data[5];
                var city = data[17];

                School s = new School(schoolName, address, zipCode, city);
                listOfSchools.Add(s);

            }

            return listOfSchools;   
        }

       
    }
    public class School
    {

        public string Name { get; set; }
        public string Address { get; set; }
        public string ZipCode { get; set; }
        public string City { get; set; }

        public School(string name, string address, string zipCode, string city)
        {
            Name = name;
            Address = address;
            ZipCode = zipCode;
            City = city;
        }
    }
}
