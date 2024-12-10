using QLPhongMachTuWPF.View;
using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Metadata.Edm;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace QLPhongMachTuWPF.ViewModel
{
    public class ManageAppointmentsVM : ViewModelBase
    {
        public ICommand AddAppointmentCommand { get; set; }

        public ManageAppointmentsVM()
        {
            AddAppointmentCommand = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                AddAppointment add = new AddAppointment();
                add.ShowDialog(); 
            }
            );
        }


    }
}
