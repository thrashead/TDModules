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
using Models.ImagesModel;
using Models.TbAdminModel;
using Models.TbKategoriModel;
using Models.TbKaynakModel;
using Models.TbKriterModel;
using TDFramework;
using TDFramework.Common;

namespace TestProjectV3
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Table<Contents> table1 = new Table<Contents>();
            Table<Categories> table2 = new Table<Categories>();

            table1.SelectSettings = new Select(3);
            table1.SelectSettings.OrderColumn = ContentsColumns.CategoryID;
            table1.SelectSettings.OrderBy = OrderBy.DESC;

            table1.WhereList.Add(new Where(ContentsColumns.ID, new int[] { 3, 4, 5 }, Operators.IN));

            Relation<Contents, Categories> rel = new Relation<Contents, Categories>(ContentsColumns.CategoryID, CategoriesColumns.ID);

            //Select Top 3 A.[ContentName] as A_ContentName 
            //From Contents A Right Join Categories B 
            //On A.CategoryID = B.ID 
            //Where (A.[ID] >= 2) 
            //AND (A.[ID] <= 6) 
            //AND (B.[ID] = 1)
            table1.Select<Categories>(table2, rel);

            dataGridView1.DataSource = table1.Data;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Table<Contents> table = new Table<Contents>();

            List<Where> wcList = new List<Where>();
            table.WhereList.Add(new Where(ContentsColumns.ID, 3, new Parantheses() { OpenCount = 1 }));
            table.WhereList.Add(new Where(ContentsColumns.ID, 4, Knots.OR, new Parantheses() { ClosedCount = 1 }));
            table.WhereList.Add(new Where(ContentsColumns.CategoryID, 1));

            //Select * From Contents Where (([ID] = 3) OR ([ID] =4)) AND ([CategoryID] = 1)
            //ResultBox rb = TDHelper<Contents>.Select(wcList);

            List<int> cats = new List<int>();
            cats.Add(8547);
            cats.Add(8548);

            //Select [CategoryID] From Contents Group By CategoryID Having (Count([CategoryID]) = 1) AND (Sum([CategoryID]) = 2)
            List<Having> havingList = new List<Having>();
            havingList.Add(new Having(ContentsColumns.CategoryID, 1, Aggregates.COUNT));
            havingList.Add(new Having(ContentsColumns.CategoryID, 2, Aggregates.SUMMARY));

            table.SelectSettings.Aggregate = new Aggregate(ContentsColumns.CategoryID, havingList);
            table.Columns = ContentsColumns.CategoryID;


            table.Select();

            table = new Table<Contents>();

            //Select Sum(ID) AggColumn, [CategoryID], [ContentName] From Contents Group By CategoryID, ContentName
            List<ContentsColumns> contentsColumns = new List<ContentsColumns>();
            contentsColumns.Add(ContentsColumns.CategoryID);
            contentsColumns.Add(ContentsColumns.ContentName);
            table.Columns = contentsColumns;
            table.SelectSettings.Aggregate = new Aggregate(ContentsColumns.ID, Aggregates.SUMMARY, contentsColumns);
            table.SelectSettings.OrderBy = OrderBy.DESC;
            table.SelectSettings.OrderColumn = ContentsColumns.CategoryID;

            table.Select();

            dataGridView1.DataSource = table.Data;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Table<Contents> table = new Table<Contents>();
            table.Values = new Contents()
            {
                Active = true
            };

            table.Insert();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Table<Kaynak> table = new Table<Kaynak>();
            table.Columns = KaynakColumns.KaynakMetinBlaBla;
            table.Values = new Kaynak()
            {
                KaynakMetinBlaBla = "hedewww2"
            };
            table.WhereList.Add(new Where(KaynakColumns.KaynakMetinBlaBla, "hedewww"));

            table.Update();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Table<Contents> table1 = new Table<Contents>();
            Table<Categories> table2 = new Table<Categories>();

            Aggregate agg = new Aggregate(ContentsColumns.CategoryID, Aggregates.MAXIMUM, ContentsColumns.CategoryID);

            //table1
            table1.SelectSettings.Aggregate = agg;

            table1.WhereList.AddRange(new List<Where>() { new Where(ContentsColumns.CategoryID, 2), new Where(ContentsColumns.CategoryID, 3) });

            table1.Columns = ContentsColumns.CategoryID;

            //table2
            table2.Columns = SelectColumns.NONE;

            table2.SelectSettings = new Select(new Pager(1, 10));

            table2.SelectSettings.Aggregate = new Aggregate(CategoriesColumns.CategoryName);

            table2.SelectSettings.Distinct = true;

            table2.SelectSettings.Top = 3;

            table2.SelectSettings.OrderColumn = CategoriesColumns.CategoryName;

            Relation<Contents, Categories> rel = new Relation<Contents, Categories>(ContentsColumns.CategoryID, CategoriesColumns.ID, JoinTypes.RIGHT);

            //With Pager As (
            //Select Row_Number() Over (Order By A.ID) As 'RowNumber', Sum(A.ID) AggColumn, A.* 
            //From Contents A Right Join Categories B 
            //On A.CategoryID = B.ID 
            //Where (A.[ID] >= 2) AND (A.[ID] <= 6) AND (B.[ID] = 1) 
            //Group By A.ID, A.ContentName, A.CategoryID, A.Active) 
            //Select Top 3 * From Pager Where RowNumber Between 1 AND 10
            table1.Select<Categories>(table2, rel);

            dataGridView1.DataSource = table1.Data;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Table<Contents> table = new Table<Contents>();
            table.SelectSettings.Aggregate = new Aggregate(ContentsColumns.ID, Aggregates.COUNT);
            table.WhereList.Add(new Where(ContentsColumns.ID, 2));

            table.Select();
            dataGridView1.DataSource = table.Data;
        }

        private void button6_Click(object sender, EventArgs e)
        {
            Table<TbKategori> table1 = new Table<TbKategori>();
            Table<Kaynak> table2 = new Table<Kaynak>();
            Table<TbKriter> table3 = new Table<TbKriter>();

            Relation<TbKategori, Kaynak> rel = new Relation<TbKategori, Kaynak>(TbKategoriColumns.KategoriID, KaynakColumns.KaynakKategoriID);
            Relation<Kaynak, TbKriter> rel2 = new Relation<Kaynak, TbKriter>(KaynakColumns.KaynakID, TbKriterColumns.KriterKaynakID);
            List<Relation<TbKategori, TbKriter>> relList = new List<Relation<TbKategori, TbKriter>>() {
             new Relation<TbKategori, TbKriter>(TbKategoriColumns.KategoriID, TbKriterColumns.KriterKaynakID),
             new Relation<TbKategori, TbKriter>(TbKategoriColumns.KategoriID, TbKriterColumns.KriterKaynakID)
            };

            table2.Columns = SelectColumns.NONE;
            table3.WhereList.Add(new Where(TbKriterColumns.KriterAktif, true));

            table3.SelectSettings.Distinct = true;
            table2.SelectSettings.Top = 4;

            table1.SelectSettings.Pager = new Pager(1, 10);

            table1.Select<Kaynak, TbKriter>(table2, table3, rel, rel2);

            dataGridView1.DataSource = table1.Data;
        }

        private void button7_Click(object sender, EventArgs e)
        {

            //Insert Into Categories([CategoryName],[Active]) Values(@CategoryName,@Active) 
            Table<Categories> tableCat = new Table<Categories>();
            tableCat.Values = new Categories()
            {
                CategoryName = "Category Name",
                Active = true
            };
            tableCat.Insert();

            //Insert Into Categories([CategoryName],[Active]) Values(@CategoryName,@Active) 
            //Select SCOPE_IDENTITY()
            tableCat = new Table<Categories>();
            tableCat.Values = new Categories()
            {
                CategoryName = "Category Name",
                Active = true
            };
            tableCat.Insert(true);

            //dataGridView1.DataSource = table.Data;
        }
    }
}
