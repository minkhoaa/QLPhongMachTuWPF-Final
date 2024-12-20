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
        private PHIEUKHAM _SelectedItemCommand {  get; set; }
        public PHIEUKHAM SelectedItemCommand { get => _SelectedItemCommand; set { _SelectedItemCommand = value; OnPropertyChanged(); }  }
        public ICommand AddDiagnosisCommand { get; set; }
        public ICommand ModifyDiagnosis { get; set; }


        public ICommand VerifyCommand { get; set; }

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
            ModifyDiagnosis = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                //if (SelectedItemCommand.NgayKham.HasValue)
                //{
                //    DateTime dateOfBirth = SelectedItemCommand.NgayKham.Value;

                //    SelectedItemCommand.NgaySinh = new DateTime(int.Parse(dateOfBirth.ToString("yyyy")), int.Parse(dateOfBirth.ToString("dd")), int.Parse(dateOfBirth.ToString("MM")));
                //}
                ModifyDiagnosis diagnosis = new ModifyDiagnosis();
                Messenger.Default.Send(SelectedItemCommand);
                diagnosis.ShowDialog();
            }
            );

        }
    }
}
