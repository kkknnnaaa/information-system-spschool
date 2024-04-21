using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace spschool
{
    [Serializable()]
    public class Sportsman
    {
        [DisplayName("Номер")]
        public int Code { get; set; }

        [DisplayName("ФИО")]
        public string FIO { get; set; }

        [DisplayName("Спорт")]
        public string Sport { get; set; }

        [DisplayName("Номер команды")]
        public int Team { get; set; }

        [DisplayName("Дата рождения")]
        public DateTime BrDate { get; set; }

        [DisplayName("Возраст")]
        public int Age
        {
            get => DateTime.Now.Year - BrDate.Year;
        }

        [DisplayName("Спортивное звание")]
        public string Title { get; set; }

        public override string ToString()
        {
            return $"Code={Code} FIO={FIO} Sport={Sport} Team={Team} BrDate={BrDate} Title={Title}";
        }

    }
}
