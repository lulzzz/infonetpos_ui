using Infonet.CStoreCommander.DataAccessLayer.IManager;
using Infonet.CStoreCommander.DataAccessLayer.ISerializeManager;
using Infonet.CStoreCommander.DataAccessLayer.Manager.SerializeTasks.FuelPump;
using Infonet.CStoreCommander.EntityLayer.Entities.Checkout;
using Infonet.CStoreCommander.EntityLayer.Entities.FuelPump;
using Infonet.CStoreCommander.EntityLayer.Entities.Sale;
using System.Collections.Generic;
using System.Threading.Tasks;
using Infonet.CStoreCommander.EntityLayer.Entities.Common;

namespace Infonet.CStoreCommander.DataAccessLayer.SerializeManager
{
    public class FuelPumpSerializeManager : SerializeManager, IFuelPumpSerializeManager
    {
        private readonly IFuelPumpRestClient _fuelPumpRestClient;
        private readonly ICacheManager _cacheManager;

        public FuelPumpSerializeManager(IFuelPumpRestClient fuelPumpRestClient,
            ICacheManager cacheManager)
        {
            _fuelPumpRestClient = fuelPumpRestClient;
            _cacheManager = cacheManager;
        }

        public async Task<Sale> AddPrepay(int activePump, string amount, string fuelGrade,
            bool isAmountCash)
        {
            var action = new AddPrepaySerializeAction(_fuelPumpRestClient, _cacheManager,
                activePump, amount, fuelGrade, isAmountCash);

            await PerformTask(action);

            return (Sale)action.ResponseValue;
        }

        public async Task<CheckoutSummary> DeletePrepay(int activePump)
        {
            var action = new DeletePrepaySerializeAction(_fuelPumpRestClient, _cacheManager,
                activePump);

            await PerformTask(action);

            return (CheckoutSummary)action.ResponseValue;
        }

        public async Task<PumpMessage> GetHeadOfficeNotification()
        {
            var action = new GetHeadOfficeNotificationSerializeAction(_fuelPumpRestClient);

            await PerformTask(action);

            return (PumpMessage)action.ResponseValue;
        }

        public async Task<InitializeFuelPump> InitializeFuelPump(bool isInitializingPump, int tillNumber)
        {
            var action = new InitializeFuelPumpSerializeAction(_fuelPumpRestClient,
                isInitializingPump, tillNumber);

            await PerformTask(action);

            return (InitializeFuelPump)action.ResponseValue;
        }

        public async Task<List<string>> LoadGrades(int pumpId, bool switchPrepay, int tillNumber)
        {
            var action = new LoadGradeSerializeAction(_fuelPumpRestClient, pumpId, switchPrepay, tillNumber);

            await PerformTask(action);

            return (List<string>)action.ResponseValue;
        }

        public async Task<bool> ResumeAllPumps()
        {
            var action = new ResumeAllSerializeAction(_fuelPumpRestClient);

            await PerformTask(action);

            return (bool)action.ResponseValue;
        }

        public async Task<bool> StopAllPumps()
        {
            var action = new StopAllSerializeAction(_fuelPumpRestClient);

            await PerformTask(action);

            return (bool)action.ResponseValue;
        }

        public async Task<bool> SwitchPrepay(int activePump, int newPumpId)
        {
            var action = new SwitchPrepaySerializeAction(_fuelPumpRestClient, _cacheManager,
                activePump, newPumpId);

            await PerformTask(action);

            return (bool)action.ResponseValue;
        }

        public async Task<string> UpdateFuelPrice(int option, int counter)
        {
            var action = new UpdateFuelPriceSerializeAction(_fuelPumpRestClient, option, counter);

            await PerformTask(action);

            return (string)action.ResponseValue;
        }

        public async Task<Sale> AddBasket(int activePump, string basketValue)
        {
            var action = new AddBasketSerializeAction(_fuelPumpRestClient, _cacheManager,
                activePump, basketValue);

            await PerformTask(action);

            return (Sale)action.ResponseValue;
        }

        public async Task<GetPumpAction> GetPumpAction(int pumpId, bool isStopPressed, bool isResumePressed)
        {
            var action = new GetPumpActionSerializeAction(_fuelPumpRestClient, pumpId, isStopPressed, isResumePressed);

            await PerformTask(action);

            return (GetPumpAction)action.ResponseValue;
        }

        public async Task<TierLevel> LoadTierLevel()
        {
            var action = new LoadTierLevelSerializeAction(_fuelPumpRestClient);

            await PerformTask(action);

            return (TierLevel)action.ResponseValue;
        }

        public async Task<TierLevel> UpdateTierlevel(List<int> pumpIds, int tierId, int levelId)
        {
            var action = new UpdateTierLevelSerializeAction(_fuelPumpRestClient, pumpIds, tierId, levelId);

            await PerformTask(action);

            return (TierLevel)action.ResponseValue;
        }

        public async Task<List<PropaneGrade>> LoadPropaneGrade()
        {
            var action = new LoadPropaneGradeSerializeAction(_fuelPumpRestClient);

            await PerformTask(action);

            return (List<PropaneGrade>)action.ResponseValue;
        }

        public async Task<List<LoadPumps>> LoadPropanePumps(int gradeId)
        {
            var action = new LoadPropanePumpSerializeAction(_fuelPumpRestClient, gradeId);

            await PerformTask(action);

            return (List<LoadPumps>)action.ResponseValue;
        }


        public async Task<Sale> AddPropane(int gradeId, int pumpId, string propaneValue, bool isAmount)
        {
            var action = new AddPropaneSerializeAction(_fuelPumpRestClient, _cacheManager, gradeId,
                pumpId, propaneValue, isAmount);

            await PerformTask(action);

            return (Sale)action.ResponseValue;
        }

        public async Task<Sale> AddManually(int pumpId, string amount, bool isCashSelected, string grade)
        {
            var action = new AddManuallySerializeAction(_fuelPumpRestClient, _cacheManager, pumpId,
                amount, isCashSelected, grade);

            await PerformTask(action);

            return (Sale)action.ResponseValue;
        }

        public async Task<FuelPrices> LoadPrices(bool grouped)
        {
            var action = new LoadPricesSerializeAction(_fuelPumpRestClient, _cacheManager, grouped);

            await PerformTask(action);

            return (FuelPrices)action.ResponseValue;
        }

        public async Task<bool> ReadTotalizer()
        {
            var action = new ReadTotalizerSerializeAction(_fuelPumpRestClient, _cacheManager);

            await PerformTask(action);

            return (bool)action.ResponseValue;
        }

        public async Task<FuelPrices> SetGroupBasePrice(List<Price> fuelPrices, int row)
        {
            var action = new SetGroupBasePriceSerializeAction(_fuelPumpRestClient, _cacheManager, fuelPrices, row);

            await PerformTask(action);

            return (FuelPrices)action.ResponseValue;
        }

        public async Task<ErrorMessageWithCaption> SaveBasePrices(FuelPrices fuelPrices)
        {
            var action = new SaveBasePricesSerializeAction(_fuelPumpRestClient, _cacheManager, fuelPrices);

            await PerformTask(action);

            return (ErrorMessageWithCaption)action.ResponseValue;
        }

        public async Task<ErrorMessageWithCaption> SaveGroupBasePrices(FuelPrices fuelPrices)
        {
            var action = new SaveGroupBasePricesSerializeAction(_fuelPumpRestClient, _cacheManager, fuelPrices);

            await PerformTask(action);

            return (ErrorMessageWithCaption)action.ResponseValue;
        }

        public async Task<Price> SetBasePrice(Price fuelPrice)
        {
            var action = new SetBasePriceSerializeAction(_fuelPumpRestClient, _cacheManager, fuelPrice);

            await PerformTask(action);

            return (Price)action.ResponseValue;
        }

        public async Task<bool> VerifyBasePrices(FuelPrices fuelPrices)
        {
            var action = new VerifyBasePricesSerializeAction(_fuelPumpRestClient, _cacheManager, fuelPrices);

            await PerformTask(action);

            return (bool)action.ResponseValue;
        }

        public async Task<bool> VerifyGroupBasePrices(FuelPrices fuelPrices)
        {
            var action = new VerifyGroupBasePricesSerializeAction(_fuelPumpRestClient, _cacheManager, fuelPrices);

            await PerformTask(action);

            return (bool)action.ResponseValue;
        }

        public async Task<bool> ClearError()
        {
            var action = new ClearErrorSerializeAction(_fuelPumpRestClient);

            await PerformTask(action);

            return (bool)action.ResponseValue;
        }

        public async Task<string> GetError()
        {
            var action = new GetErrorSerializeAction(_fuelPumpRestClient);

            await PerformTask(action);

            return (string)action.ResponseValue;
        }

        public async Task<UncompletePrepayLoad> UncompletePrepayLoad()
        {
            var action = new UncompletePrepayLoadSerializeAction(_fuelPumpRestClient);

            await PerformTask(action);

            return (UncompletePrepayLoad)action.ResponseValue;
        }

        public async Task<UncompletePrepayChange> UncompletePrepayChange(string finishAmount, string finishPrice,
            string finishQty, int gradeId, string positionId, string prepayAmount, int pumpId, int saleNumber)
        {
            var action = new UncompletePrepayChangeSerializeAction(_fuelPumpRestClient, _cacheManager, finishAmount,
                finishPrice, finishQty, gradeId, positionId, prepayAmount, pumpId, saleNumber);

            await PerformTask(action);

            return (UncompletePrepayChange)action.ResponseValue;
        }

        public async Task<OverPayment> UncompleteOverPayment(int pumpId, string finishAmount,
            string finishQuantity, string finishPrice, string prepayAmount,
            int positionId, int gradeId, int saleNumber)
        {
            var action = new UncompleteOverPaymentSerializeAction(_fuelPumpRestClient, _cacheManager,pumpId, finishAmount,
                finishQuantity, finishPrice, prepayAmount, positionId, gradeId, saleNumber);

            await PerformTask(action);

            return (OverPayment)action.ResponseValue;
        }

        public async Task<Sale> DeleteUncomplete(int pumpId)
        {
            var action = new UncompleteDeleteSerializeAction(_fuelPumpRestClient,_cacheManager, pumpId);

            await PerformTask(action);

            return (Sale)action.ResponseValue;
        }

        public async Task<bool> StopBroadcast()
        {
            var action = new StopBroadcastSerializeAction(_fuelPumpRestClient);

            await PerformTask(action);

            return (bool)action.ResponseValue;
        }

        public async Task<bool> CheckError()
        {
            var action = new CheckErrorSerializeAction(_fuelPumpRestClient);

            await PerformTask(action);

            return (bool)action.ResponseValue;
        }

        public async Task<string> GetFuelVolume(int gradeId, int pumpId, string propaneValue)
        {
            var action = new GetFuelVolumeSerializeAction(_fuelPumpRestClient, _cacheManager, gradeId, pumpId, propaneValue);

            await PerformTask(action);

            return (string)action.ResponseValue;
        }
    }
}
