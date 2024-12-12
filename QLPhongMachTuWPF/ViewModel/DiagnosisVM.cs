using QLPhongMachTuWPF.Model;
using QLPhongMachTuWPF.View;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace QLPhongMachTuWPF.ViewModel
{
    public class DiagnosisVM : ViewModelBase
    {
        public ICommand AddStaffCommand { get; set; }
        private ObservableCollection<PHIEUKHAM> _diagnosis;
        public ObservableCollection<PHIEUKHAM> DiagnosisList { get => _diagnosis; set { _diagnosis = value; OnPropertyChanged(); } }
        public DiagnosisVM()
        {
            DiagnosisList = new ObservableCollection<PHIEUKHAM>(DataProvider.Ins.db.PHIEUKHAMs);
            AddStaffCommand = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                AddStaff add = new AddStaff();
                add.ShowDialog();

            }
            );
        }
    }
}
