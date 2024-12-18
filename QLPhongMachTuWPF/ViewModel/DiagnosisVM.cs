using GalaSoft.MvvmLight.Messaging;
using QLPhongMachTuWPF.Model;
using QLPhongMachTuWPF.View;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System.Windows.Input;

namespace QLPhongMachTuWPF.ViewModel
{
    public class DiagnosisVM : ViewModelBase
    {

        public ICommand AddDiagnosisCommand { get; set; }

        public ICommand  ModifyDiagnosisCommand { get; set; }

        private ObservableCollection<PHIEUKHAM> _DiagnosisList;
        public ICollectionView FilteredDiagnosis{ get; set; }
        public ObservableCollection<PHIEUKHAM> DiagnosisList { get => _DiagnosisList; set { _DiagnosisList = value; OnPropertyChanged(); } }
        public DiagnosisVM()
        {
            DiagnosisList = new ObservableCollection<PHIEUKHAM>(DataProvider.Ins.db.PHIEUKHAMs);

            FilteredDiagnosis = CollectionViewSource.GetDefaultView(DiagnosisList);
            Messenger.Default.Register<PHIEUKHAM>(this, (diagnosis) =>
            {
              
                if (diagnosis == null) return;

                Application.Current.Dispatcher.Invoke(() =>
                {
                    DiagnosisList.Add(diagnosis);
                  
                });

                DataProvider.Ins.db.SaveChanges();
                FilteredDiagnosis?.Refresh();

            });


            AddDiagnosisCommand = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                AddMedicineDiagnosis add = new AddMedicineDiagnosis();
                add.ShowDialog();

            }
            );
            ModifyDiagnosisCommand = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                   
            }
            );
        }
    }
}
