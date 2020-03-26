using Infonet.CStoreCommander.EntityLayer.Entities.Cash;
using Infonet.CStoreCommander.EntityLayer.Entities.Checkout;
using Infonet.CStoreCommander.EntityLayer.Entities.Common;
using Infonet.CStoreCommander.EntityLayer.Entities.FuelPump;
using Infonet.CStoreCommander.EntityLayer.Entities.Sale;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Infonet.CStoreCommander.BussinessLayer.IBussinessLogic
{
    public interface IFuelPumpBusinessLogic
    {
        Task<InitializeFuelPump> InitializeFuelPump(bool isInitializingPump, int tillNumber);

        Task<PumpMessage> GetHeadOfficeMessage();

        Task<List<string>> LoadGrades(int pumpId, bool switchPrepay, int tillNumber);

        Task<bool> ResumeAllPumps();

        Task<bool> StopAllPumps();

        Task<Sale> AddPrepay(int activePump, string amount, string fuelGrade, bool isAmountCash);

        Task<CheckoutSummary> DeletePrepay(int activePump);

        Task<bool> SwitchPrepay(int activePump, int newPumpId);

        Task<string> UpdateFuelPrice(int option, int counter);

        Task<Sale> AddBasket(int activePump, string basketValue);

        Task<GetPumpAction> GetPumpAction(int pumpId, bool isStopPressed, bool isResumePressed);

        Task<TierLevel> LoadTierlevel();

        Task<TierLevel> UpdateTierlevel(List<int> pumpIds, int tierId, int levelId);

        Task<List<PropaneGrade>> LoadPropaneGrade();

        Task<List<LoadPumps>> LoadPropanePumps(int gradeId);

        Task<Sale> AddPropane(int gradeId, int pumpId, string propaneValue,
            bool isAmount);

        Task<Sale> AddManually(int pumpId, string amount, bool isCashSelected, string grade);

        Task<FuelPrices> LoadPrices(bool grouped);

        Task<bool> ReadTotalizer();

        Task<FuelPrices> SetGroupBasePrice(List<Price> fuelPrices, int row);

        Task<bool> VerifyGroupBasePrices(FuelPrices fuelPrices);

        Task<ErrorMessageWithCaption> SaveGroupBasePrices(FuelPrices fuelPrices);

        Task<Price> SetBasePrice(Price fuelPrice);

        Task<bool> VerifyBasePrices(FuelPrices fuelPrices);

        Task<ErrorMessageWithCaption> SaveBasePrices(FuelPrices fuelPrices);

        Task<bool> ClearError();

        Task<string> GetError();

        Task<UncompletePrepayLoad> UncompletePrepayLoad();

        Task<UncompletePrepayChange> UncompletePrepayChange(string finishAmount, string finishPrice,
             string finishQty, int gradeId, string positionId, string prepayAmount,
             int pumpId, int saleNumber);

        Task<OverPayment> UncompleteOverPayment(int pumpId, string finishAmount,
            string finishQuantity, string finishPrice, string prepayAmount,
            int positionId, int gradeId, int saleNumber);

        Task<Sale> DeleteUncomplete(int pumpId);

        Task<List<CashButtons>> GetManualCashButtons();

        Task<List<CashButtons>> GetPrepayCashButtons();

        Task<bool> StopBroadcast();

        Task<string> GetFuelVolume(int gradeId, int pumpId, string propaneValue);

        Task<bool> CheckError();
    }
}
