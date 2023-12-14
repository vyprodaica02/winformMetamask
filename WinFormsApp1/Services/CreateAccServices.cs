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

namespace WinFormsApp1.Services
{
    public class CreateAccServices : ICreateAccount
    {
        private readonly appDbcontext dbcontext;
        public CreateAccServices()
        {
            dbcontext = new appDbcontext();
        }
        public async Task CreateAcc( int sl)
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
                }
                catch (Exception )
                {
                    trans.Rollback();
                }
            }
        }
    }
}
