using Infonet.CStoreCommander.BussinessLayer.IBussinessLogic;
using Infonet.CStoreCommander.DataAccessLayer.ISerializeManager;
using Infonet.CStoreCommander.EntityLayer.Entities.Cash;
using Infonet.CStoreCommander.EntityLayer.Entities.Checkout;
using Infonet.CStoreCommander.EntityLayer.Entities.FuelPump;
using Infonet.CStoreCommander.EntityLayer.Entities.Sale;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Infonet.CStoreCommander.EntityLayer.Entities.Common;

namespace Infonet.CStoreCommander.BussinessLayer.BussinessLogic
{
    public class FuelPumpBusinessLogic : IFuelPumpBusinessLogic
    {
        private readonly IFuelPumpSerializeManager _fuelPumpSerializeManager;
        private readonly IReportsBussinessLogic _reportsBussinessLogic;
        private readonly ICacheBusinessLogic _cacheBusinessLogic;

        public FuelPumpBusinessLogic(IFuelPumpSerializeManager fuelPumpSerializeManager,
            IReportsBussinessLogic reportsBussinessLogic,
            ICacheBusinessLogic cacheBusinessLogic)
        {
            _fuelPumpSerializeManager = fuelPumpSerializeManager;
            _reportsBussinessLogic = reportsBussinessLogic;
            _cacheBusinessLogic = cacheBusinessLogic;
        }

        public async Task<Sale> AddBasket(int activePump, string basketValue)
        {
            return await _fuelPumpSerializeManager.AddBasket(activePump, basketValue);
        }

        public async Task<Sale> AddManually(int pumpId, string amount, bool isCashSelected, string grade)
        {
            return await _fuelPumpSerializeManager.AddManually(pumpId, amount, isCashSelected, grade);
        }

        public async Task<Sale> AddPrepay(int activePump, string amount, string fuelGrade,
            bool isAmountCash)
        {
            return await _fuelPumpSerializeManager.AddPrepay(activePump, amount,
                fuelGrade, isAmountCash);
        }

        public async Task<Sale> AddPropane(int gradeId, int pumpId, string propaneValue, bool isAmount)
        {
            return await _fuelPumpSerializeManager.AddPropane(gradeId, pumpId, propaneValue, isAmount);
        }

        public async Task<bool> CheckError()
        {
            return await _fuelPumpSerializeManager.CheckError();
        }

        public async Task<bool> ClearError()
        {
            return await _fuelPumpSerializeManager.ClearError();
        }

        public async Task<CheckoutSummary> DeletePrepay(int activePump)
        {
            var result = await _fuelPumpSerializeManager.DeletePrepay(activePump);
            if (result != null)
            {
                result.IsDeletePrepay = true;
            }
            return result;
        }

        public async Task<Sale> DeleteUncomplete(int pumpId)
        {
            return await _fuelPumpSerializeManager.DeleteUncomplete(pumpId);
        }

        public async Task<string> GetError()
        {
            return await _fuelPumpSerializeManager.GetError();
        }

        public async Task<string> GetFuelVolume(int gradeId, int pumpId, string propaneValue)
        {
            return await _fuelPumpSerializeManager.GetFuelVolume(gradeId, pumpId, propaneValue);
        }

        public async Task<PumpMessage> GetHeadOfficeMessage()
        {
            return await _fuelPumpSerializeManager.GetHeadOfficeNotification();
        }

        public async Task<List<CashButtons>> GetManualCashButtons()
        {
            var cashValues = new List<int> { 5, 10, 15, 20 };

            return (from c in cashValues
                    select new CashButtons
                    {
                        Button = c.ToString(),
                        Value = c
                    }).ToList();
        }

        public async Task<List<CashButtons>> GetPrepayCashButtons()
        {
            var cashValues = new List<int> { 5, 10, 15, 20 };

            return (from c in cashValues
                    select new CashButtons
                    {
                        Button = c.ToString(),
                        Value = c
                    }).ToList();
        }

        public async Task<GetPumpAction> GetPumpAction(int pumpId, bool isStopPressed, bool isResumePressed)
        {
            return await _fuelPumpSerializeManager.GetPumpAction(pumpId, isStopPressed, isResumePressed);
        }

        public async Task<InitializeFuelPump> InitializeFuelPump(bool isInitializingPump, int tillNumber)
        {
            return await _fuelPumpSerializeManager.InitializeFuelPump(isInitializingPump, tillNumber);
        }

        public async Task<List<string>> LoadGrades(int pumpId, bool switchPrepay, int tillNumber)
        {
            return await _fuelPumpSerializeManager.LoadGrades(pumpId, switchPrepay, tillNumber);
        }

        public async Task<FuelPrices> LoadPrices(bool grouped)
        {
            var result = await _fuelPumpSerializeManager.LoadPrices(grouped);
            if (result?.Report != null)
            {
                await _reportsBussinessLogic.SaveReport(result.Report);
            }
            return result;
        }

        public async Task<List<PropaneGrade>> LoadPropaneGrade()
        {
            return await _fuelPumpSerializeManager.LoadPropaneGrade();
        }

        public async Task<List<LoadPumps>> LoadPropanePumps(int gradeId)
        {
            return await _fuelPumpSerializeManager.LoadPropanePumps(gradeId);
        }

        public async Task<TierLevel> LoadTierlevel()
        {
            return await _fuelPumpSerializeManager.LoadTierLevel();
        }

        public async Task<bool> ReadTotalizer()
        {
            return await _fuelPumpSerializeManager.ReadTotalizer();
        }

        public async Task<bool> ResumeAllPumps()
        {
            return await _fuelPumpSerializeManager.ResumeAllPumps();
        }

        public async Task<ErrorMessageWithCaption> SaveBasePrices(FuelPrices fuelPrices)
        {
            try
            {
                _cacheBusinessLogic.AreFuelPricesSaved = false;
                return await _fuelPumpSerializeManager.SaveBasePrices(fuelPrices);
            }
            finally
            {
                _cacheBusinessLogic.AreFuelPricesSaved = true;
            }
        }

        public async Task<ErrorMessageWithCaption> SaveGroupBasePrices(FuelPrices fuelPrices)
        {
            try
            {
                _cacheBusinessLogic.AreFuelPricesSaved = false;
                return await _fuelPumpSerializeManager.SaveGroupBasePrices(fuelPrices);
            }
            finally
            {
                _cacheBusinessLogic.AreFuelPricesSaved = true;
            }
        }

        public async Task<Price> SetBasePrice(Price fuelPrice)
        {
            return await _fuelPumpSerializeManager.SetBasePrice(fuelPrice);
        }

        public async Task<FuelPrices> SetGroupBasePrice(List<Price> fuelPrices, int row)
        {
            var result = await _fuelPumpSerializeManager.SetGroupBasePrice(fuelPrices, row);
            if (result?.Report != null)
            {
                await _reportsBussinessLogic.SaveReport(result.Report);
            }
            return result;
        }

        public async Task<bool> StopAllPumps()
        {
            return await _fuelPumpSerializeManager.StopAllPumps();
        }

        public async Task<bool> StopBroadcast()
        {
            return await _fuelPumpSerializeManager.StopBroadcast();
        }

        public async Task<bool> SwitchPrepay(int activePump, int newPumpId)
        {
            return await _fuelPumpSerializeManager.SwitchPrepay(activePump, newPumpId);
        }

        public async Task<OverPayment> UncompleteOverPayment(int pumpId, string finishAmount,
            string finishQuantity, string finishPrice, string prepayAmount, int positionId,
            int gradeId, int saleNumber)
        {
            var response = await _fuelPumpSerializeManager.UncompleteOverPayment(pumpId, finishAmount,
             finishQuantity, finishPrice, prepayAmount, positionId, gradeId, saleNumber);

            await _reportsBussinessLogic.SaveReport(response.TaxExemptReceipt);

            return response;
        }

        public async Task<UncompletePrepayChange> UncompletePrepayChange(string finishAmount,
            string finishPrice, string finishQty, int gradeId, string positionId,
            string prepayAmount, int pumpId, int saleNumber)
        {
            var response = await _fuelPumpSerializeManager.UncompletePrepayChange(finishAmount,
             finishPrice, finishQty, gradeId, positionId, prepayAmount, pumpId, saleNumber);

            await _reportsBussinessLogic.SaveReport(response.TaxExemptReceipt);

            return response;
        }

        public async Task<UncompletePrepayLoad> UncompletePrepayLoad()
        {
            return await _fuelPumpSerializeManager.UncompletePrepayLoad();
        }

        public async Task<string> UpdateFuelPrice(int option, int counter)
        {
            return await _fuelPumpSerializeManager.UpdateFuelPrice(option, counter);
        }

        public async Task<TierLevel> UpdateTierlevel(List<int> pumpIds, int tierId, int levelId)
        {
            return await _fuelPumpSerializeManager.UpdateTierlevel(pumpIds, tierId, levelId);
        }

        public async Task<bool> VerifyBasePrices(FuelPrices fuelPrices)
        {
            return await _fuelPumpSerializeManager.VerifyBasePrices(fuelPrices);
        }

        public async Task<bool> VerifyGroupBasePrices(FuelPrices fuelPrices)
        {
            return await _fuelPumpSerializeManager.VerifyGroupBasePrices(fuelPrices);
        }
    }
}
