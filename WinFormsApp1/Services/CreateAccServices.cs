using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WinFormsApp1.DataX;
using WinFormsApp1.entity;
using WinFormsApp1.IServices;
using Nethereum.Signer;
using Nethereum.Model;
using Nethereum.HdWallet;
using NBitcoin;
using System.Windows.Forms;

namespace WinFormsApp1.Services
{
    public class CreateAccServices : ICreateAccount
    {
        private readonly appDbcontext dbcontext;

        public CreateAccServices()
        {
            dbcontext = new appDbcontext();
        }

        public async Task CreateAcc(int sl)
        {
            var accounts = new List<ListAcc>();

            using (var trans = dbcontext.Database.BeginTransaction())
            {
                try
                {
                    for (int i = 0; i < sl; ++i)
                    {
                        var ethECKey = EthECKey.GenerateKey();
                        var address = ethECKey.GetPublicAddress();
                        var wallet = new Wallet(Wordlist.English, WordCount.Twelve);
                        var mnemonics = string.Join(" ", wallet.Words); // Tạo Mnemonic ngẫu nhiên
                        var listAcc = new ListAcc
                        {
                            Address = address,
                            PrivateKey = ethECKey.GetPrivateKey(),
                            mnemonic = mnemonics
                        };

                        accounts.Add(listAcc);
                        dbcontext.listAccs.Add(listAcc);
                    }

                    await dbcontext.SaveChangesAsync();
                    trans.Commit();

                    // Hiển thị thông báo khi thành công
                    MessageBox.Show("Thêm tài khoản thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    trans.Rollback();

                    // Hiển thị thông báo khi thất bại và in chi tiết lỗi (điều này có thể được thay đổi tùy thuộc vào cách bạn muốn xử lý lỗi)
                    MessageBox.Show($"Thêm tài khoản thất bại. Lỗi: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }
}
