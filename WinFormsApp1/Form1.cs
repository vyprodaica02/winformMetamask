using WinFormsApp1.IServices;
using WinFormsApp1.Services;

namespace WinFormsApp1
{
    public partial class Form1 : Form
    {
        private int slAcc;
        private readonly ICreateAccount createAccount;
        public Form1()
        {
            InitializeComponent();
            if (int.TryParse(SLcreate.Text, out int parsedValue))
            {
                slAcc = parsedValue;
            }
            else
            {
                slAcc = 1;
            }
            createAccount = new CreateAccServices();
        }
        private void btnCreate_Click(object sender, EventArgs e)
        {
            createAccount.CreateAcc(slAcc);
        }
    }
}
