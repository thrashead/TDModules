using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace TDFactory
{
    public partial class CreateRelaion : Form
    {
        public ConnectionInfo ConnectionInfo { get; set; }
        public ColumnInfo ForeignInfo { get; set; }
        public string TableName { get; set; }
        public string ColumnName { get; set; }
        bool duzenle = false;

        ColumnInfo foreignColumn = new ColumnInfo();
        ColumnInfo primaryColumn = new ColumnInfo();
        List<ColumnInfo> primaryColumns = new List<ColumnInfo>();

        public CreateRelaion()
        {
            InitializeComponent();
        }

        public CreateRelaion(bool _duzenle)
        {
            duzenle = _duzenle;
            InitializeComponent();
        }

        private void CreateRelaion_Load(object sender, EventArgs e)
        {
            foreach (Relation item in TDFactory.Relations.Where(a => a.ForeignTable == TableName).ToList())
            {
                listBox1.Items.Add(item.RelationName);
            }

            if (ForeignInfo != null)
            {
                foreignColumn = ForeignInfo;

                textBox1.Text = TableName;
                textBox2.Text = foreignColumn.ColumnName;
                textBox3.Text = TableName + "_";
                label1.Text = foreignColumn.DataType;

                List<string> temptablenames = Helper.TableNames(ConnectionInfo);

                foreach (string item in temptablenames.Where(a => a != TableName).ToList())
                {
                    List<ColumnInfo> tempcolinfolist = Helper.GetColumnsInfo(ConnectionInfo, item.ToString()).Where(a => a.DataType == foreignColumn.DataType && a.IsPrimaryKey).ToList();

                    if (tempcolinfolist.Count > 0)
                    {
                        comboBox1.Items.Add(item);
                    }
                }

                if (comboBox1.Items.Count <= 0)
                {
                    MessageBox.Show("İlgili veritabanında foreign key oluşturmak istediğiniz alana karşılık tanımlayabileceğiniz bir alan herhangi bir tabloda yer almamaktadır.");
                    this.Close();
                }

                comboBox1.SelectedIndex = -1;
            }
            else
            {
                this.Close();
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox1.SelectedIndex >= 0)
            {
                primaryColumns = Helper.GetColumnsInfo(ConnectionInfo, comboBox1.Text).Where(a => a.DataType == foreignColumn.DataType && a.IsPrimaryKey).ToList();

                textBox3.Text = TableName + "_" + comboBox1.Text;

                comboBox2.Items.Clear();

                foreach (ColumnInfo item in primaryColumns)
                {
                    comboBox2.Items.Add(item.ColumnName);
                }

                comboBox2.SelectedIndex = 0;

                primaryColumn = primaryColumns.Where(a => a.ColumnName == comboBox2.Text).FirstOrDefault();
                label2.Text = primaryColumn.DataType;
            }
        }

        private void comboBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }

        private void comboBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }

        private void btnKaydet_Click(object sender, EventArgs e)
        {
            if (comboBox1.SelectedIndex >= 0 && comboBox2.SelectedIndex >= 0)
            {
                if (primaryColumn.IsPrimaryKey)
                {
                    if (TDFactory.Relations.Where(a => a.RelationName == "FK_" + textBox3.Text).ToList().Count <= 0)
                    {
                        List<Relation> listRel = TDFactory.Relations.Where(a => a.ForeignTable == foreignColumn.TableName && a.ForeignColumn == foreignColumn.ColumnName && a.PrimaryTable == primaryColumn.TableName && a.PrimaryColumn == primaryColumn.ColumnName).ToList();

                        if (listRel.Count <= 0)
                        {
                            TDFactory.Relations.Add(new Relation() { RelationName = "FK_" + textBox3.Text, ForeignTable = foreignColumn.TableName, ForeignColumn = foreignColumn.ColumnName, PrimaryTable = primaryColumn.TableName, PrimaryColumn = primaryColumn.ColumnName });
                            listBox1.Items.Add("FK_" + textBox3.Text);
                        }
                        else
                        {
                            MessageBox.Show("Aynı kolonlar için " + listRel.FirstOrDefault().RelationName + " isimli bir Relation daha önce zaten oluşturulmuş.\nLütfen listeden kontrol ediniz.");
                        }
                    }
                    else
                    {
                        MessageBox.Show("Aynı isimde bir Relation daha önce zaten oluşturulmuş.\nLütfen Yeni bir isim veriniz.");
                    }
                }
                else
                {
                    MessageBox.Show("Foreign Key oluşturulacak kolona karşılık gelen kolon Primary key değil.\nLütfen Primary Key olan bir kolon seçiniz.");
                }
            }
            else
            {
                MessageBox.Show("Lütfen önce Foreign Key oluşturulacak kolona karşılık gelen Primary key'i seçiniz.");
            }
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            primaryColumn = primaryColumns.Where(a => a.ColumnName == comboBox2.Text).FirstOrDefault();
            label2.Text = primaryColumn.DataType;
        }

        private void btnIptal_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnClearRelations_Click(object sender, EventArgs e)
        {
            TDFactory.Relations.Clear();
            listBox1.Items.Clear();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (listBox1.SelectedIndex >= 0)
            {
                if (TDFactory.Relations.Where(a => a.RelationName == listBox1.SelectedItem.ToString()).ToList().Count > 0)
                {
                    TDFactory.Relations.Remove(TDFactory.Relations.Where(a => a.RelationName == listBox1.SelectedItem.ToString()).FirstOrDefault());
                }

                listBox1.Items.Remove(listBox1.SelectedItem);
            }
        }

        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }

        private void textBox1_MouseMove(object sender, MouseEventArgs e)
        {
            textBox1.Cursor = Cursors.Default;
        }

        private void textBox2_MouseMove(object sender, MouseEventArgs e)
        {
            textBox2.Cursor = Cursors.Default;
        }

        private void textBox1_Enter(object sender, EventArgs e)
        {
            this.ActiveControl = null;
        }

        private void textBox2_Enter(object sender, EventArgs e)
        {
            this.ActiveControl = null;
        }
    }
}
