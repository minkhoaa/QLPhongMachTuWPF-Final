using QLPhongMachTuWPF.Model;
using QLPhongMachTuWPF.View;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data.Entity.Core.Metadata.Edm;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Input;

namespace QLPhongMachTuWPF.ViewModel
{
    public class ManageAppointmentsVM : ViewModelBase
    {
        private LICHHEN _SelectedItemCommand { get; set; }

        public LICHHEN SelectedItemCommand
        {
            get => _SelectedItemCommand;
            set
            {
                if (_SelectedItemCommand != value)
                {
                    _SelectedItemCommand = value;
                    OnPropertyChanged();
                }
            }
        }




        public ICommand AddAppointmentCommand { get; set; }




        public ICollectionView FilteredAppointment{ get; set; }

        private ObservableCollection<LICHHEN> _Appointment;
        public ObservableCollection<LICHHEN> Appointment { get => _Appointment; set { _Appointment = value; OnPropertyChanged(); } }

        public ManageAppointmentsVM()
        {

            Appointment = new ObservableCollection<LICHHEN>(DataProvider.Ins.db.LICHHENs);
            FilteredAppointment = CollectionViewSource.GetDefaultView(Appointment); 


            AddAppointmentCommand = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                AddAppointment add = new AddAppointment();
                add.ShowDialog(); 
            }
            );
        }


    }
}
