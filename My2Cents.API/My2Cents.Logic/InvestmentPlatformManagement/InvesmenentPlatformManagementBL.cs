using My2Cents.DataInfrastructure;
using My2Cents.DatabaseManagement;
using Microsoft.Data.SqlClient;
using My2Cents.DataInfrastructure.Models;

namespace My2Cents.Logic
{
    public class InvesmenentPlatformManagementBL : IInvesmenentPlatformManagementBL
    {
        private IInvesmenentPlatformManagementDL _repo;
        public InvesmenentPlatformManagementBL(IInvesmenentPlatformManagementDL p_repo)
        {
            _repo = p_repo;
        }

        public async Task<CryptoOrderHistoryDto> PlaceOrderCrypto(int _userID, int _cryptoID, decimal amount)
        {
            try
            {
               return await _repo.PlaceOrderCrypto(_userID, _cryptoID, amount);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public async Task<CryptoOrderHistoryDto> PlaceOrderCryptoFiat(int p_userID, int p_cryptoID, decimal amount)
        {
            try
            {
                return await _repo.PlaceOrderCryptoFiat(p_userID, p_cryptoID, amount);
            }
            catch (System.Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public async Task<StockOrderHistoryDto> PlaceOrderStock(int p_userID, int p_stockID, decimal amount)
        {
            try
            {
                return await _repo.PlaceOrderStock(p_userID, p_stockID, amount);
            }
            catch (System.Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public async Task<StockOrderHistoryDto> PlaceOrderStockFiat(int p_userID, int p_stockID, decimal amount)
        {
            try
            {
                return await _repo.PlaceOrderStockFiat(p_userID, p_stockID, amount);
            }
            catch (System.Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public async Task<CryptoOrderHistoryDto> SellCrypto(int _userID, int _cryptoID, decimal amount)
        {
            try
            {
                return await _repo.SellCrypto(_userID, _cryptoID, amount);
            }
            catch (System.Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public async Task<CryptoOrderHistoryDto> SellCryptoFiat(int p_userID, int p_cryptoID, decimal amount)
        {
            try
            {
                return await _repo.SellCryptoFiat(p_userID, p_cryptoID, amount);
            }
            catch (System.Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public async Task<StockOrderHistoryDto> SellStock(int p_userID, int p_stockID, decimal amount)
        {
            try
            {
                return await _repo.SellStock(p_userID, p_stockID, amount);
            }
            catch (System.Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public async Task<StockOrderHistoryDto> SellStockFiat(int p_userID, int p_stockID, decimal amount)
        {
            try
            {
                return await _repo.SellStockFiat(p_userID, p_stockID, amount);
            }
            catch (System.Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public async Task<List<CryptoDto>> UpdateCryptosData(){
            return await _repo.UpdateCryptosData();
        }

        public async Task<List<StockDto>> UpdateStocksData()
        {
            return await _repo.UpdateStocksData();
        }

        // public StockOrderHistory PlaceOrderStock(StockAsset _asset, StockOrderHistory _sOrderHis, Account _balance, int _userID, int _stockID, decimal _stockPrice, decimal amount)
        // {
        //    try
        //    {
        //         foreach (var item in ViewAssets(_userID)) //Method not actually there need to discuss with Vijhan team
        //         {
        //             if (item.StockName == _stockname)
        //             {
        //                 _repo.SubtractFromAccount(_balance);
        //                 _repo.BuyExistingStock(_asset);
        //                 return _repo.AddStockOrderHistory(_sOrderHis);
        //             }
        //         }
        //    }
        //    catch (SqlException)
        //    {

        //        return null;
        //    }
        // }

        // public CryptoOrderHistory SellOrderCrypto(CryptoAsset _asset, CryptoOrderHistory _cOrderHis, Account _balance, int _userID, int _cryptoID, decimal _cryptoPrice, decimal amount)
        // {
        //     throw new NotImplementedException();
        // }

        // public StockOrderHistory SellOrderStock(StockAsset _asset, StockOrderHistory _sOrderHis, Account _balance, int _userID, int _stockID, decimal _stockPrice, decimal amount)
        // {
        //     throw new NotImplementedException();
        // }
    }
}