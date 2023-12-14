using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using WinFormsApp1.DataX;
using WinFormsApp1.entity;
using WinFormsApp1.IServices;
using WinFormsApp1.Services;

namespace WinFormsApp1
{
    public partial class Form1 : Form
    {
        private int slAcc = 1;
        private readonly ICreateAccount createAccount;
        private readonly appDbcontext dbcontext;

        public List<ListAcc> ListAccounts { get; set; }

        public Form1()
        {
            InitializeComponent();
            SLcreate.Text = slAcc.ToString();
            createAccount = new CreateAccServices();
            dbcontext = new appDbcontext();
            ListAccounts = new List<ListAcc>();
            SetupDataGridView();
            RefreshDataGridView();
        }

        private void SetupDataGridView()
        {
            // Tạo cột cho DataGridView
            dataGridView1.AutoGenerateColumns = false;

            // Thêm cột "Địa chỉ"
            var addressColumn = new DataGridViewTextBoxColumn
            {
                HeaderText = "Địa chỉ",
                DataPropertyName = "Address"
            };
            dataGridView1.Columns.Add(addressColumn);

            // Thêm cột "Khóa riêng"
            var privateKeyColumn = new DataGridViewTextBoxColumn
            {
                HeaderText = "Khóa riêng",
                DataPropertyName = "PrivateKey"
            };
            dataGridView1.Columns.Add(privateKeyColumn);

            // Thêm cột "Mnemonic"
            var mnemonicColumn = new DataGridViewTextBoxColumn
            {
                HeaderText = "Mnemonic",
                DataPropertyName = "mnemonic"
            };
            dataGridView1.Columns.Add(mnemonicColumn);
        }

        private void SetColumnHeight(DataGridViewColumn column, int height)
        {
            column.DefaultCellStyle.Font = new Font(dataGridView1.Font.FontFamily, height);
        }
        private void RefreshDataGridView()
        {
            ListAccounts = dbcontext.listAccs.ToList();
            dataGridView1.DataSource = null;
            dataGridView1.DataSource = ListAccounts;
        }

        private void btnCreate_Click(object sender, EventArgs e)
        {
            createAccount.CreateAcc(slAcc);
            RefreshDataGridView();
        }

        private void SLcreate_TextChanged(object sender, EventArgs e)
        {
            if (int.TryParse(SLcreate.Text, out int parsedValue))
            {
                slAcc = parsedValue;
            }
            else
            {
                slAcc = 1;
            }
        }
    }
}
