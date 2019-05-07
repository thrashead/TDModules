using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Models.CategoriesModel;
using Models.ContentsModel;
using TDFramework;
using TDFramework.Common;

namespace TestProjectV2
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //Insert Into Categories([CategoryName],[Active]) Values(@CategoryName,@Active) 
            Categories cat = new Categories()
            {
                CategoryName = "Category Name",
                Active = true
            };
            ResultBox rb = TDHelper<Categories>.Insert(cat);

            //Insert Into Categories([CategoryName],[Active]) Values(@CategoryName,@Active) 
            //Select SCOPE_IDENTITY()
            Categories cat2 = new Categories()
            {
                CategoryName = "Category Name",
                Active = true
            };
            ResultBox rb2 = TDHelper<Categories>.Insert(cat2, true);


            dataGridView1.DataSource = rb.Data;
        }
    }
}
