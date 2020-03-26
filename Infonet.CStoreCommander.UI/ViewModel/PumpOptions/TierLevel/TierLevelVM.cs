using Infonet.CStoreCommander.BussinessLayer.IBussinessLogic;
using Infonet.CStoreCommander.UI.Model.FuelPump;
using System.Collections.Generic;
using System.Linq;
using Infonet.CStoreCommander.EntityLayer.Entities.FuelPump;
using System.Collections.ObjectModel;
using GalaSoft.MvvmLight.Command;
using Infonet.CStoreCommander.UI.Messages;
using Infonet.CStoreCommander.UI.Service;

namespace Infonet.CStoreCommander.UI.ViewModel.TierLevelVM
{
    public class TierLevelVM : VMBase
    {
        private TierLevelModel _tierlevelModel;
        private readonly IFuelPumpBusinessLogic _fuelPumpBusinessLogic;
        private List<int> _selectedTierLevel;
        private bool _isSetButtonEnable;
        private ObservableCollection<PumpTierLevelModel> _selectedTierList;
        private PumpTierLevelModel _pumpTierModelToAdd;

        public PumpTierLevelModel PumpTierModelToAdd
        {
            get { return _pumpTierModelToAdd; }
            set
            {
                _pumpTierModelToAdd = value;
                RaisePropertyChanged(nameof(PumpTierModelToAdd));
            }
        }


        public ObservableCollection<PumpTierLevelModel> SelectedTierList
        {
            get { return _selectedTierList; }
            set
            {
                if (_selectedTierList != value)
                {
                    _selectedTierList = value;

                    if (_selectedTierList?.Count == 1)
                    {
                        IsSetButtonEnable = true;
                        ResetCheckBoxes();
                        SetCheckBoxesOfSelectedPump();
                    }
                    else if (_selectedTierList?.Count > 0)
                    {
                        IsSetButtonEnable = true;
                        ResetCheckBoxes();
                    }
                    else
                    {
                        ResetCheckBoxes();
                        IsSetButtonEnable = false;
                    }
                    RaisePropertyChanged(nameof(SelectedTierList));
                }
            }
        }

        private void SetCheckBoxesOfSelectedPump()
        {
            if (SelectedTierList.FirstOrDefault() == null)
            {
                return;
            }

            if (SelectedTierList.FirstOrDefault() == null)
            {
                return;
            }
            var tierToBeChecked = TierlevelModel.Tiers.FirstOrDefault(x => x.TierId == SelectedTierList.FirstOrDefault().TierId);
            var levelToBeChecked = TierlevelModel.Levels.FirstOrDefault(x => x.LevelId == SelectedTierList.FirstOrDefault().LevelId);

            if (levelToBeChecked != null)
            {
                levelToBeChecked.IsChecked = true;
            }

            if (tierToBeChecked != null)
            {
                tierToBeChecked.IsChecked = true;
            }
        }

        public bool IsSetButtonEnable
        {
            get { return _isSetButtonEnable; }
            set
            {
                if (_isSetButtonEnable != value)
                {
                    _isSetButtonEnable = value;
                    RaisePropertyChanged(nameof(IsSetButtonEnable));
                }
            }
        }

        public RelayCommand UpdateTierLevelCommand { get; set; }
        public RelayCommand<object> TierCheckedCommand { get; set; }
        public RelayCommand<object> LevelCheckedCommand { get; set; }
        public RelayCommand BackPageCommand { get; set; }

        public TierLevelModel TierlevelModel
        {
            get { return _tierlevelModel; }
            set
            {
                if (value != _tierlevelModel)
                {
                    _tierlevelModel = value;
                    RaisePropertyChanged(nameof(TierlevelModel));
                }
            }
        }


        private void LoadTierLevel()
        {
            PerformAction(async () =>
            {
                var response = await _fuelPumpBusinessLogic.LoadTierlevel();
                MapTierLevels(response);
            });
        }

        public TierLevelVM(IFuelPumpBusinessLogic fuelPumpBusinessLogic)
        {
            _fuelPumpBusinessLogic = fuelPumpBusinessLogic;
            InitalizeCommands();
        }

        private void ResetCheckBoxes()
        {
            foreach (var tier in TierlevelModel.Tiers)
            {
                tier.IsChecked = false;
            }

            foreach (var level in TierlevelModel.Levels)
            {
                level.IsChecked = false;
            }
        }

        private void MapTierLevels(TierLevel response)
        {
            var tempLevels = (from t in response.Levels
                              select new LevelModel
                              {
                                  LevelId = t.LevelId,
                                  LevelName = t.LevelName
                              }).ToList();

            var tempTier = (from t in response.Tiers
                            select new TierModel
                            {
                                TierId = t.TierId,
                                TierName = t.TierName
                            }).ToList();

            var tempPumpTierLevel = (from p in response.PumpTierLevels
                                     select new PumpTierLevelModel
                                     {
                                         LevelId = p.LevelId,
                                         TierName = p.TierName,
                                         TierId = p.TierId,
                                         LevelName = p.LevelName,
                                         PumpId = p.PumpId
                                     }).ToList();

            TierlevelModel = new TierLevelModel
            {
                PageCaption = response.PageCaption,
                Levels = new ObservableCollection<LevelModel>(tempLevels),
                PumpTierLevels = new ObservableCollection<PumpTierLevelModel>(tempPumpTierLevel),
                Tiers = new ObservableCollection<TierModel>(tempTier)
            };

        }

        private void InitalizeCommands()
        {
            TierCheckedCommand = new RelayCommand<object>(TierLevelChecked);
            UpdateTierLevelCommand = new RelayCommand(UpdateTierLevel);
            LevelCheckedCommand = new RelayCommand<object>(LevelChecked);
            BackPageCommand = new RelayCommand(BackPage);
        }

        private void BackPage()
        {
            if (CacheBusinessLogic.IsFuelOnlySystem)
            {
                MessengerInstance.Send<FuelOnlySystemMessage>(new FuelOnlySystemMessage());
            }
            NavigateService.Instance.NavigateToHome();
        }

        private void LevelChecked(dynamic levelId)
        {
            if (TierlevelModel.Levels?.Count > 0)
            {
                foreach (var level in TierlevelModel.Levels)
                {
                    if (level.LevelId != levelId)
                    {
                        level.IsChecked = false;
                    }
                }
            }
        }

        private void TierLevelChecked(dynamic tierId)
        {
            if (TierlevelModel.Tiers?.Count > 0)
            {
                foreach (var tier in TierlevelModel.Tiers)
                {
                    if (tier.TierId != tierId)
                    {
                        tier.IsChecked = false;
                    }
                }
            }
        }

        private void UpdateTierLevel()
        {
            PerformAction(async () =>
            {
                _selectedTierLevel = (from s in SelectedTierList
                                      select s.PumpId).ToList();


                var tierId = (from t in TierlevelModel.Tiers
                              where t.IsChecked == true
                              select t.TierId).FirstOrDefault();

                var levelId = (from p in TierlevelModel.Levels
                               where p.IsChecked == true
                               select p.LevelId).FirstOrDefault();
                //TODO: Mohit 
                TierlevelModel.PageCaption = "Setting in Progress";
                var response = await _fuelPumpBusinessLogic.UpdateTierlevel(_selectedTierLevel, tierId, levelId);
                MapTierLevels(response);
            });
        }

        internal void Reset()
        {
            LoadTierLevel();
            _selectedTierLevel = new List<int>();
        }
    }
}
